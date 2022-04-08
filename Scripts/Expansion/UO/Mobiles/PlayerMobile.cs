using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Server.Accounting;
using Server.ContextMenus;
using Server.Engines.BulkOrders;
using Server.Engines.CannedEvil;
using Server.Engines.CityLoyalty;
using Server.Engines.Craft;
using Server.Engines.Help;
using Server.Engines.PartySystem;
using Server.Engines.Points;
using Server.Engines.Quests;
using Server.Engines.Shadowguard;
using Server.Engines.VoidPool;
using Server.Engines.VvV;
using Server.Ethics;
using Server.Factions;
using Server.Guilds;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Movement;
using Server.Multis;
using Server.Network;
using Server.Regions;
using Server.Services.Virtues;
using Server.SkillHandlers;
using Server.Spells;
using Server.Spells.Bushido;
using Server.Spells.First;
using Server.Spells.Fifth;
using Server.Spells.Fourth;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;
using Server.Spells.Seventh;
using Server.Spells.Sixth;
using Server.Spells.SkillMasteries;
using Server.Spells.Spellweaving;
using Server.Engines.SphynxFortune;
using Server.Engines.VendorSearching;
using Server.Targeting;

using RankDefinition = Server.Guilds.RankDefinition;

namespace Server.Mobiles
{
    #region Enums
    [Flags]
    public enum PlayerFlag
    {
        None = 0x00000000,
        Glassblowing = 0x00000001,
        Masonry = 0x00000002,
        SandMining = 0x00000004,
        StoneMining = 0x00000008,
        ToggleMiningStone = 0x00000010,
        KarmaLocked = 0x00000020,
        AutoRenewInsurance = 0x00000040,
        UseOwnFilter = 0x00000080,
        PublicMyServUOX = 0x00000100,
        PagingSquelched = 0x00000200,
        Young = 0x00000400,
        AcceptGuildInvites = 0x00000800,
        DisplayChampionTitle = 0x00001000,
        HasStatReward = 0x00002000,
        Bedlam = 0x00010000,
        LibraryFriend = 0x00020000,
        Spellweaving = 0x00040000,
        GemMining = 0x00080000,
        ToggleMiningGem = 0x00100000,
        BasketWeaving = 0x00200000,
        AbyssEntry = 0x00400000,
        ToggleClippings = 0x00800000,
        ToggleCutClippings = 0x01000000,
        ToggleCutReeds = 0x02000000,
        MechanicalLife = 0x04000000,
        Unused = 0x08000000,
        ToggleCutTopiaries = 0x10000000,
        HasValiantStatReward = 0x20000000,
        RefuseTrades = 0x40000000,
    }

    [Flags]
    public enum ExtendedPlayerFlag
    {
        Unused = 0x00000001,
        ToggleStoneOnly = 0x00000002,
        CanBuyCarpets = 0x00000004,
        VoidPool = 0x00000008,
        DisabledPvpWarning = 0x00000010,
    }

    public enum NpcGuild
    {
        None,
        MagesGuild,
        WarriorsGuild,
        ThievesGuild,
        RangersGuild,
        HealersGuild,
        MinersGuild,
        MerchantsGuild,
        TinkersGuild,
        TailorsGuild,
        FishermensGuild,
        BardsGuild,
        BlacksmithsGuild
    }

    public enum SolenFriendship
    {
        None,
        Red,
        Black
    }
    #endregion

    public partial class PlayerMobile : Mobile, IHonorTarget
    {
        public static List<PlayerMobile> Instances { get; private set; }

        static PlayerMobile()
        {
            Instances = new List<PlayerMobile>(0x1000);
        }

        #region Mount Blocking
        public void SetMountBlock(BlockMountType type, TimeSpan duration, bool dismount)
        {
            if (dismount)
            {
                BaseMount.BaseDismount(this, this, type, duration, false);
            }
            else
            {
                BaseMount.SetMountPrevention(this, type, duration);
            }
        }
        #endregion

        #region Stygian Abyss
        public override void ToggleFlying()
        {
            if (Race != Race.Gargoyle)
                return;

            if (Frozen)
            {
                SendLocalizedMessage(1060170); // You cannot use this ability while frozen.
                return;
            }

            if (!Flying)
            {
                if (BeginAction(typeof(FlySpell)))
                {
                    if (Spell is Spell)
                        ((Spell)Spell).Disturb(DisturbType.Unspecified, false, false);

                    Spell spell = new FlySpell(this);
                    spell.Cast();

                    Timer.DelayCall(TimeSpan.FromSeconds(3), () => EndAction(typeof(FlySpell)));
                }
                else
                {
                    LocalOverheadMessage(MessageType.Regular, 0x3B2, 1075124); // You must wait before casting that spell again.
                }
            }
            else if (IsValidLandLocation(Location, Map))
            {
                if (BeginAction(typeof(FlySpell)))
                {
                    if (Spell is Spell)
                        ((Spell)Spell).Disturb(DisturbType.Unspecified, false, false);

                    Animate(AnimationType.Land, 0);
                    Flying = false;
                    BuffInfo.RemoveBuff(this, BuffIcon.Fly);

                    Timer.DelayCall(TimeSpan.FromSeconds(3), () => EndAction(typeof(FlySpell)));
                }
                else
                {
                    LocalOverheadMessage(MessageType.Regular, 0x3B2, 1075124); // You must wait before casting that spell again.
                }
            }
            else
                LocalOverheadMessage(MessageType.Regular, 0x3B2, 1113081); // You may not land here.
        }

        public static bool IsValidLandLocation(Point3D p, Map map)
        {
            return map.CanFit(p.X, p.Y, p.Z, 16, false, false);
        }
        #endregion

        private class CountAndTimeStamp
        {
            private int m_Count;

            public DateTime TimeStamp { get; private set; }

            public int Count
            {
                get => m_Count;
                set
                {
                    m_Count = value;
                    TimeStamp = DateTime.UtcNow;
                }
            }
        }

        private TimeSpan m_NpcGuildGameTime;
        private int m_NonAutoreinsuredItems;
        private RankDefinition m_GuildRank;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool NextEnhanceSuccess { get; set; }

        private List<Mobile> m_AllFollowers;

        public bool UseSummoningRite { get; set; }

        #region Points System
        private PointsSystemProps _PointsSystemProps;
        private BODProps _BODProps;
        private AccountGoldProps _AccountGold;

        [CommandProperty(AccessLevel.GameMaster)]
        public PointsSystemProps PointSystems
        {
            get
            {
                if (_PointsSystemProps == null)
                    _PointsSystemProps = new PointsSystemProps(this);

                return _PointsSystemProps;
            }
            set
            {
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public BODProps BODData
        {
            get
            {
                if (_BODProps == null)
                {
                    _BODProps = new BODProps(this);
                }

                return _BODProps;
            }
            set
            {
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public AccountGoldProps AccountGold
        {
            get
            {
                if (_AccountGold == null)
                {
                    _AccountGold = new AccountGoldProps(this);
                }

                return _AccountGold;
            }
            set
            {
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int AccountSovereigns
        {
            get
            {
                var acct = Account as Account;

                if (acct != null)
                {
                    return acct.Sovereigns;
                }

                return 0;
            }
            set
            {
                var acct = Account as Account;

                if (acct != null)
                {
                    acct.SetSovereigns(value);
                }
            }
        }

        public bool DepositSovereigns(int amount)
        {
            var acct = Account as Account;

            if (acct != null)
            {
                return acct.DepositSovereigns(amount);
            }

            return false;
        }

        public bool WithdrawSovereigns(int amount)
        {
            var acct = Account as Account;

            if (acct != null)
            {
                return acct.WithdrawSovereigns(amount);
            }

            return false;
        }
        #endregion

        #region Getters & Setters
        public List<Mobile> RecentlyReported { get; set; }

        public List<Mobile> AutoStabled { get; private set; }

        public bool NinjaWepCooldown { get; set; }

        public List<Mobile> AllFollowers
        {
            get
            {
                if (m_AllFollowers == null)
                {
                    m_AllFollowers = new List<Mobile>();
                }

                return m_AllFollowers;
            }
        }

        [CommandProperty(AccessLevel.GameMaster, true)]
        public RankDefinition GuildRank
        {
            get
            {
                if (AccessLevel >= AccessLevel.GameMaster)
                {
                    return RankDefinition.Leader;
                }
                else
                {
                    return m_GuildRank;
                }
            }
            set => m_GuildRank = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int GuildMessageHue { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int AllianceMessageHue { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Profession { get; set; }

        public int StepsTaken { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public NpcGuild NpcGuild { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NpcGuildJoinTime { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NextBODTurnInTime { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastOnline { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public long LastMoved => LastMoveTime;

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NpcGuildGameTime { get => m_NpcGuildGameTime; set => m_NpcGuildGameTime = value; }

        public int ExecutesLightningStrike { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ToothAche { get => BaseSweet.GetToothAche(this); set => BaseSweet.SetToothAche(this, value, true); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool MechanicalLife { get => GetFlag(PlayerFlag.MechanicalLife); set => SetFlag(PlayerFlag.MechanicalLife, value); }
        #endregion

        #region PlayerFlags
        public PlayerFlag Flags { get; set; }
        public ExtendedPlayerFlag ExtendedFlags { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PagingSquelched { get => GetFlag(PlayerFlag.PagingSquelched); set => SetFlag(PlayerFlag.PagingSquelched, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Glassblowing { get => GetFlag(PlayerFlag.Glassblowing); set => SetFlag(PlayerFlag.Glassblowing, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Masonry { get => GetFlag(PlayerFlag.Masonry); set => SetFlag(PlayerFlag.Masonry, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SandMining { get => GetFlag(PlayerFlag.SandMining); set => SetFlag(PlayerFlag.SandMining, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool StoneMining { get => GetFlag(PlayerFlag.StoneMining); set => SetFlag(PlayerFlag.StoneMining, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GemMining { get => GetFlag(PlayerFlag.GemMining); set => SetFlag(PlayerFlag.GemMining, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool BasketWeaving { get => GetFlag(PlayerFlag.BasketWeaving); set => SetFlag(PlayerFlag.BasketWeaving, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleMiningStone { get => GetFlag(PlayerFlag.ToggleMiningStone); set => SetFlag(PlayerFlag.ToggleMiningStone, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AbyssEntry { get => GetFlag(PlayerFlag.AbyssEntry); set => SetFlag(PlayerFlag.AbyssEntry, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleMiningGem { get => GetFlag(PlayerFlag.ToggleMiningGem); set => SetFlag(PlayerFlag.ToggleMiningGem, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KarmaLocked { get => GetFlag(PlayerFlag.KarmaLocked); set => SetFlag(PlayerFlag.KarmaLocked, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AutoRenewInsurance { get => GetFlag(PlayerFlag.AutoRenewInsurance); set => SetFlag(PlayerFlag.AutoRenewInsurance, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool UseOwnFilter { get => GetFlag(PlayerFlag.UseOwnFilter); set => SetFlag(PlayerFlag.UseOwnFilter, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AcceptGuildInvites { get => GetFlag(PlayerFlag.AcceptGuildInvites); set => SetFlag(PlayerFlag.AcceptGuildInvites, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasStatReward { get => GetFlag(PlayerFlag.HasStatReward); set => SetFlag(PlayerFlag.HasStatReward, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasValiantStatReward { get => GetFlag(PlayerFlag.HasValiantStatReward); set => SetFlag(PlayerFlag.HasValiantStatReward, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool RefuseTrades
        {
            get => GetFlag(PlayerFlag.RefuseTrades);
            set => SetFlag(PlayerFlag.RefuseTrades, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool DisabledPvpWarning
        {
            get => GetFlag(ExtendedPlayerFlag.DisabledPvpWarning);
            set => SetFlag(ExtendedPlayerFlag.DisabledPvpWarning, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanBuyCarpets
        {
            get => GetFlag(ExtendedPlayerFlag.CanBuyCarpets);
            set => SetFlag(ExtendedPlayerFlag.CanBuyCarpets, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool VoidPool
        {
            get => GetFlag(ExtendedPlayerFlag.VoidPool);
            set => SetFlag(ExtendedPlayerFlag.VoidPool, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleStoneOnly
        {
            get => GetFlag(ExtendedPlayerFlag.ToggleStoneOnly);
            set => SetFlag(ExtendedPlayerFlag.ToggleStoneOnly, value);
        }

        #region Plant system
        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleClippings { get => GetFlag(PlayerFlag.ToggleClippings); set => SetFlag(PlayerFlag.ToggleClippings, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleCutReeds { get => GetFlag(PlayerFlag.ToggleCutReeds); set => SetFlag(PlayerFlag.ToggleCutReeds, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleCutClippings { get => GetFlag(PlayerFlag.ToggleCutClippings); set => SetFlag(PlayerFlag.ToggleCutClippings, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleCutTopiaries { get => GetFlag(PlayerFlag.ToggleCutTopiaries); set => SetFlag(PlayerFlag.ToggleCutTopiaries, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime SSNextSeed { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime SSSeedExpire { get; set; }

        private Point3D m_SSSeedLocation;

        public Point3D SSSeedLocation { get => m_SSSeedLocation; set => m_SSSeedLocation = value; }

        public Map SSSeedMap { get; set; }
        #endregion

        #endregion

        #region Auto Arrow Recovery

        public Dictionary<Type, int> RecoverableAmmo { get; } = new Dictionary<Type, int>();

        public void RecoverAmmo()
        {
            if (Core.SE && Alive)
            {
                foreach (var kvp in RecoverableAmmo)
                {
                    if (kvp.Value > 0)
                    {
                        Item ammo = null;

                        try
                        {
                            ammo = Activator.CreateInstance(kvp.Key) as Item;
                        }
                        catch
                        { }

                        if (ammo != null)
                        {
                            string name = ammo.Name;
                            ammo.Amount = kvp.Value;

                            if (name == null)
                            {
                                if (ammo is Arrow)
                                {
                                    name = "arrow";
                                }
                                else if (ammo is Bolt)
                                {
                                    name = "bolt";
                                }
                            }

                            if (name != null && ammo.Amount > 1)
                            {
                                name = string.Format("{0}s", name);
                            }

                            if (name == null)
                            {
                                name = string.Format("#{0}", ammo.LabelNumber);
                            }

                            PlaceInBackpack(ammo);
                            SendLocalizedMessage(1073504, string.Format("{0}\t{1}", ammo.Amount, name)); // You recover ~1_NUM~ ~2_AMMO~.
                        }
                    }
                }

                RecoverableAmmo.Clear();
            }
        }
        #endregion

        #region Reward Stable Slots
        [CommandProperty(AccessLevel.GameMaster)]
        public int RewardStableSlots { get; set; }
        #endregion


        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime AnkhNextUse { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NextGemOfSalvationUse { get; set; }

        #region Mondain's Legacy
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Bedlam { get => GetFlag(PlayerFlag.Bedlam); set => SetFlag(PlayerFlag.Bedlam, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool LibraryFriend { get => GetFlag(PlayerFlag.LibraryFriend); set => SetFlag(PlayerFlag.LibraryFriend, value); }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Spellweaving { get => GetFlag(PlayerFlag.Spellweaving); set => SetFlag(PlayerFlag.Spellweaving, value); }
        #endregion

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan DisguiseTimeLeft => DisguiseTimers.TimeRemaining(this);

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime PeacedUntil { get; set; }

        [CommandProperty(AccessLevel.Decorator)]
        public override string TitleName
        {
            get
            {
                string name;

                if (Fame >= 10000)
                    name = string.Format("{0} {1}", Female ? "Lady" : "Lord", RawName);
                else
                    name = RawName;

                return name;
            }
        }

        #region Scroll of Alacrity
        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime AcceleratedStart { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public SkillName AcceleratedSkill { get; set; }
        #endregion

        public static Direction GetDirection4(Point3D from, Point3D to)
        {
            int dx = from.X - to.X;
            int dy = from.Y - to.Y;

            int rx = dx - dy;
            int ry = dx + dy;

            Direction ret;

            if (rx >= 0 && ry >= 0)
            {
                ret = Direction.West;
            }
            else if (rx >= 0 && ry < 0)
            {
                ret = Direction.South;
            }
            else if (rx < 0 && ry < 0)
            {
                ret = Direction.East;
            }
            else
            {
                ret = Direction.North;
            }

            return ret;
        }

        public override bool OnDroppedItemToWorld(Item item, Point3D location)
        {
            if (!base.OnDroppedItemToWorld(item, location))
            {
                return false;
            }

            if (Core.AOS)
            {
                IPooledEnumerable mobiles = Map.GetMobilesInRange(location, 0);

                foreach (Mobile m in mobiles)
                {
                    if (m.Z >= location.Z && m.Z < location.Z + 16)
                    {
                        mobiles.Free();
                        return false;
                    }
                }

                mobiles.Free();
            }

            BounceInfo bi = item.GetBounce();

            if (bi != null && (!Core.SA || AccessLevel > AccessLevel.Counselor))
            {
                Type type = item.GetType();

                if (type.IsDefined(typeof(FurnitureAttribute), true) || type.IsDefined(typeof(DynamicFlipingAttribute), true))
                {
                    var objs = type.GetCustomAttributes(typeof(Flipable), true);

                    if (objs != null && objs.Length > 0)
                    {
                        Flipable fp = objs[0] as Flipable;

                        if (fp != null)
                        {
                            var itemIDs = fp.ItemIDs;

                            Point3D oldWorldLoc = bi.m_WorldLoc;
                            Point3D newWorldLoc = location;

                            if (oldWorldLoc.X != newWorldLoc.X || oldWorldLoc.Y != newWorldLoc.Y)
                            {
                                Direction dir = GetDirection4(oldWorldLoc, newWorldLoc);

                                if (itemIDs.Length == 2)
                                {
                                    switch (dir)
                                    {
                                        case Direction.North:
                                        case Direction.South:
                                            item.ItemID = itemIDs[0];
                                            break;
                                        case Direction.East:
                                        case Direction.West:
                                            item.ItemID = itemIDs[1];
                                            break;
                                    }
                                }
                                else if (itemIDs.Length == 4)
                                {
                                    switch (dir)
                                    {
                                        case Direction.South:
                                            item.ItemID = itemIDs[0];
                                            break;
                                        case Direction.East:
                                            item.ItemID = itemIDs[1];
                                            break;
                                        case Direction.North:
                                            item.ItemID = itemIDs[2];
                                            break;
                                        case Direction.West:
                                            item.ItemID = itemIDs[3];
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        public override int GetPacketFlags()
        {
            int flags = base.GetPacketFlags();

            return flags;
        }

        public override int GetOldPacketFlags()
        {
            int flags = base.GetOldPacketFlags();

            return flags;
        }

        public bool GetFlag(PlayerFlag flag)
        {
            return ((Flags & flag) != 0);
        }

        public void SetFlag(PlayerFlag flag, bool value)
        {
            if (value)
            {
                Flags |= flag;
            }
            else
            {
                Flags &= ~flag;
            }
        }

        public bool GetFlag(ExtendedPlayerFlag flag)
        {
            return ((ExtendedFlags & flag) != 0);
        }

        public void SetFlag(ExtendedPlayerFlag flag, bool value)
        {
            if (value)
            {
                ExtendedFlags |= flag;
            }
            else
            {
                ExtendedFlags &= ~flag;
            }
        }

        public DesignContext DesignContext { get; set; }

        public static void Initialize()
        {
            if (FastwalkPrevention)
            {
                PacketHandlers.RegisterThrottler(0x02, MovementThrottle_Callback);
            }

            EventSink.Login += OnLogin;
            EventSink.Logout += OnLogout;
            EventSink.Connected += EventSink_Connected;
            EventSink.Disconnected += EventSink_Disconnected;

            #region Enchanced Client
            EventSink.TargetedSkill += Targeted_Skill;
            EventSink.EquipMacro += EquipMacro;
            EventSink.UnequipMacro += UnequipMacro;
            #endregion

            if (Core.SE)
            {
                Timer.DelayCall(TimeSpan.Zero, CheckPets);
            }
        }

        #region Enhanced Client
        private static void Targeted_Skill(TargetedSkillEventArgs e)
        {
            Mobile from = e.Mobile;
            int SkillId = e.SkillID;
            IEntity target = e.Target;

            if (from == null || target == null)
                return;

            from.TargetLocked = true;

            if (e.SkillID == 35)
            {
                AnimalTaming.DisableMessage = true;
                AnimalTaming.DeferredTarget = false;
            }

            if (from.UseSkill(e.SkillID) && from.Target != null)
            {
                from.Target.Invoke(from, target);
            }

            if (e.SkillID == 35)
            {
                AnimalTaming.DeferredTarget = true;
                AnimalTaming.DisableMessage = false;
            }

            from.TargetLocked = false;
        }

        public static void EquipMacro(EquipMacroEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;

            if (pm != null && pm.Backpack != null && pm.Alive && e.List != null && e.List.Count > 0)
            {
                Container pack = pm.Backpack;

                e.List.ForEach(serial =>
                {
                    Item item = pack.Items.FirstOrDefault(i => i.Serial == serial);

                    if (item != null)
                    {
                        Item toMove = pm.FindItemOnLayer(item.Layer);

                        if (toMove != null)
                        {
                            //pack.DropItem(toMove);
                            toMove.Internalize();

                            if (!pm.EquipItem(item))
                            {
                                pm.EquipItem(toMove);
                            }
                            else
                            {
                                pack.DropItem(toMove);
                            }
                        }
                        else
                        {
                            pm.EquipItem(item);
                        }
                    }
                });
            }
        }

        public static void UnequipMacro(UnequipMacroEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;

            if (pm != null && pm.Backpack != null && pm.Alive && e.List != null && e.List.Count > 0)
            {
                Container pack = pm.Backpack;

                List<Item> worn = new List<Item>(pm.Items);

                foreach (var item in worn)
                {
                    if (e.List.Contains((int)item.Layer))
                    {
                        pack.TryDropItem(pm, item, false);
                    }
                }

                ColUtility.Free(worn);
            }
        }
        #endregion

        private static void CheckPets()
        {
            foreach (PlayerMobile pm in World.Mobiles.Values.OfType<PlayerMobile>())
            {
                if (((!pm.Mounted || (pm.Mount != null && pm.Mount is EtherealMount)) &&
                     (pm.AllFollowers.Count > pm.AutoStabled.Count)) ||
                    (pm.Mounted && (pm.AllFollowers.Count > (pm.AutoStabled.Count + 1))))
                {
                    pm.AutoStablePets(); /* autostable checks summons, et al: no need here */
                }
            }
        }

        public override void OnSkillInvalidated(Skill skill)
        {
            if (Core.AOS && skill.SkillName == SkillName.MagicResist)
            {
                UpdateResistances();
            }
        }

        public override int GetMaxResistance(ResistanceType type)
        {
            if (IsStaff())
            {
                return 100;
            }

            int max = base.GetMaxResistance(type);
            int refineBonus = BaseArmor.GetRefinedResist(this, type);

            if (refineBonus != 0)
            {
                max += refineBonus;
            }
            else
            {
                max += Spells.Mysticism.StoneFormSpell.GetMaxResistBonus(this);
            }

            if (Core.ML && Race == Race.Elf && type == ResistanceType.Energy)
            {
                max += 5; //Intended to go after the 60 max from curse
            }

            if (type != ResistanceType.Physical && 60 < max && Spells.Fourth.CurseSpell.UnderEffect(this))
            {
                max -= 10;
                //max = 60;
            }

            if ((type == ResistanceType.Fire || type == ResistanceType.Poison) && CorpseSkinSpell.IsUnderEffects(this))
            {
                max = CorpseSkinSpell.GetResistMalus(this);
            }

            return max;
        }

        public override void ComputeResistances()
        {
            base.ComputeResistances();

            for (int i = 0; i < Resistances.Length; ++i)
            {
                Resistances[i] = 0;
            }

            Resistances[0] += BasePhysicalResistance;
            Resistances[1] += BaseFireResistance;
            Resistances[2] += BaseColdResistance;
            Resistances[3] += BasePoisonResistance;
            Resistances[4] += BaseEnergyResistance;

            for (int i = 0; ResistanceMods != null && i < ResistanceMods.Count; ++i)
            {
                ResistanceMod mod = ResistanceMods[i];
                int v = (int)mod.Type;

                if (v >= 0 && v < Resistances.Length)
                {
                    Resistances[v] += mod.Offset;
                }
            }

            for (int i = 0; i < Items.Count; ++i)
            {
                Item item = Items[i];

                if (item.CheckPropertyConfliction(this))
                {
                    continue;
                }

                ISetItem setItem = item as ISetItem;

                Resistances[0] += setItem != null && setItem.SetEquipped ? setItem.SetResistBonus(ResistanceType.Physical) : item.PhysicalResistance;
                Resistances[1] += setItem != null && setItem.SetEquipped ? setItem.SetResistBonus(ResistanceType.Fire) : item.FireResistance;
                Resistances[2] += setItem != null && setItem.SetEquipped ? setItem.SetResistBonus(ResistanceType.Cold) : item.ColdResistance;
                Resistances[3] += setItem != null && setItem.SetEquipped ? setItem.SetResistBonus(ResistanceType.Poison) : item.PoisonResistance;
                Resistances[4] += setItem != null && setItem.SetEquipped ? setItem.SetResistBonus(ResistanceType.Energy) : item.EnergyResistance;
            }

            for (int i = 0; i < Resistances.Length; ++i)
            {
                int min = GetMinResistance((ResistanceType)i);
                int max = GetMaxResistance((ResistanceType)i);

                if (max < min)
                {
                    max = min;
                }

                if (Resistances[i] > max)
                {
                    Resistances[i] = max;
                }
                else if (Resistances[i] < min)
                {
                    Resistances[i] = min;
                }
            }
        }

        protected override void OnRaceChange(Race oldRace)
        {
            if (oldRace == Race.Gargoyle && Flying)
            {
                Flying = false;
                SendSpeedControl(SpeedControlType.Disable);
                BuffInfo.RemoveBuff(this, BuffIcon.Fly);
            }
            else if (oldRace != Race.Gargoyle && Race == Race.Gargoyle && Mounted)
            {
                Mount.Rider = null;
            }

            ValidateEquipment();
            UpdateResistances();
        }

        public override int MaxWeight => (((Core.ML && Race == Race.Human) ? 100 : 40) + (int)(3.5 * Str));

        private int m_LastGlobalLight = -1, m_LastPersonalLight = -1;

        public override void OnNetStateChanged()
        {
            m_LastGlobalLight = -1;
            m_LastPersonalLight = -1;
        }

        public override void ComputeBaseLightLevels(out int global, out int personal)
        {
            global = LightCycle.ComputeLevelFor(this);

            bool racialNightSight = (Core.ML && Race == Race.Elf);

            if (LightLevel < 21 && (AosAttributes.GetValue(this, AosAttribute.NightSight) > 0 || racialNightSight))
            {
                personal = 21;
            }
            else
            {
                personal = LightLevel;
            }
        }

        public override void CheckLightLevels(bool forceResend)
        {
            NetState ns = NetState;

            if (ns == null)
            {
                return;
            }

            int global, personal;

            ComputeLightLevels(out global, out personal);

            if (!forceResend)
            {
                forceResend = (global != m_LastGlobalLight || personal != m_LastPersonalLight);
            }

            if (!forceResend)
            {
                return;
            }

            m_LastGlobalLight = global;
            m_LastPersonalLight = personal;

            ns.Send(GlobalLightLevel.Instantiate(global));
            ns.Send(new PersonalLightLevel(this, personal));
        }

        public override bool SendSpeedControl(SpeedControlType type)
        {
            AnimalFormContext context = AnimalForm.GetContext(this);

            if (context != null && context.SpeedBoost)
            {
                switch (type)
                {
                    case SpeedControlType.WalkSpeed: return base.SendSpeedControl(SpeedControlType.WalkSpeedFast);
                    case SpeedControlType.Disable: return base.SendSpeedControl(SpeedControlType.MountSpeed);
                }
            }

            return base.SendSpeedControl(type);
        }

        public override int GetMinResistance(ResistanceType type)
        {
            int magicResist = (int)(Skills[SkillName.MagicResist].Value * 10);
            int min = int.MinValue;

            if (magicResist >= 1000)
            {
                min = 40 + ((magicResist - 1000) / 50);
            }
            else if (magicResist >= 400)
            {
                min = (magicResist - 400) / 15;
            }

            return Math.Max(MinPlayerResistance, Math.Min(MaxPlayerResistance, min));
        }

        #region City Loyalty
        public override int GetResistance(ResistanceType type)
        {
            int resistance = base.GetResistance(type) + SphynxFortune.GetResistanceBonus(this, type);

            if (CityLoyaltySystem.HasTradeDeal(this, TradeDeal.SocietyOfClothiers))
            {
                resistance++;
                return Math.Min(resistance, GetMaxResistance(type));
            }

            return resistance;
        }
        #endregion

        public override void OnManaChange(int oldValue)
        {
            base.OnManaChange(oldValue);
            if (ExecutesLightningStrike > 0)
            {
                if (Mana < ExecutesLightningStrike)
                {
                    SpecialMove.ClearCurrentMove(this);
                }
            }
        }

        private static void OnLogin(LoginEventArgs e)
        {
            Mobile from = e.Mobile;

            CheckAtrophies(from);

            if (AccountHandler.LockdownLevel > AccessLevel.VIP)
            {
                string notice;

                Account acct = from.Account as Account;

                if (acct == null || !acct.HasAccess(from.NetState))
                {
                    if (from.IsPlayer())
                    {
                        notice = "The server is currently under lockdown. No players are allowed to log in at this time.";
                    }
                    else
                    {
                        notice = "The server is currently under lockdown. You do not have sufficient access level to connect.";
                    }

                    Timer.DelayCall(TimeSpan.FromSeconds(1.0), new TimerStateCallback(Disconnect), from);
                }
                else if (from.AccessLevel >= AccessLevel.Administrator)
                {
                    notice =
                        "The server is currently under lockdown. As you are an administrator, you may change this from the [Admin gump.";
                }
                else
                {
                    notice = "The server is currently under lockdown. You have sufficient access level to connect.";
                }

                from.SendGump(new NoticeGump(1060637, 30720, notice, 0xFFC000, 300, 140, null, null));
                return;
            }
            PlayerMobile pm = from as PlayerMobile;
            if (pm != null)
            {
                pm.ClaimAutoStabledPets();
                pm.ValidateEquipment();
                ReportMurdererGump.CheckMurderer(from);
            }
            else if (Siege.SiegeShard && from.Map == Map.Trammel && from.AccessLevel == AccessLevel.Player)
            {
                from.Map = Map.Felucca;
            }

            if (pm != null && pm.Young && from.Account is Account acc && acc.Young)
            {
                TimeSpan ts = Accounting.Account.YoungDuration - acc.TotalGameTime;
                int hours = Math.Max((int)ts.TotalHours, 0);

                pm.SendAsciiMessage("You will enjoy the benefits and relatively safe status of a young player for {0} more hour{1}.", hours, hours != 1 ? "s" : "");
            }

            //if (from is PlayerMobile)
            //{
            //   ((PlayerMobile)from).ClaimAutoStabledPets();
            //    ((PlayerMobile)from).ValidateEquipment();

            //    ReportMurdererGump.CheckMurderer(from);
            //}
            //else if (Siege.SiegeShard && from.Map == Map.Trammel && from.AccessLevel == AccessLevel.Player)
            //{
            //    from.Map = Map.Felucca;
            //}

            if (((from.Map == Map.Trammel && from.Region.IsPartOf("Blackthorn Castle")) || PointsSystem.FellowshipData.Enabled && from.Region.IsPartOf("BlackthornDungeon") || from.Region.IsPartOf("Ver Lor Reg")) && from.Player && from.AccessLevel == AccessLevel.Player && from.CharacterOut)
            {
                StormLevelGump menu = new StormLevelGump(from);
                menu.BeginClose();
                from.SendGump(menu);
            }

            if (from.NetState != null && from.NetState.IsEnhancedClient && from.Mount is EtherealMount)
            {
                Timer.DelayCall(TimeSpan.FromSeconds(1), mount =>
                {
                    if (mount.IsChildOf(from.Backpack))
                    {
                        mount.Rider = from;
                    }
                },
                (EtherealMount)from.Mount);
            }

            from.CheckStatTimers();
        }

        private bool m_NoDeltaRecursion;

        public void ValidateEquipment()
        {
            if (m_NoDeltaRecursion || Map == null || Map == Map.Internal)
            {
                return;
            }

            if (Items == null)
            {
                return;
            }

            m_NoDeltaRecursion = true;
            Timer.DelayCall(TimeSpan.Zero, ValidateEquipment_Sandbox);
        }

        private void ValidateEquipment_Sandbox()
        {
            try
            {
                if (Map == null || Map == Map.Internal)
                {
                    return;
                }

                var items = Items;

                if (items == null)
                {
                    return;
                }

                bool moved = false;

                int str = Str;
                int dex = Dex;
                int intel = Int;

                #region Factions
                int factionItemCount = 0;
                #endregion

                Mobile from = this;

                #region Ethics
                Ethic ethic = Ethic.Find(from);
                #endregion

                for (int i = items.Count - 1; i >= 0; --i)
                {
                    if (i >= items.Count)
                    {
                        continue;
                    }

                    Item item = items[i];

                    #region Ethics
                    if ((item.SavedFlags & 0x100) != 0)
                    {
                        if (item.Hue != Ethic.Hero.Definition.PrimaryHue)
                        {
                            item.SavedFlags &= ~0x100;
                        }
                        else if (ethic != Ethic.Hero)
                        {
                            from.AddToBackpack(item);
                            moved = true;
                            continue;
                        }
                    }
                    else if ((item.SavedFlags & 0x200) != 0)
                    {
                        if (item.Hue != Ethic.Evil.Definition.PrimaryHue)
                        {
                            item.SavedFlags &= ~0x200;
                        }
                        else if (ethic != Ethic.Evil)
                        {
                            from.AddToBackpack(item);
                            moved = true;
                            continue;
                        }
                    }
                    #endregion

                    bool morph = from.FindItemOnLayer(Layer.Earrings) is MorphEarrings;

                    if (item is BaseWeapon weapon)
                    {
                        bool drop = false;

                        if (dex < weapon.DexRequirement)
                        {
                            drop = true;
                        }
                        else if (str < AOS.Scale(weapon.StrRequirement, 100 - weapon.GetLowerStatReq()))
                        {
                            drop = true;
                        }
                        else if (intel < weapon.IntRequirement)
                        {
                            drop = true;
                        }
                        else if (weapon.RequiredRace != null && weapon.RequiredRace != Race && !morph)
                        {
                            drop = true;
                        }

                        if (drop)
                        {
                            string name = weapon.Name;

                            if (name == null)
                            {
                                name = string.Format("#{0}", weapon.LabelNumber);
                            }

                            from.SendLocalizedMessage(1062001, name); // You can no longer wield your ~1_WEAPON~
                            from.AddToBackpack(weapon);
                            moved = true;
                        }
                    }
                    else if (item is BaseArmor armor)
                    {
                        bool drop = false;

                        if (!armor.AllowMaleWearer && !from.Female && from.AccessLevel < AccessLevel.GameMaster)
                        {
                            drop = true;
                        }
                        else if (!armor.AllowFemaleWearer && from.Female && from.AccessLevel < AccessLevel.GameMaster)
                        {
                            drop = true;
                        }
                        else if (armor.RequiredRace != null && armor.RequiredRace != Race && !morph)
                        {
                            drop = true;
                        }
                        else
                        {
                            int strBonus = armor.ComputeStatBonus(StatType.Str), strReq = armor.ComputeStatReq(StatType.Str);
                            int dexBonus = armor.ComputeStatBonus(StatType.Dex), dexReq = armor.ComputeStatReq(StatType.Dex);
                            int intBonus = armor.ComputeStatBonus(StatType.Int), intReq = armor.ComputeStatReq(StatType.Int);

                            if (dex < dexReq || (dex + dexBonus) < 1)
                            {
                                drop = true;
                            }
                            else if (str < strReq || (str + strBonus) < 1)
                            {
                                drop = true;
                            }
                            else if (intel < intReq || (intel + intBonus) < 1)
                            {
                                drop = true;
                            }
                        }

                        if (drop)
                        {
                            string name = armor.Name;

                            if (name == null)
                            {
                                name = string.Format("#{0}", armor.LabelNumber);
                            }

                            if (armor is BaseShield)
                            {
                                from.SendLocalizedMessage(1062003, name); // You can no longer equip your ~1_SHIELD~
                            }
                            else
                            {
                                from.SendLocalizedMessage(1062002, name); // You can no longer wear your ~1_ARMOR~
                            }

                            from.AddToBackpack(armor);
                            moved = true;
                        }
                    }
                    else if (item is BaseClothing clothing)
                    {
                        bool drop = false;

                        if (!clothing.AllowMaleWearer && !from.Female && from.AccessLevel < AccessLevel.GameMaster)
                        {
                            drop = true;
                        }
                        else if (!clothing.AllowFemaleWearer && from.Female && from.AccessLevel < AccessLevel.GameMaster)
                        {
                            drop = true;
                        }
                        else if (clothing.RequiredRace != null && clothing.RequiredRace != Race && !morph)
                        {
                            drop = true;
                        }
                        else
                        {
                            int strBonus = clothing.ComputeStatBonus(StatType.Str);
                            int strReq = clothing.ComputeStatReq(StatType.Str);

                            if (str < strReq || (str + strBonus) < 1)
                            {
                                drop = true;
                            }
                        }

                        if (drop)
                        {
                            string name = clothing.Name;

                            if (name == null)
                            {
                                name = string.Format("#{0}", clothing.LabelNumber);
                            }

                            from.SendLocalizedMessage(1062002, name); // You can no longer wear your ~1_ARMOR~

                            from.AddToBackpack(clothing);
                            moved = true;
                        }
                    }
                    else if (item is BaseQuiver)
                    {
                        if (Race == Race.Gargoyle)
                        {
                            from.AddToBackpack(item);

                            from.SendLocalizedMessage(1062002, "quiver"); // You can no longer wear your ~1_ARMOR~
                            moved = true;
                        }
                    }

                    FactionItem factionItem = FactionItem.Find(item);

                    if (factionItem != null)
                    {
                        bool drop = false;

                        PlayerState state = PlayerState.Find(this);
                        Faction ourFaction = null;

                        if (state != null)
                            ourFaction = state.Faction;

                        if (ourFaction == null || ourFaction != factionItem.Faction)
                        {
                            drop = true;
                        }
                        else if (state != null && state.Rank.Rank < factionItem.MinRank)
                        {
                            drop = true;
                        }
                        else if (++factionItemCount > FactionItem.GetMaxWearables(this))
                        {
                            drop = true;
                        }

                        if (drop)
                        {
                            from.AddToBackpack(item);
                            moved = true;
                        }
                    }

                    #region Vice Vs Virtue
                    IVvVItem vvvItem = item as IVvVItem;

                    if (vvvItem != null && vvvItem.IsVvVItem && !Engines.VvV.ViceVsVirtueSystem.IsVvV(from))
                    {
                        from.AddToBackpack(item);
                        moved = true;
                    }
                    #endregion
                }

                if (moved)
                {
                    from.SendLocalizedMessage(500647); // Some equipment has been moved to your backpack.
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                m_NoDeltaRecursion = false;
            }
        }

        public override void Delta(MobileDelta flag)
        {
            base.Delta(flag);

            if ((flag & MobileDelta.Stat) != 0)
            {
                ValidateEquipment();
            }

            InvalidateProperties();
        }

        private static void Disconnect(object state)
        {
            NetState ns = ((Mobile)state).NetState;

            if (ns != null)
            {
                ns.Dispose();
            }
        }

        private static void OnLogout(LogoutEventArgs e)
        {
            if (!(e.Mobile is PlayerMobile pm))
                return;

            #region Scroll of Alacrity
            if (pm.AcceleratedStart > DateTime.UtcNow)
            {
                pm.AcceleratedStart = DateTime.UtcNow;
                ScrollOfAlacrity.AlacrityEnd(pm);
            }
            #endregion

            BaseFamiliar.OnLogout(pm);

            BaseEscort.DeleteEscort(pm);
        }

        private static void EventSink_Connected(ConnectedEventArgs e)
        {
            if (e.Mobile is PlayerMobile pm)
            {
                pm.SessionStart = DateTime.UtcNow;

                if (pm.Quest != null)
                {
                    pm.Quest.StartTimer();
                }

                #region Mondain's Legacy
                QuestHelper.StartTimer(pm);
                #endregion

                pm.BedrollLogout = false;
                pm.BlanketOfDarknessLogout = false;
                pm.LastOnline = DateTime.UtcNow;
            }

            DisguiseTimers.StartTimer(e.Mobile);

            Timer.DelayCall(TimeSpan.Zero, new TimerStateCallback(ClearSpecialMovesCallback), e.Mobile);

            if (e.Mobile.Account is Account acc && acc.Young && acc.YoungTimer == null)
            {
                acc.YoungTimer = new YoungTimer(acc);
                acc.YoungTimer.Start();
            }
        }

        private static void ClearSpecialMovesCallback(object state)
        {
            Mobile from = (Mobile)state;

            SpecialMove.ClearAllMoves(from);
        }

        private static void EventSink_Disconnected(DisconnectedEventArgs e)
        {
            Mobile from = e.Mobile;
            DesignContext context = DesignContext.Find(from);

            if (context != null)
            {
                /* Client disconnected
				*  - Remove design context
				*  - Eject all from house
				*  - Restore relocated entities
				*/
                // Remove design context
                DesignContext.Remove(from);

                // Eject all from house
                from.RevealingAction();

                foreach (Item item in context.Foundation.GetItems())
                {
                    item.Location = context.Foundation.BanLocation;
                }

                foreach (Mobile mobile in context.Foundation.GetMobiles())
                {
                    mobile.Location = context.Foundation.BanLocation;
                }

                // Restore relocated entities
                context.Foundation.RestoreRelocatedEntities();
            }

            PlayerMobile pm = e.Mobile as PlayerMobile;

            if (pm != null)
            {
                pm.m_GameTime += (DateTime.UtcNow - pm.SessionStart);

                if (pm.Quest != null)
                {
                    pm.Quest.StopTimer();
                }

                #region Mondain's Legacy
                QuestHelper.StopTimer(pm);
                #endregion

                pm.SpeechLog = null;
                pm.LastOnline = DateTime.UtcNow;

                pm.AutoStablePets();
            }

            DisguiseTimers.StopTimer(from);
            Account acc = e.Mobile.Account as Account;

			if (acc != null && acc.YoungTimer != null)
			{
				acc.YoungTimer.Stop();
				acc.YoungTimer = null;
			}

			if (acc != null && pm != null)
			{
				acc.TotalGameTime += DateTime.UtcNow - pm.SessionStart;
			}
        }

        public override void RevealingAction()
        {
            if (DesignContext != null)
            {
                return;
            }

            InvisibilitySpell.RemoveTimer(this);

            base.RevealingAction();
        }

        public override void OnHiddenChanged()
        {
            base.OnHiddenChanged();

            RemoveBuff(BuffIcon.Invisibility);
            //Always remove, default to the hiding icon EXCEPT in the invis spell where it's explicitly set

            if (!Hidden)
            {
                RemoveBuff(BuffIcon.HidingAndOrStealth);
            }
            else // if( !InvisibilitySpell.HasTimer( this ) )
            {
                BuffInfo.AddBuff(this, new BuffInfo(BuffIcon.HidingAndOrStealth, 1075655)); //Hidden/Stealthing & You Are Hidden
            }
        }

        public override void OnSubItemAdded(Item item)
        {
            if (AccessLevel < AccessLevel.GameMaster && item.IsChildOf(Backpack))
            {
                int curWeight = BodyWeight + TotalWeight;

                if (curWeight > MaxWeight)
                {
                    SendLocalizedMessage(1019035, true, string.Format(" : {0} / {1}", curWeight, MaxWeight));
                }
            }
        }

        public override void OnSubItemRemoved(Item item)
        {
            if (Server.Engines.UOStore.UltimaStore.HasPendingItem(this))
                Timer.DelayCall<PlayerMobile>(TimeSpan.FromSeconds(1.5), Server.Engines.UOStore.UltimaStore.CheckPendingItem, this);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            // This will update aggressor for the aggressors master
            if (aggressor is BaseCreature && ((BaseCreature)aggressor).ControlMaster != null && ((BaseCreature)aggressor).ControlMaster != this)
            {
                Mobile aggressiveMaster = ((BaseCreature)aggressor).ControlMaster;

                // First lets find out if the creatures master is in our aggressor list
                AggressorInfo info = Aggressors.FirstOrDefault(i => i.Attacker == aggressiveMaster);

                if (info != null)
                {
                    // already in the list, so we're refreshing it
                    info.Refresh();
                    info.CriminalAggression = criminal;
                }
                else
                {
                    // not in the list, so we're adding it
                    Aggressors.Add(AggressorInfo.Create(aggressiveMaster, this, criminal));

                    if (CanSee(aggressiveMaster) && NetState != null)
                    {
                        NetState.Send(MobileIncoming.Create(NetState, this, aggressiveMaster));
                    }

                    UpdateAggrExpire();
                }

                // Now, if I am in the creatures master aggressor list, it needs to be refreshed
                info = aggressiveMaster.Aggressors.FirstOrDefault(i => i.Attacker == this);

                if (info != null)
                {
                    info.Refresh();
                }

                info = Aggressed.FirstOrDefault(i => i.Defender == aggressiveMaster);

                if (info != null)
                {
                    info.Refresh();
                }

                // next lets find out if we're on the creatures master aggressed list
                info = aggressiveMaster.Aggressed.FirstOrDefault(i => i.Defender == this);

                if (info != null)
                {
                    // already in the list, so we're refreshing it
                    info.Refresh();
                    info.CriminalAggression = criminal;
                }
                else
                {
                    // not in the list, so we're adding it
                    aggressor.Aggressed.Add(AggressorInfo.Create(aggressiveMaster, this, criminal));

                    if (CanSee(aggressiveMaster) && NetState != null)
                    {
                        NetState.Send(MobileIncoming.Create(NetState, this, aggressiveMaster));
                    }

                    UpdateAggrExpire();
                }

                if (aggressiveMaster is PlayerMobile || (aggressiveMaster is BaseCreature && !((BaseCreature)aggressiveMaster).IsMonster))
                {
                    BuffInfo.AddBuff(this, new BuffInfo(BuffIcon.HeatOfBattleStatus, 1153801, 1153827, Aggression.CombatHeatDelay, this, true));
                    BuffInfo.AddBuff(aggressiveMaster, new BuffInfo(BuffIcon.HeatOfBattleStatus, 1153801, 1153827, Aggression.CombatHeatDelay, aggressiveMaster, true));
                }
            }

            base.AggressiveAction(aggressor, criminal);

            /*
            if (aggressor is BaseCreature && ((BaseCreature)aggressor).ControlMaster != null && ((BaseCreature)aggressor).ControlMaster != this)
            {
                Mobile aggressiveMaster = ((BaseCreature)aggressor).ControlMaster;

                if (NotorietyHandlers.CheckAggressor(Aggressors, aggressor))
                {
                    Aggressors.Add(AggressorInfo.Create(aggressiveMaster, this, criminal));
                    aggressiveMaster.Delta(MobileDelta.Noto);

                    if (NotorietyHandlers.CheckAggressed(aggressor.Aggressed, this))
                        aggressiveMaster.Aggressed.Add(AggressorInfo.Create(aggressiveMaster, this, criminal));

                    if (aggressiveMaster is PlayerMobile || (aggressiveMaster is BaseCreature && !((BaseCreature)aggressiveMaster).IsMonster))
                    {
                        BuffInfo.AddBuff(this, new BuffInfo(BuffIcon.HeatOfBattleStatus, 1153801, 1153827, Aggression.CombatHeatDelay, this, true));
                        BuffInfo.AddBuff(aggressiveMaster, new BuffInfo(BuffIcon.HeatOfBattleStatus, 1153801, 1153827, Aggression.CombatHeatDelay, aggressiveMaster, true));
                    }
                }
            }
            */
        }

        public override void DoHarmful(IDamageable damageable, bool indirect)
        {
            base.DoHarmful(damageable, indirect);

            if (ViceVsVirtueSystem.Enabled && (ViceVsVirtueSystem.EnhancedRules || Map == Faction.Facet) && damageable is Mobile)
            {
                ViceVsVirtueSystem.CheckHarmful(this, (Mobile)damageable);
            }
        }

        public override void DoBeneficial(Mobile target)
        {
            base.DoBeneficial(target);

            if (ViceVsVirtueSystem.Enabled && (ViceVsVirtueSystem.EnhancedRules || Map == Faction.Facet) && target != null)
            {
                ViceVsVirtueSystem.CheckBeneficial(this, target);
            }
        }

        public override bool CanBeHarmful(IDamageable damageable, bool message, bool ignoreOurBlessedness, bool ignorePeaceCheck)
        {
            Mobile target = damageable as Mobile;

            if (DesignContext != null || (target is PlayerMobile && ((PlayerMobile)target).DesignContext != null))
            {
                return false;
            }

            #region Mondain's Legacy
            if (Peaced && !ignorePeaceCheck)
            {
                //!+ TODO: message
                return false;
            }
            #endregion

            if ((target is BaseVendor && ((BaseVendor)target).IsInvulnerable) || target is PlayerVendor || target is TownCrier)
            {
                if (message)
                {
                    if (target.Title == null)
                    {
                        SendMessage("{0} the vendor cannot be harmed.", target.Name);
                    }
                    else
                    {
                        SendMessage("{0} {1} cannot be harmed.", target.Name, target.Title);
                    }
                }

                return false;
            }

            if (damageable is IDamageableItem && !((IDamageableItem)damageable).CanDamage)
            {
                if (message)
                    SendMessage("That cannot be harmed.");

                return false;
            }

            return base.CanBeHarmful(damageable, message, ignoreOurBlessedness, ignorePeaceCheck);
        }

        public override bool CanBeBeneficial(Mobile target, bool message, bool allowDead)
        {
            if (DesignContext != null || (target is PlayerMobile && ((PlayerMobile)target).DesignContext != null))
            {
                return false;
            }

            return base.CanBeBeneficial(target, message, allowDead);
        }

        public override bool CheckContextMenuDisplay(IEntity target)
        {
            return (DesignContext == null);
        }

        public override void OnItemAdded(Item item)
        {
            base.OnItemAdded(item);

            if (item is BaseArmor || item is BaseWeapon)
            {
                Hits = Hits;
                Stam = Stam;
                Mana = Mana;
            }

            if (NetState != null)
            {
                CheckLightLevels(false);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public BaseWeapon LastWeapon { get; set; }

        public override void OnItemRemoved(Item item)
        {
            base.OnItemRemoved(item);

            if (item is BaseArmor || item is BaseWeapon)
            {
                Hits = Hits;
                Stam = Stam;
                Mana = Mana;
            }

            if (item is BaseWeapon)
            {
                LastWeapon = item as BaseWeapon;
            }

            if (NetState != null)
            {
                CheckLightLevels(false);
            }
        }

        public override double ArmorRating
        {
            get
            {
                //BaseArmor ar;
                double rating = 0.0;

                AddArmorRating(ref rating, NeckArmor);
                AddArmorRating(ref rating, HandArmor);
                AddArmorRating(ref rating, HeadArmor);
                AddArmorRating(ref rating, ArmsArmor);
                AddArmorRating(ref rating, LegsArmor);
                AddArmorRating(ref rating, ChestArmor);
                AddArmorRating(ref rating, ShieldArmor);

                return VirtualArmor + VirtualArmorMod + rating;
            }
        }

        private void AddArmorRating(ref double rating, Item armor)
        {
            BaseArmor ar = armor as BaseArmor;

            if (ar != null && (!Core.AOS || ar.ArmorAttributes.MageArmor == 0))
            {
                rating += ar.ArmorRatingScaled;
            }
        }

        #region [Stats]Max
        [CommandProperty(AccessLevel.GameMaster)]
        public override int HitsMax
        {
            get
            {
                int strBase;
                int strOffs = GetStatOffset(StatType.Str);

                if (Core.AOS)
                {
                    strBase = Str; //Str already includes GetStatOffset/str
                    strOffs = AosAttributes.GetValue(this, AosAttribute.BonusHits);

                    if (Core.ML && strOffs > 25 && IsPlayer())
                    {
                        strOffs = 25;
                    }

                    if (AnimalForm.UnderTransformation(this, typeof(BakeKitsune)) ||
                        AnimalForm.UnderTransformation(this, typeof(GreyWolf)))
                    {
                        strOffs += 20;
                    }

                    // Skill Masteries
                    if (Core.TOL)
                    {
                        strOffs += ToughnessSpell.GetHPBonus(this);
                        strOffs += InvigorateSpell.GetHPBonus(this);
                    }
                }
                else
                {
                    strBase = RawStr;
                }

                return (strBase / 2) + 50 + strOffs;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int StamMax => base.StamMax + AosAttributes.GetValue(this, AosAttribute.BonusStam);

        [CommandProperty(AccessLevel.GameMaster)]
        public override int ManaMax => base.ManaMax + AosAttributes.GetValue(this, AosAttribute.BonusMana) +
                       ((Core.ML && Race == Race.Elf) ? 20 : 0) +
                       MasteryInfo.IntuitionBonus(this) +
                       UraliTranceTonic.GetManaBuff(this);
        #endregion

        #region Stat Getters/Setters
        [CommandProperty(AccessLevel.GameMaster)]
        public override int Str
        {
            get
            {
                if (Core.ML && IsPlayer())
                {
                    var str = base.Str;

                    return Math.Min(base.Str, StrMaxCap);
                }

                return base.Str;
            }
            set => base.Str = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int Int
        {
            get
            {
                if (Core.ML && IsPlayer())
                {
                    return Math.Min(base.Int, IntMaxCap);
                }

                return base.Int;
            }
            set => base.Int = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int Dex
        {
            get
            {
                if (Core.ML && IsPlayer())
                {
                    var dex = base.Dex;

                    return Math.Min(dex, DexMaxCap);
                }

                return base.Dex;
            }
            set => base.Dex = value;
        }
        #endregion

        public long NextPassiveDetectHidden { get; set; }

        public override bool Move(Direction d)
        {
            NetState ns = NetState;

            if (ns != null)
            {
                if (HasGump(typeof(ResurrectGump)))
                {
                    if (Alive)
                    {
                        CloseGump(typeof(ResurrectGump));
                    }
                    else
                    {
                        //	SendLocalizedMessage(500111); // You are frozen and cannot move.
                        return false;
                    }
                }
            }

            int speed = ComputeMovementSpeed(d);

            bool res;

            if (!Alive)
            {
                MovementImpl.IgnoreMovableImpassables = true;
            }

            res = base.Move(d);

            MovementImpl.IgnoreMovableImpassables = false;

            if (!res)
            {
                return false;
            }

            NextMovementTime += speed;

            if (!Siege.SiegeShard && Core.TickCount - NextPassiveDetectHidden >= 0)
            {
                DetectHidden.DoPassiveDetect(this);
                NextPassiveDetectHidden = Core.TickCount + (int)TimeSpan.FromSeconds(2).TotalMilliseconds;
            }
            return true;
        }

        public override bool CheckMovement(Direction d, out int newZ)
        {
            DesignContext context = DesignContext;

            if (context == null)
            {
                bool check = base.CheckMovement(d, out newZ);

                if (check && Sigil.ExistsOn(this, true) && !Server.Engines.VvV.VvVSigil.CheckMovement(this, d))
                {
                    SendLocalizedMessage(1155414); // You may not remove the sigil from the battle region!
                    return false;
                }

                return check;
            }

            HouseFoundation foundation = context.Foundation;

            newZ = foundation.Z + HouseFoundation.GetLevelZ(context.Level, context.Foundation);

            int newX = X, newY = Y;
            Movement.Movement.Offset(d, ref newX, ref newY);

            int startX = foundation.X + foundation.Components.Min.X + 1;
            int startY = foundation.Y + foundation.Components.Min.Y + 1;
            int endX = startX + foundation.Components.Width - 1;
            int endY = startY + foundation.Components.Height - 2;

            return (newX >= startX && newY >= startY && newX < endX && newY < endY && Map == foundation.Map);
        }

        public override void OnHitsChange(int oldValue)
        {
            if (Race == Race.Gargoyle)
            {
                if (Hits <= HitsMax / 2)
                {
                    BuffInfo.AddBuff(this, new BuffInfo(BuffIcon.Berserk, 1080449, 1115021, string.Format("{0}\t{1}", GetRacialBerserkBuff(false), GetRacialBerserkBuff(true)), false));
                    Delta(MobileDelta.WeaponDamage);
                }
                else if (oldValue < Hits && Hits > HitsMax / 2)
                {
                    BuffInfo.RemoveBuff(this, BuffIcon.Berserk);
                    Delta(MobileDelta.WeaponDamage);
                }
            }

            base.OnHitsChange(oldValue);
        }

        /// <summary>
        /// Returns Racial Berserk value, for spell or melee
        /// </summary>
        /// <param name="spell">true for spell damage, false for damage increase (melee)</param>
        /// <returns></returns>
        public virtual int GetRacialBerserkBuff(bool spell)
        {
            if (Race != Race.Gargoyle || Hits > HitsMax / 2)
                return 0;

            double perc = (Hits / (double)HitsMax) * 100;
            int value = 0;

            perc = (100 - perc) / 20;

            if (perc > 4)
                value += spell ? 12 : 60;
            else if (perc >= 3)
                value += spell ? 9 : 45;
            else if (perc >= 2)
                value += spell ? 6 : 30;
            else if (perc >= 1)
                value += spell ? 3 : 15;

            return value;
        }

        public override void OnHeal(ref int amount, Mobile from)
        {
            base.OnHeal(ref amount, from);

            if (from == null)
                return;

            BestialSetHelper.OnHeal(this, from, ref amount);

            if (Core.SA && amount > 0 && from != null && from != this)
            {
                for (int i = Aggressed.Count - 1; i >= 0; i--)
                {
                    var info = Aggressed[i];

                    if (info.Defender.InRange(Location, Core.GlobalMaxUpdateRange) && info.Defender.DamageEntries.Any(de => de.Damager == this))
                    {
                        info.Defender.RegisterDamage(amount, from);
                    }

                    if (info.Defender.Player && from.CanBeHarmful(info.Defender, false))
                    {
                        from.DoHarmful(info.Defender, true);
                    }
                }

                for (int i = Aggressors.Count - 1; i >= 0; i--)
                {
                    var info = Aggressors[i];

                    if (info.Attacker.InRange(Location, Core.GlobalMaxUpdateRange) && info.Attacker.DamageEntries.Any(de => de.Damager == this))
                    {
                        info.Attacker.RegisterDamage(amount, from);
                    }

                    if (info.Attacker.Player && from.CanBeHarmful(info.Attacker, false))
                    {
                        from.DoHarmful(info.Attacker, true);
                    }
                }
            }
        }

        public override bool AllowItemUse(Item item)
        {
            return DesignContext.Check(this);
        }

        public SkillName[] AnimalFormRestrictedSkills { get; } = new[]
        {
            SkillName.ArmsLore, SkillName.Begging, SkillName.Discordance, SkillName.Forensics, SkillName.Inscribe,
            SkillName.ItemID, SkillName.Meditation, SkillName.Peacemaking, SkillName.Provocation, SkillName.RemoveTrap,
            SkillName.SpiritSpeak, SkillName.Stealing, SkillName.TasteID
        };

        public override bool AllowSkillUse(SkillName skill)
        {
            if (AnimalForm.UnderTransformation(this))
            {
                for (int i = 0; i < AnimalFormRestrictedSkills.Length; i++)
                {
                    if (AnimalFormRestrictedSkills[i] == skill)
                    {
                        #region Mondain's Legacy
                        AnimalFormContext context = AnimalForm.GetContext(this);

                        if (skill == SkillName.Stealing && context.StealingMod != null && context.StealingMod.Value > 0)
                        {
                            continue;
                        }
                        #endregion

                        SendLocalizedMessage(1070771); // You cannot use that skill in this form.
                        return false;
                    }
                }
            }

            return DesignContext.Check(this);
        }

        private bool m_LastProtectedMessage;
        private int m_NextProtectionCheck = 10;

        public virtual void RecheckTownProtection()
        {
            m_NextProtectionCheck = 10;

            GuardedRegion reg = (GuardedRegion)Region.GetRegion(typeof(GuardedRegion));
            bool isProtected = (reg != null && !reg.IsDisabled());

            if (isProtected != m_LastProtectedMessage)
            {
                if (isProtected)
                {
                    SendLocalizedMessage(500112); // You are now under the protection of the town guards.
                }
                else
                {
                    SendLocalizedMessage(500113); // You have left the protection of the town guards.
                }

                m_LastProtectedMessage = isProtected;
            }
        }

        public override void MoveToWorld(Point3D loc, Map map)
        {
            base.MoveToWorld(loc, map);

            RecheckTownProtection();
        }

        public override void SetLocation(Point3D loc, bool isTeleport)
        {
            if (!isTeleport && IsPlayer() && !Flying)
            {
                // moving, not teleporting
                int zDrop = (Location.Z - loc.Z);

                if (zDrop > 20) // we fell more than one story
                {
                    Hits -= ((zDrop / 20) * 10) - 5; // deal some damage; does not kill, disrupt, etc
                    SendMessage("Ouch!");
                }
            }

            base.SetLocation(loc, isTeleport);

            if (isTeleport || --m_NextProtectionCheck == 0)
            {
                RecheckTownProtection();
            }
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            //base.GetContextMenuEntries(from, list);

            list.Add(new PaperdollEntry(this));

            if (from == this)
            {
                if (Core.HS && Alive)
                {
                    list.Add(new SearchVendors(this));
                }

                BaseHouse house = BaseHouse.FindHouseAt(this);

                if (house != null)
                {
                    if (house.IsCoOwner(this))
                    {
                        list.Add(new CallbackEntry(6205, ReleaseCoOwnership));
                    }
                }

                if (Core.SA)
                {
                    list.Add(new TitlesMenuEntry(this));
                }

                if (Alive && Core.SA)
                {
                    list.Add(new Engines.Points.LoyaltyRating(this));
                }

                list.Add(new OpenBackpackEntry(this));

                if (Alive && InsuranceEnabled)
                {
                    if (Core.SA)
                    {
                        list.Add(new CallbackEntry(1114299, OpenItemInsuranceMenu));
                    }

                    list.Add(new CallbackEntry(6201, ToggleItemInsurance));

                    if (!Core.SA)
                    {
                        if (AutoRenewInsurance)
                        {
                            list.Add(new CallbackEntry(6202, CancelRenewInventoryInsurance));
                        }
                        else
                        {
                            list.Add(new CallbackEntry(6200, AutoRenewInventoryInsurance));
                        }
                    }
                }
                else if (Siege.SiegeShard)
                {
                    list.Add(new CallbackEntry(3006168, SiegeBlessItem));
                }

                if (Core.ML && Alive)
                {
                    QuestHelper.GetContextMenuEntries(list);

                    if (!Core.SA && RewardTitles.Count > 0)
                    {
                        list.Add(new CallbackEntry(6229, ShowChangeTitle));
                    }
                }

                if (Quest != null)
                {
                    Quest.GetContextMenuEntries(list);
                }

                if (house != null)
                {
                    if (Alive && house.InternalizedVendors.Count > 0 && house.IsOwner(this))
                    {
                        list.Add(new CallbackEntry(6204, GetVendor));
                    }

                    if (house.IsAosRules)
                    {
                        list.Add(new CallbackEntry(6207, LeaveHouse));
                    }
                }

                if (Core.HS)
                {
                    list.Add(new CallbackEntry(RefuseTrades ? 1154112 : 1154113, ToggleTrades)); // Allow Trades / Refuse Trades				
                }

                if (JusticeProtectors.Count > 0)
                {
                    list.Add(new CallbackEntry(6157, CancelProtection));
                }

                #region Void Pool
                if (VoidPool || Region.IsPartOf<VoidPoolRegion>())
                {
                    var controller = Map == Map.Felucca ? VoidPoolController.InstanceFel : VoidPoolController.InstanceTram;

                    if (controller != null)
                    {
                        if (!VoidPool)
                        {
                            VoidPool = true;
                        }

                        list.Add(new VoidPoolInfo(this, controller));
                    }
                }
                #endregion

                #region TOL Shadowguard
                if (ShadowguardController.GetInstance(Location, Map) != null)
                {
                    list.Add(new ExitEntry(this));
                }
                #endregion

                if (Core.UOR && !Core.SA && Alive)
                {
                    list.Add(new CallbackEntry(6210, ToggleChampionTitleDisplay));
                }

                if (DisabledPvpWarning)
                {
                    list.Add(new CallbackEntry(1113797, EnablePvpWarning));
                }
            }
            else
            {
                if (Core.HS)
                {
                    BaseGalleon galleon = BaseGalleon.FindGalleonAt(from.Location, from.Map);

                    if (galleon != null && galleon.IsOwner(from))
                        list.Add(new ShipAccessEntry(this, from, galleon));
                }

                if (Alive && Core.AOS)
                {
                    Party theirParty = from.Party as Party;
                    Party ourParty = Party as Party;

                    if (theirParty == null && ourParty == null)
                    {
                        list.Add(new AddToPartyEntry(from, this));
                    }
                    else if (theirParty != null && theirParty.Leader == from)
                    {
                        if (ourParty == null)
                        {
                            list.Add(new AddToPartyEntry(from, this));
                        }
                        else if (ourParty == theirParty)
                        {
                            list.Add(new RemoveFromPartyEntry(from, this));
                        }
                    }
                }

                if (Core.TOL && from.InRange(this, 10))
                {
                    list.Add(new CallbackEntry(1077728, () => OpenTrade(from))); // Trade
                }

                BaseHouse curhouse = BaseHouse.FindHouseAt(this);

                if (curhouse != null)
                {
                    if (Alive && Core.AOS && curhouse.IsAosRules && curhouse.IsFriend(from))
                    {
                        list.Add(new EjectPlayerEntry(from, this));
                    }
                }
            }
        }

        private void CancelProtection()
        {
            for (int i = 0; i < JusticeProtectors.Count; ++i)
            {
                Mobile prot = JusticeProtectors[i];

                string args = string.Format("{0}\t{1}", Name, prot.Name);

                prot.SendLocalizedMessage(1049371, args);
                // The protective relationship between ~1_PLAYER1~ and ~2_PLAYER2~ has been ended.
                SendLocalizedMessage(1049371, args);
                // The protective relationship between ~1_PLAYER1~ and ~2_PLAYER2~ has been ended.
            }

            JusticeProtectors.Clear();
        }

        #region Insurance
        private void ToggleItemInsurance()
        {
            if (!CheckAlive())
            {
                return;
            }

            BeginTarget(-1, false, TargetFlags.None, ToggleItemInsurance_Callback);
            SendLocalizedMessage(1060868); // Target the item you wish to toggle insurance status on <ESC> to cancel
        }

        private bool CanInsure(Item item)
        {
            #region Mondain's Legacy
            if (item is BaseQuiver && item.LootType == LootType.Regular)
            {
                return true;
            }
            #endregion

            if (((item is Container) && !(item is BaseQuiver)) || item is BagOfSending || item is KeyRing || item is MountItem)
            {
                return false;
            }

            if ((item is Spellbook && item.LootType == LootType.Blessed) || item is Runebook || item is PotionKeg ||
                item is Sigil)
            {
                return false;
            }

            if (item is BaseBalmOrLotion || item is GemOfSalvation || item is SeedOfLife || item is ManaDraught)
            {
                return false;
            }

            if (item.Stackable)
            {
                return false;
            }

            if (item.LootType == LootType.Cursed)
            {
                return false;
            }

            if (item.ItemID == 0x204E) // death shroud
            {
                return false;
            }

            if (item.LootType == LootType.Blessed)
                return false;

            return true;
        }

        private void ToggleItemInsurance_Callback(Mobile from, object obj)
        {
            if (!CheckAlive())
                return;

            ToggleItemInsurance_Callback(from, obj as Item, true);
        }

        private void ToggleItemInsurance_Callback(Mobile from, Item item, bool target)
        {
            if (item == null || !item.IsChildOf(this))
            {
                if (target)
                    BeginTarget(-1, false, TargetFlags.None, new TargetCallback(ToggleItemInsurance_Callback));

                SendLocalizedMessage(1060871, "", 0x23); // You can only insure items that you have equipped or that are in your backpack
            }
            else if (item.Insured)
            {
                item.Insured = false;

                SendLocalizedMessage(1060874, "", 0x35); // You cancel the insurance on the item

                if (target)
                {
                    BeginTarget(-1, false, TargetFlags.None, new TargetCallback(ToggleItemInsurance_Callback));
                    SendLocalizedMessage(1060868, "", 0x23); // Target the item you wish to toggle insurance status on <ESC> to cancel
                }
            }
            else if (!CanInsure(item))
            {
                if (target)
                    BeginTarget(-1, false, TargetFlags.None, new TargetCallback(ToggleItemInsurance_Callback));

                SendLocalizedMessage(1060869, "", 0x23); // You cannot insure that
            }
            else
            {
                if (!item.PayedInsurance)
                {
                    int cost = GetInsuranceCost(item);

                    if (Banker.Withdraw(from, cost))
                    {
                        SendLocalizedMessage(1060398, cost.ToString()); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
                        item.PayedInsurance = true;
                    }
                    else
                    {
                        SendLocalizedMessage(1061079, "", 0x23); // You lack the funds to purchase the insurance
                        return;
                    }
                }

                item.Insured = true;

                SendLocalizedMessage(1060873, "", 0x23); // You have insured the item

                if (target)
                {
                    BeginTarget(-1, false, TargetFlags.None, new TargetCallback(ToggleItemInsurance_Callback));
                    SendLocalizedMessage(1060868, "", 0x23); // Target the item you wish to toggle insurance status on <ESC> to cancel
                }
            }
        }

        public int GetInsuranceCost(Item item)
        {
            var imbueWeight = Imbuing.GetTotalWeight(item, -1, false, false);
            int cost = 600; // this handles old items, set items, etc

            if (item.GetType().IsAssignableFrom(typeof(Factions.FactionItem)))
                cost = 800;
            else if (imbueWeight > 0)
                cost = Math.Min(800, Math.Max(10, imbueWeight));
            else if (Mobiles.GenericBuyInfo.BuyPrices.ContainsKey(item.GetType()))
                cost = Math.Min(800, Math.Max(10, Mobiles.GenericBuyInfo.BuyPrices[item.GetType()]));
            else if (item.LootType == LootType.Newbied)
                cost = 10;

            var negAttrs = RunicReforging.GetNegativeAttributes(item);

            if (negAttrs != null && negAttrs.Prized > 0)
                cost *= 2;

            if (Region != null)
                cost = (int)(cost * Region.InsuranceMultiplier);

            return cost;
        }

        private void AutoRenewInventoryInsurance()
        {
            if (!CheckAlive())
            {
                return;
            }

            SendLocalizedMessage(1060881, "", 0x23); // You have selected to automatically reinsure all insured items upon death
            AutoRenewInsurance = true;
        }

        private void CancelRenewInventoryInsurance()
        {
            if (!CheckAlive())
            {
                return;
            }

            if (Core.SE)
            {
                if (!HasGump(typeof(CancelRenewInventoryInsuranceGump)))
                {
                    SendGump(new CancelRenewInventoryInsuranceGump(this, null));
                }
            }
            else
            {
                SendLocalizedMessage(1061075, "", 0x23); // You have cancelled automatically reinsuring all insured items upon death
                AutoRenewInsurance = false;
            }
        }

        #region Siege Bless Item

        [CommandProperty(AccessLevel.GameMaster)]
        public Item BlessedItem { get; set; }

        private void SiegeBlessItem()
        {
            if (BlessedItem != null && BlessedItem.Deleted)
                BlessedItem = null;

            BeginTarget(2, false, TargetFlags.None, (from, targeted) =>
            {
                Siege.TryBlessItem(this, targeted);
            });
        }

        public override bool Drop(Point3D loc)
        {
            if (!Siege.SiegeShard || BlessedItem == null)
                return base.Drop(loc);

            Item item = Holding;
            bool drop = base.Drop(loc);

            if (item != null && drop && item.Parent == null && BlessedItem != null && BlessedItem == item)
            {
                BlessedItem = null;
                item.LootType = LootType.Regular;

                SendLocalizedMessage(1075292, item.Name != null ? item.Name : "#" + item.LabelNumber.ToString()); // ~1_NAME~ has been unblessed.
            }

            return drop;
        }
        #endregion

        private class CancelRenewInventoryInsuranceGump : Gump
        {
            private readonly PlayerMobile m_Player;
            private readonly ItemInsuranceMenuGump m_InsuranceGump;

            public CancelRenewInventoryInsuranceGump(PlayerMobile player, ItemInsuranceMenuGump insuranceGump)
                : base(250, 200)
            {
                m_Player = player;
                m_InsuranceGump = insuranceGump;

                AddBackground(0, 0, 240, 142, 0x13BE);
                AddImageTiled(6, 6, 228, 100, 0xA40);
                AddImageTiled(6, 116, 228, 20, 0xA40);
                AddAlphaRegion(6, 6, 228, 142);

                AddHtmlLocalized(8, 8, 228, 100, 1071021, 0x7FFF, false, false);
                // You are about to disable inventory insurance auto-renewal.

                AddButton(6, 116, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
                AddHtmlLocalized(40, 118, 450, 20, 1060051, 0x7FFF, false, false); // CANCEL

                AddButton(114, 116, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0);
                AddHtmlLocalized(148, 118, 450, 20, 1071022, 0x7FFF, false, false); // DISABLE IT!
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (!m_Player.CheckAlive())
                {
                    return;
                }

                if (info.ButtonID == 1)
                {
                    m_Player.SendLocalizedMessage(1061075, "", 0x23);
                    // You have cancelled automatically reinsuring all insured items upon death
                    m_Player.AutoRenewInsurance = false;
                }
                else
                {
                    m_Player.SendLocalizedMessage(1042021); // Cancelled.
                }

                if (m_InsuranceGump != null)
                    m_Player.SendGump(m_InsuranceGump.NewInstance());
            }
        }

        private void OpenItemInsuranceMenu()
        {
            if (!CheckAlive())
                return;

            List<Item> items = new List<Item>();

            foreach (Item item in Items)
            {
                if (DisplayInItemInsuranceGump(item))
                    items.Add(item);
            }

            Container pack = Backpack;

            if (pack != null)
                items.AddRange(pack.FindItemsByType<Item>(true, DisplayInItemInsuranceGump));

            // TODO: Investigate item sorting

            CloseGump(typeof(ItemInsuranceMenuGump));

            if (items.Count == 0)
                SendLocalizedMessage(1114915, "", 0x35); // None of your current items meet the requirements for insurance.
            else
                SendGump(new ItemInsuranceMenuGump(this, items.ToArray()));
        }

        private bool DisplayInItemInsuranceGump(Item item)
        {
            if (item.Parent is LockableContainer && ((LockableContainer)item.Parent).Locked)
                return false;

            return ((item.Visible || AccessLevel >= AccessLevel.GameMaster) && (item.Insured || CanInsure(item)));
        }

        private class ItemInsuranceMenuGump : Gump
        {
            private PlayerMobile m_From;
            private Item[] m_Items;
            private bool[] m_Insure;
            private int m_Page;

            public ItemInsuranceMenuGump(PlayerMobile from, Item[] items)
                : this(from, items, null, 0)
            {
            }

            public ItemInsuranceMenuGump(PlayerMobile from, Item[] items, bool[] insure, int page)
                : base(25, 50)
            {
                m_From = from;
                m_Items = items;

                if (insure == null)
                {
                    insure = new bool[items.Length];

                    for (int i = 0; i < items.Length; ++i)
                        insure[i] = items[i].Insured;
                }

                m_Insure = insure;
                m_Page = page;

                AddPage(0);

                AddBackground(0, 0, 520, 510, 0x13BE);
                AddImageTiled(10, 10, 500, 30, 0xA40);
                AddImageTiled(10, 50, 500, 355, 0xA40);
                AddImageTiled(10, 415, 500, 80, 0xA40);
                AddAlphaRegion(10, 10, 500, 485);

                AddButton(15, 470, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
                AddHtmlLocalized(50, 472, 80, 20, 1011012, 0x7FFF, false, false); // CANCEL

                if (from.AutoRenewInsurance)
                    AddButton(360, 10, 9723, 9724, 1, GumpButtonType.Reply, 0);
                else
                    AddButton(360, 10, 9720, 9722, 1, GumpButtonType.Reply, 0);

                AddHtmlLocalized(395, 14, 105, 20, 1114122, 0x7FFF, false, false); // AUTO REINSURE

                AddButton(395, 470, 0xFA5, 0xFA6, 2, GumpButtonType.Reply, 0);
                AddHtmlLocalized(430, 472, 50, 20, 1006044, 0x7FFF, false, false); // OK

                AddHtmlLocalized(10, 14, 150, 20, 1114121, 0x7FFF, false, false); // <CENTER>ITEM INSURANCE MENU</CENTER>

                AddHtmlLocalized(45, 54, 70, 20, 1062214, 0x7FFF, false, false); // Item
                AddHtmlLocalized(250, 54, 70, 20, 1061038, 0x7FFF, false, false); // Cost
                AddHtmlLocalized(400, 54, 70, 20, 1114311, 0x7FFF, false, false); // Insured

                int balance = Banker.GetBalance(from);
                int cost = 0;

                for (int i = 0; i < items.Length; ++i)
                {
                    if (insure[i])
                        cost += m_From.GetInsuranceCost(items[i]);
                }

                AddHtmlLocalized(15, 420, 300, 20, 1114310, 0x7FFF, false, false); // GOLD AVAILABLE:
                AddLabel(215, 420, 0x481, balance.ToString());
                AddHtmlLocalized(15, 435, 300, 20, 1114123, 0x7FFF, false, false); // TOTAL COST OF INSURANCE:
                AddLabel(215, 435, 0x481, cost.ToString());

                if (cost != 0)
                {
                    AddHtmlLocalized(15, 450, 300, 20, 1114125, 0x7FFF, false, false); // NUMBER OF DEATHS PAYABLE:
                    AddLabel(215, 450, 0x481, (balance / cost).ToString());
                }

                for (int i = page * 4, y = 72; i < (page + 1) * 4 && i < items.Length; ++i, y += 75)
                {
                    Item item = items[i];
                    Rectangle2D b = ItemBounds.Table[item.ItemID];

                    AddImageTiledButton(40, y, 0x918, 0x918, 0, GumpButtonType.Page, 0, item.ItemID, item.Hue, 40 - b.Width / 2 - b.X, 30 - b.Height / 2 - b.Y);
                    AddItemProperty(item.Serial);

                    if (insure[i])
                    {
                        AddButton(400, y, 9723, 9724, 100 + i, GumpButtonType.Reply, 0);
                        AddLabel(250, y, 0x481, m_From.GetInsuranceCost(item).ToString());
                    }
                    else
                    {
                        AddButton(400, y, 9720, 9722, 100 + i, GumpButtonType.Reply, 0);
                        AddLabel(250, y, 0x66C, m_From.GetInsuranceCost(item).ToString());
                    }
                }

                if (page >= 1)
                {
                    AddButton(15, 380, 0xFAE, 0xFAF, 3, GumpButtonType.Reply, 0);
                    AddHtmlLocalized(50, 380, 450, 20, 1044044, 0x7FFF, false, false); // PREV PAGE
                }

                if ((page + 1) * 4 < items.Length)
                {
                    AddButton(400, 380, 0xFA5, 0xFA7, 4, GumpButtonType.Reply, 0);
                    AddHtmlLocalized(435, 380, 70, 20, 1044045, 0x7FFF, false, false); // NEXT PAGE
                }
            }

            public ItemInsuranceMenuGump NewInstance()
            {
                return new ItemInsuranceMenuGump(m_From, m_Items, m_Insure, m_Page);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (info.ButtonID == 0 || !m_From.CheckAlive())
                    return;

                switch (info.ButtonID)
                {
                    case 1: // Auto Reinsure
                        {
                            if (m_From.AutoRenewInsurance)
                            {
                                if (!m_From.HasGump(typeof(CancelRenewInventoryInsuranceGump)))
                                    m_From.SendGump(new CancelRenewInventoryInsuranceGump(m_From, this));
                            }
                            else
                            {
                                m_From.AutoRenewInventoryInsurance();
                                m_From.SendGump(new ItemInsuranceMenuGump(m_From, m_Items, m_Insure, m_Page));
                            }

                            break;
                        }
                    case 2: // OK
                        {
                            m_From.SendGump(new ItemInsuranceMenuConfirmGump(m_From, m_Items, m_Insure, m_Page));

                            break;
                        }
                    case 3: // Prev
                        {
                            if (m_Page >= 1)
                                m_From.SendGump(new ItemInsuranceMenuGump(m_From, m_Items, m_Insure, m_Page - 1));

                            break;
                        }
                    case 4: // Next
                        {
                            if ((m_Page + 1) * 4 < m_Items.Length)
                                m_From.SendGump(new ItemInsuranceMenuGump(m_From, m_Items, m_Insure, m_Page + 1));

                            break;
                        }
                    default:
                        {
                            int idx = info.ButtonID - 100;

                            if (idx >= 0 && idx < m_Items.Length)
                                m_Insure[idx] = !m_Insure[idx];

                            m_From.SendGump(new ItemInsuranceMenuGump(m_From, m_Items, m_Insure, m_Page));

                            break;
                        }
                }
            }
        }

        private class ItemInsuranceMenuConfirmGump : Gump
        {
            private PlayerMobile m_From;
            private Item[] m_Items;
            private bool[] m_Insure;
            private int m_Page;

            public ItemInsuranceMenuConfirmGump(PlayerMobile from, Item[] items, bool[] insure, int page)
                : base(250, 200)
            {
                m_From = from;
                m_Items = items;
                m_Insure = insure;
                m_Page = page;

                AddBackground(0, 0, 240, 142, 0x13BE);
                AddImageTiled(6, 6, 228, 100, 0xA40);
                AddImageTiled(6, 116, 228, 20, 0xA40);
                AddAlphaRegion(6, 6, 228, 142);

                AddHtmlLocalized(8, 8, 228, 100, 1114300, 0x7FFF, false, false); // Do you wish to insure all newly selected items?

                AddButton(6, 116, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
                AddHtmlLocalized(40, 118, 450, 20, 1060051, 0x7FFF, false, false); // CANCEL

                AddButton(114, 116, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0);
                AddHtmlLocalized(148, 118, 450, 20, 1073996, 0x7FFF, false, false); // ACCEPT
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (!m_From.CheckAlive())
                    return;

                if (info.ButtonID == 1)
                {
                    for (int i = 0; i < m_Items.Length; ++i)
                    {
                        Item item = m_Items[i];

                        if (item.Insured != m_Insure[i])
                            m_From.ToggleItemInsurance_Callback(m_From, item, false);
                    }
                }
                else
                {
                    m_From.SendLocalizedMessage(1042021); // Cancelled.
                    m_From.SendGump(new ItemInsuranceMenuGump(m_From, m_Items, m_Insure, m_Page));
                }
            }
        }

        #endregion

        private void ToggleTrades()
        {
            RefuseTrades = !RefuseTrades;
        }

        private void GetVendor()
        {
            BaseHouse house = BaseHouse.FindHouseAt(this);

            if (CheckAlive() && house != null && house.IsOwner(this) && house.InternalizedVendors.Count > 0)
            {
                CloseGump(typeof(ReclaimVendorGump));
                SendGump(new ReclaimVendorGump(house));
            }
        }

        private void LeaveHouse()
        {
            BaseHouse house = BaseHouse.FindHouseAt(this);

            if (house != null)
            {
                Location = house.BanLocation;
            }
        }

        private void ReleaseCoOwnership()
        {
            BaseHouse house = BaseHouse.FindHouseAt(this);

            if (house != null && house.IsCoOwner(this))
            {
                SendGump(new WarningGump(1060635, 30720, 1062006, 32512, 420, 280, new WarningGumpCallback(ClearCoOwners_Callback), house));
            }
        }

        public void ClearCoOwners_Callback(Mobile from, bool okay, object state)
        {
            BaseHouse house = (BaseHouse)state;

            if (house.Deleted)
                return;

            if (okay && house.IsCoOwner(from))
            {
                if (house.CoOwners != null)
                    house.CoOwners.Remove(from);

                from.SendLocalizedMessage(501300); // You have been removed as a house co-owner.
            }
        }

        private void EnablePvpWarning()
        {
            DisabledPvpWarning = false;
            SendLocalizedMessage(1113798); // Your PvP warning query has been re-enabled.
        }

        private delegate void ContextCallback();

        private class CallbackEntry : ContextMenuEntry
        {
            private readonly ContextCallback m_Callback;

            public CallbackEntry(int number, ContextCallback callback)
                : this(number, -1, callback)
            { }

            public CallbackEntry(int number, int range, ContextCallback callback)
                : base(number, range)
            {
                m_Callback = callback;
            }

            public override void OnClick()
            {
                if (m_Callback != null)
                {
                    m_Callback();
                }
            }
        }

        public override void OnAosSingleClick(Mobile from)
        {
            if (ViewOPL)
            {
                base.OnAosSingleClick(from);
            }
            else if (from.Region.OnSingleClick(from, this))
            {
                OnSingleClick(from);
            }
        }

        public override void DisruptiveAction()
        {
            if (Meditating)
            {
                RemoveBuff(BuffIcon.ActiveMeditation);
            }

            base.DisruptiveAction();
        }

        public override bool Meditating
        {
            set
            {
                base.Meditating = value;
                if (value == false)
                {
                    RemoveBuff(BuffIcon.ActiveMeditation);
                }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this == from && !Warmode)
            {
                IMount mount = Mount;

                if (mount != null && !DesignContext.Check(this))
                {
                    return;
                }
            }

            base.OnDoubleClick(from);
        }

        public override void DisplayPaperdollTo(Mobile to)
        {
            if (DesignContext.Check(this))
            {
                base.DisplayPaperdollTo(to);
            }
        }

        private static bool m_NoRecursion;

        public override bool CheckEquip(Item item)
        {
            if (!base.CheckEquip(item))
            {
                return false;
            }

            Region r = Region.Find(Location, Map);

            if (r is Server.Engines.ArenaSystem.ArenaRegion)
            {
                if (!((Server.Engines.ArenaSystem.ArenaRegion)r).AllowItemEquip(this, item))
                {
                    return false;
                }
            }

            #region Factions
            FactionItem factionItem = FactionItem.Find(item);

            if (factionItem != null)
            {
                PlayerState state = PlayerState.Find(this);
                Faction faction = null;

                if (state != null)
                {
                    faction = state.Faction;
                }

                if (faction == null)
                {
                    SendLocalizedMessage(1010371); // You cannot equip a faction item!
                    return false;
                }
                else if (faction != factionItem.Faction)
                {
                    SendLocalizedMessage(1010372); // You cannot equip an opposing faction's item!
                    return false;
                }
                else if (state != null && state.Rank.Rank < factionItem.MinRank)
                {
                    SendLocalizedMessage(1094804); // You are not high enough in rank to equip this item.
                    return false;
                }
                else
                {
                    int maxWearables = FactionItem.GetMaxWearables(this);

                    for (int i = 0; i < Items.Count; ++i)
                    {
                        Item equiped = Items[i];

                        if (item != equiped && FactionItem.Find(equiped) != null)
                        {
                            if (--maxWearables == 0)
                            {
                                SendLocalizedMessage(1010373); // You do not have enough rank to equip more faction items!
                                return false;
                            }
                        }
                    }
                }
            }
            #endregion

            #region Vice Vs Virtue
            IVvVItem vvvItem = item as IVvVItem;

            if (vvvItem != null && vvvItem.IsVvVItem && !Engines.VvV.ViceVsVirtueSystem.IsVvV(this))
            {
                return false;
            }
            #endregion

            if (AccessLevel < AccessLevel.GameMaster && item.Layer != Layer.Mount && HasTrade)
            {
                BounceInfo bounce = item.GetBounce();

                if (bounce != null)
                {
                    if (bounce.m_Parent is Item)
                    {
                        Item parent = (Item)bounce.m_Parent;

                        if (parent == Backpack || parent.IsChildOf(Backpack))
                        {
                            return true;
                        }
                    }
                    else if (bounce.m_Parent == this)
                    {
                        return true;
                    }
                }

                SendLocalizedMessage(1004042); // You can only equip what you are already carrying while you have a trade pending.
                return false;
            }

            return true;
        }

        public override bool OnDragLift(Item item)
        {
            if (item is IPromotionalToken && ((IPromotionalToken)item).GumpType != null)
            {
                Type t = ((IPromotionalToken)item).GumpType;

                if (HasGump(t))
                    CloseGump(t);
            }

            return base.OnDragLift(item);
        }

        public override bool CheckTrade(
            Mobile to, Item item, SecureTradeContainer cont, bool message, bool checkItems, int plusItems, int plusWeight)
        {
            int msgNum = 0;

            if (BlessedItem != null && BlessedItem == item)
            {
                msgNum = 1075282; // You cannot trade a blessed item.
            }

            if (msgNum == 0 && cont == null)
            {
                if (to.Holding != null)
                {
                    msgNum = 1062727; // You cannot trade with someone who is dragging something.
                }
                else if (HasTrade)
                {
                    msgNum = 1062781; // You are already trading with someone else!
                }
                else if (to.HasTrade)
                {
                    msgNum = 1062779; // That person is already involved in a trade
                }
                else if (to is PlayerMobile && ((PlayerMobile)to).RefuseTrades)
                {
                    msgNum = 1154111; // ~1_NAME~ is refusing all trades.
                }
                else if (item is IFactionItem && ((IFactionItem)item).FactionItemState != null)
                {
                    msgNum = 1094803; // This faction reward is bound to you, and cannot be traded.
                }
            }

            if (msgNum == 0 && item != null)
            {
                if (cont != null)
                {
                    plusItems += cont.TotalItems;
                    plusWeight += cont.TotalWeight;
                }

                if (Backpack == null || !Backpack.CheckHold(this, item, false, checkItems, plusItems, plusWeight))
                {
                    msgNum = 1004040; // You would not be able to hold this if the trade failed.
                }
                else if (to.Backpack == null || !to.Backpack.CheckHold(to, item, false, checkItems, plusItems, plusWeight))
                {
                    msgNum = 1004039; // The recipient of this trade would not be able to carry 
                }
                else
                {
                    msgNum = CheckContentForTrade(item);
                }
            }

            if (msgNum == 0)
            {
                return true;
            }

            if (!message)
            {
                return false;
            }

            if (msgNum == 1154111)
            {
                if (to != null)
                {
                    SendLocalizedMessage(msgNum, to.Name);
                }
            }
            else
            {
                SendLocalizedMessage(msgNum);
            }

            return false;
        }

        private static int CheckContentForTrade(Item item)
        {
            if (item is TrapableContainer && ((TrapableContainer)item).TrapType != TrapType.None)
            {
                return 1004044; // You may not trade trapped items.
            }

            if (StolenItem.IsStolen(item))
            {
                return 1004043; // You may not trade recently stolen items.
            }

            if (item is Container)
            {
                foreach (Item subItem in item.Items)
                {
                    int msg = CheckContentForTrade(subItem);

                    if (msg != 0)
                    {
                        return msg;
                    }
                }
            }

            return 0;
        }

        public override bool CheckNonlocalDrop(Mobile from, Item item, Item target)
        {
            if (!base.CheckNonlocalDrop(from, item, target))
            {
                return false;
            }

            if (from.AccessLevel >= AccessLevel.GameMaster)
            {
                return true;
            }

            Container pack = Backpack;
            if (from == this && HasTrade && (target == pack || target.IsChildOf(pack)))
            {
                BounceInfo bounce = item.GetBounce();

                if (bounce != null && bounce.m_Parent is Item)
                {
                    Item parent = (Item)bounce.m_Parent;

                    if (parent == pack || parent.IsChildOf(pack))
                    {
                        return true;
                    }
                }

                SendLocalizedMessage(1004041); // You can't do that while you have a trade pending.
                return false;
            }

            return true;
        }

        protected override void OnLocationChange(Point3D oldLocation)
        {
            CheckLightLevels(false);

            DesignContext context = DesignContext;

            if (context == null || m_NoRecursion)
            {
                return;
            }

            m_NoRecursion = true;

            HouseFoundation foundation = context.Foundation;

            int newX = X, newY = Y;
            int newZ = foundation.Z + HouseFoundation.GetLevelZ(context.Level, context.Foundation);

            int startX = foundation.X + foundation.Components.Min.X + 1;
            int startY = foundation.Y + foundation.Components.Min.Y + 1;
            int endX = startX + foundation.Components.Width - 1;
            int endY = startY + foundation.Components.Height - 2;

            if (newX >= startX && newY >= startY && newX < endX && newY < endY && Map == foundation.Map)
            {
                if (Z != newZ)
                {
                    Location = new Point3D(X, Y, newZ);
                }

                m_NoRecursion = false;
                return;
            }

            Location = new Point3D(foundation.X, foundation.Y, newZ);
            Map = foundation.Map;

            m_NoRecursion = false;
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (m is BaseCreature && !((BaseCreature)m).Controlled)
            {
                return (!Alive || !m.Alive || IsDeadBondedPet || m.IsDeadBondedPet) || (Hidden && IsStaff());
            }

            return base.OnMoveOver(m);
        }

        public override bool CheckShove(Mobile shoved)
        {
            if (TransformationSpellHelper.UnderTransformation(shoved, typeof(WraithFormSpell)))
            {
                return true;
            }

            return base.CheckShove(shoved);
        }

        protected override void OnMapChange(Map oldMap)
        {
            ViceVsVirtueSystem.OnMapChange(this);

            if (NetState != null && NetState.IsEnhancedClient)
            {
                Waypoints.OnMapChange(this, oldMap);
            }

            if ((Map != Faction.Facet && oldMap == Faction.Facet) || (Map == Faction.Facet && oldMap != Faction.Facet))
            {
                InvalidateProperties();
            }

            BaseGump.CheckCloseGumps(this);

            DesignContext context = DesignContext;

            if (context == null || m_NoRecursion)
            {
                return;
            }

            m_NoRecursion = true;

            HouseFoundation foundation = context.Foundation;

            if (Map != foundation.Map)
            {
                Map = foundation.Map;
            }

            m_NoRecursion = false;
        }

        public override void OnBeneficialAction(Mobile target, bool isCriminal)
        {
            if (SentHonorContext != null)
            {
                SentHonorContext.OnSourceBeneficialAction(target);
            }

            if (Siege.SiegeShard && isCriminal)
            {
                Criminal = true;
                return;
            }

            base.OnBeneficialAction(target, isCriminal);
        }

        public override bool IsBeneficialCriminal(Mobile target)
        {
            if (!target.Criminal && target is BaseCreature && ((BaseCreature)target).GetMaster() == this)
                return false;

            return base.IsBeneficialCriminal(target);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            int disruptThreshold;

            if (!Core.AOS)
            {
                disruptThreshold = 0;
            }
            else if (from != null && from.Player)
            {
                disruptThreshold = 19;
            }
            else
            {
                disruptThreshold = 26;
            }

            if (Core.SA)
            {
                disruptThreshold += Dex / 12;
            }

            if (amount > disruptThreshold)
            {
                BandageContext c = BandageContext.GetContext(this);

                if (c != null)
                {
                    c.Slip();
                }
            }

            if (Confidence.IsRegenerating(this))
            {
                Confidence.StopRegenerating(this);
            }

            if (ReceivedHonorContext != null)
            {
                ReceivedHonorContext.OnTargetDamaged(from, amount);
            }
            if (SentHonorContext != null)
            {
                SentHonorContext.OnSourceDamaged(from, amount);
            }

            if (willKill && from is PlayerMobile)
            {
                Timer.DelayCall(TimeSpan.FromSeconds(10), ((PlayerMobile)@from).RecoverAmmo);
            }

            #region Mondain's Legacy
            if (InvisibilityPotion.HasTimer(this))
            {
                InvisibilityPotion.Iterrupt(this);
            }
            #endregion

            UndertakersStaff.TryRemoveTimer(this);

            base.OnDamage(amount, from, willKill);
        }

        public override void Resurrect()
        {
            bool wasAlive = Alive;

            base.Resurrect();

            if (Alive && !wasAlive)
            {
                Item deathRobe = new DeathRobe();

                if (!EquipItem(deathRobe))
                {
                    deathRobe.Delete();
                }

                if (NetState != null /*&& NetState.IsEnhancedClient*/)
                {
                    Waypoints.RemoveHealers(this, Map);
                }

                #region Scroll of Alacrity
                if (AcceleratedStart > DateTime.UtcNow)
                {
                    BuffInfo.AddBuff(this, new BuffInfo(BuffIcon.ArcaneEmpowerment, 1078511, 1078512, AcceleratedSkill.ToString()));
                }
                #endregion
            }
        }

        public override double RacialSkillBonus
        {
            get
            {
                if (Core.ML && Race == Race.Human)
                {
                    return 20.0;
                }

                return 0;
            }
        }

        public override double GetRacialSkillBonus(SkillName skill)
        {
            if (Core.ML && Race == Race.Human)
                return 20.0;

            if (Core.SA && Race == Race.Gargoyle)
            {
                if (skill == SkillName.Imbuing)
                    return 30.0;

                if (skill == SkillName.Throwing)
                    return 20.0;
            }

            return RacialSkillBonus;
        }

        public override void OnWarmodeChanged()
        {
            if (!Warmode)
            {
                Timer.DelayCall(TimeSpan.FromSeconds(10), RecoverAmmo);
            }
        }

        private Mobile m_InsuranceAward;
        private int m_InsuranceCost;
        private int m_InsuranceBonus;

        public List<Item> EquipSnapshot { get; private set; }

        private bool FindItems_Callback(Item item)
        {
            if (!item.Deleted && (item.LootType == LootType.Blessed || item.Insured))
            {
                if (Backpack != item.Parent)
                {
                    return true;
                }
            }
            return false;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override bool Criminal
        {
            get => base.Criminal;
            set
            {
                bool crim = base.Criminal;
                base.Criminal = value;

                if (value != crim)
                {
                    if (value)
                        BuffInfo.AddBuff(this, new BuffInfo(BuffIcon.CriminalStatus, 1153802, 1153828));
                    else
                        BuffInfo.RemoveBuff(this, BuffIcon.CriminalStatus);
                }
            }
        }

        public override bool OnBeforeDeath()
        {
            NetState state = NetState;

            if (state != null)
            {
                state.CancelAllTrades();
            }

            if (Criminal)
                BuffInfo.RemoveBuff(this, BuffIcon.CriminalStatus);

            DropHolding();

            if (Core.AOS && Backpack != null && !Backpack.Deleted)
            {
                var ilist = Backpack.FindItemsByType<Item>(FindItems_Callback);

                for (int i = 0; i < ilist.Count; i++)
                {
                    Backpack.AddItem(ilist[i]);
                }
            }

            EquipSnapshot = new List<Item>(Items);

            m_NonAutoreinsuredItems = 0;
            m_InsuranceCost = 0;
            m_InsuranceAward = base.FindMostRecentDamager(false);

            if (m_InsuranceAward is BaseCreature)
            {
                Mobile master = ((BaseCreature)m_InsuranceAward).GetMaster();

                if (master != null)
                {
                    m_InsuranceAward = master;
                }
            }

            if (m_InsuranceAward != null && (!m_InsuranceAward.Player || m_InsuranceAward == this))
            {
                m_InsuranceAward = null;
            }

            if (m_InsuranceAward is PlayerMobile)
            {
                ((PlayerMobile)m_InsuranceAward).m_InsuranceBonus = 0;
            }

            if (ReceivedHonorContext != null)
            {
                ReceivedHonorContext.OnTargetKilled();
            }

            if (SentHonorContext != null)
            {
                SentHonorContext.OnSourceKilled();
            }

            RecoverAmmo();

            if (NetState != null && NetState.IsEnhancedClient)
            {
                Waypoints.AddCorpse(this);
            }

            return base.OnBeforeDeath();
        }

        private bool CheckInsuranceOnDeath(Item item)
        {
            if (Young)
                return false;

            if (InsuranceEnabled && item.Insured)
            {
                int insuredAmount = GetInsuranceCost(item);

                if (AutoRenewInsurance)
                {
                    int cost = (m_InsuranceAward == null ? insuredAmount : insuredAmount / 2);

                    if (Banker.Withdraw(this, cost))
                    {
                        m_InsuranceCost += cost;
                        item.PayedInsurance = true;
                        SendLocalizedMessage(1060398, cost.ToString()); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
                    }
                    else
                    {
                        SendLocalizedMessage(1061079, "", 0x23); // You lack the funds to purchase the insurance
                        item.PayedInsurance = false;
                        item.Insured = false;
                        m_NonAutoreinsuredItems++;
                    }
                }
                else
                {
                    item.PayedInsurance = false;
                    item.Insured = false;
                }

                if (m_InsuranceAward != null)
                {
                    if (Banker.Deposit(m_InsuranceAward, insuredAmount / 2))
                    {
                        if (m_InsuranceAward is PlayerMobile)
                        {
                            ((PlayerMobile)m_InsuranceAward).m_InsuranceBonus += insuredAmount / 2;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public override DeathMoveResult GetParentMoveResultFor(Item item)
        {
            if (CheckInsuranceOnDeath(item) && !Young)
            {
                return DeathMoveResult.MoveToBackpack;
            }

            DeathMoveResult res = base.GetParentMoveResultFor(item);

            if (res == DeathMoveResult.MoveToCorpse && item.Movable && Young)
            {
                res = DeathMoveResult.MoveToBackpack;
            }

            return res;
        }

        public override DeathMoveResult GetInventoryMoveResultFor(Item item)
        {
            if (CheckInsuranceOnDeath(item) && !Young)
            {
                return DeathMoveResult.MoveToBackpack;
            }

            DeathMoveResult res = base.GetInventoryMoveResultFor(item);

            if (res == DeathMoveResult.MoveToCorpse && item.Movable && Young)
            {
                res = DeathMoveResult.MoveToBackpack;
            }

            return res;
        }

        public override void OnDeath(Container c)
        {
            if (NetState != null /*&& NetState.IsEnhancedClient*/)
            {
                Waypoints.OnDeath(this);
            }

            Mobile m = FindMostRecentDamager(false);
            PlayerMobile killer = m as PlayerMobile;

            if (killer == null && m is BaseCreature)
            {
                killer = ((BaseCreature)m).GetMaster() as PlayerMobile;
            }

            if (m_NonAutoreinsuredItems > 0)
            {
                SendLocalizedMessage(1061115);
            }

            base.OnDeath(c);

            EquipSnapshot = null;

            HueMod = -1;
            NameMod = null;
            SavagePaintExpiration = TimeSpan.Zero;

            SetHairMods(-1, -1);

            PolymorphSpell.StopTimer(this);
            IncognitoSpell.StopTimer(this);
            DisguiseTimers.RemoveTimer(this);

            WeakenSpell.RemoveEffects(this);
            ClumsySpell.RemoveEffects(this);
            FeeblemindSpell.RemoveEffects(this);
            CurseSpell.RemoveEffect(this);
            Spells.Second.ProtectionSpell.EndProtection(this);


            EndAction(typeof(PolymorphSpell));
            EndAction(typeof(IncognitoSpell));

            MeerMage.StopEffect(this, false);

            BaseEscort.DeleteEscort(this);

            #region Stygian Abyss
            if (Flying)
            {
                Flying = false;
                BuffInfo.RemoveBuff(this, BuffIcon.Fly);
            }
            #endregion

            StolenItem.ReturnOnDeath(this, c);

            if (PermaFlags.Count > 0)
            {
                PermaFlags.Clear();

                if (c is Corpse)
                {
                    ((Corpse)c).Criminal = true;
                }

                if (Stealing.ClassicMode)
                {
                    Criminal = true;
                }
            }

            if (killer != null && Murderer && DateTime.UtcNow >= killer.m_NextJustAward)
            {
                // This scales 700.0 skill points to 1000 valor points
                int pointsToGain = SkillsTotal / 7;

                // This scales 700.0 skill points to 7 minutes wait
                int minutesToWait = Math.Max(1, SkillsTotal / 1000);

                bool gainedPath = false;

                if (VirtueHelper.Award(m, VirtueName.Justice, pointsToGain, ref gainedPath))
                {
                    if (gainedPath)
                    {
                        m.SendLocalizedMessage(1049367); // You have gained a path in Justice!
                    }
                    else
                    {
                        m.SendLocalizedMessage(1049363); // You have gained in Justice.
                    }

                    m.FixedParticles(0x375A, 9, 20, 5027, EffectLayer.Waist);
                    m.PlaySound(0x1F7);

                    killer.m_NextJustAward = DateTime.UtcNow + TimeSpan.FromMinutes(minutesToWait);
                }
            }

            if (m_InsuranceAward is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)m_InsuranceAward;

                if (pm.m_InsuranceBonus > 0)
                {
                    pm.SendLocalizedMessage(1060397, pm.m_InsuranceBonus.ToString());
                    // ~1_AMOUNT~ gold has been deposited into your bank box.
                }
            }

            if (Young)
            {
                if (YoungDeathTeleport())
                {
                    Timer.DelayCall(TimeSpan.FromSeconds(2.5), SendYoungDeathNotice);
                }
            }

            Faction.HandleDeath(this, killer);

            Guilds.Guild.HandleDeath(this, killer);

            if (m_BuffTable != null)
            {
                var list = new List<BuffInfo>();

                foreach (BuffInfo buff in m_BuffTable.Values)
                {
                    if (!buff.RetainThroughDeath)
                    {
                        list.Add(buff);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    RemoveBuff(list[i]);
                }
            }

            #region Stygian Abyss
            if (Region.IsPartOf("Abyss") && SSSeedExpire > DateTime.UtcNow)
            {
                SendGump(new ResurrectGump(this, ResurrectMessage.SilverSapling));
            }

            if (LastKiller is BaseVoidCreature)
                ((BaseVoidCreature)LastKiller).Mutate(VoidEvolution.Killing);
            #endregion
        }

        #region Stuck Menu
        private DateTime[] m_StuckMenuUses;

        public bool CanUseStuckMenu()
        {
            if (m_StuckMenuUses == null)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < m_StuckMenuUses.Length; ++i)
                {
                    if ((DateTime.UtcNow - m_StuckMenuUses[i]) > TimeSpan.FromDays(1.0))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public void UsedStuckMenu()
        {
            if (m_StuckMenuUses == null)
            {
                m_StuckMenuUses = new DateTime[2];
            }

            for (int i = 0; i < m_StuckMenuUses.Length; ++i)
            {
                if ((DateTime.UtcNow - m_StuckMenuUses[i]) > TimeSpan.FromDays(1.0))
                {
                    m_StuckMenuUses[i] = DateTime.UtcNow;
                    return;
                }
            }
        }
        #endregion

        private readonly Hashtable m_AntiMacroTable;
        private TimeSpan m_GameTime;
        private TimeSpan m_ShortTermElapse;
        private TimeSpan m_LongTermElapse;
        private DateTime m_SavagePaintExpiration;

        public SkillName Learning { get; set; } = (SkillName)(-1);

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan SavagePaintExpiration
        {
            get
            {
                TimeSpan ts = m_SavagePaintExpiration - DateTime.UtcNow;

                if (ts < TimeSpan.Zero)
                {
                    ts = TimeSpan.Zero;
                }

                return ts;
            }
            set => m_SavagePaintExpiration = DateTime.UtcNow + value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextSmithBulkOrder
        {
            get => BulkOrderSystem.GetNextBulkOrder(BODType.Smith, this);
            set => BulkOrderSystem.SetNextBulkOrder(BODType.Smith, this, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextTailorBulkOrder
        {
            get => BulkOrderSystem.GetNextBulkOrder(BODType.Tailor, this);
            set => BulkOrderSystem.SetNextBulkOrder(BODType.Tailor, this, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextAlchemyBulkOrder
        {
            get => BulkOrderSystem.GetNextBulkOrder(BODType.Alchemy, this);
            set => BulkOrderSystem.SetNextBulkOrder(BODType.Alchemy, this, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextInscriptionBulkOrder
        {
            get => BulkOrderSystem.GetNextBulkOrder(BODType.Inscription, this);
            set => BulkOrderSystem.SetNextBulkOrder(BODType.Inscription, this, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextTinkeringBulkOrder
        {
            get => BulkOrderSystem.GetNextBulkOrder(BODType.Tinkering, this);
            set => BulkOrderSystem.SetNextBulkOrder(BODType.Tinkering, this, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextFletchingBulkOrder
        {
            get => BulkOrderSystem.GetNextBulkOrder(BODType.Fletching, this);
            set => BulkOrderSystem.SetNextBulkOrder(BODType.Fletching, this, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextCarpentryBulkOrder
        {
            get => BulkOrderSystem.GetNextBulkOrder(BODType.Carpentry, this);
            set => BulkOrderSystem.SetNextBulkOrder(BODType.Carpentry, this, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextCookingBulkOrder
        {
            get => BulkOrderSystem.GetNextBulkOrder(BODType.Cooking, this);
            set => BulkOrderSystem.SetNextBulkOrder(BODType.Cooking, this, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastEscortTime { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastPetBallTime { get; set; }

        public PlayerMobile()
        {
            Instances.Add(this);

            AutoStabled = new List<Mobile>();

            #region Mondain's Legacy
            //m_Quests = new List<BaseQuest>();
            //m_Chains = new Dictionary<QuestChain, BaseChain>();
            DoneQuests = new List<QuestRestartInfo>();
            Collections = new Dictionary<Collection, int>();
            RewardTitles = new List<object>();

            PeacedUntil = DateTime.UtcNow;
            #endregion

            VisibilityList = new List<Mobile>();
            PermaFlags = new List<Mobile>();
            m_AntiMacroTable = new Hashtable();
            RecentlyReported = new List<Mobile>();

            //m_BOBFilter = new BOBFilter();

            m_GameTime = TimeSpan.Zero;
            m_ShortTermElapse = TimeSpan.FromHours(8.0);
            m_LongTermElapse = TimeSpan.FromHours(40.0);

            JusticeProtectors = new List<Mobile>();
            m_GuildRank = RankDefinition.Lowest;

            m_ChampionTitles = new ChampionTitleInfo();
        }

        public override bool MutateSpeech(List<Mobile> hears, ref string text, ref object context)
        {
            if (Alive)
            {
                return false;
            }

            if (Core.ML && Skills[SkillName.SpiritSpeak].Value >= 100.0)
            {
                return false;
            }

            if (Core.AOS)
            {
                for (int i = 0; i < hears.Count; ++i)
                {
                    Mobile m = hears[i];

                    if (m != this && m.Skills[SkillName.SpiritSpeak].Value >= 100.0)
                    {
                        return false;
                    }
                }
            }

            return base.MutateSpeech(hears, ref text, ref context);
        }

        public override void DoSpeech(string text, int[] keywords, MessageType type, int hue)
        {
            if (Guilds.Guild.NewGuildSystem && (type == MessageType.Guild || type == MessageType.Alliance))
            {
                Guild g = Guild as Guild;
                if (g == null)
                {
                    SendLocalizedMessage(1063142); // You are not in a guild!
                }
                else if (type == MessageType.Alliance)
                {
                    if (g.Alliance != null && g.Alliance.IsMember(g))
                    {
                        //g.Alliance.AllianceTextMessage( hue, "[Alliance][{0}]: {1}", Name, text );
                        g.Alliance.AllianceChat(this, text);
                        SendToStaffMessage(this, "[Alliance]: {0}", text);

                        AllianceMessageHue = hue;
                    }
                    else
                    {
                        SendLocalizedMessage(1071020); // You are not in an alliance!
                    }
                }
                else //Type == MessageType.Guild
                {
                    GuildMessageHue = hue;

                    g.GuildChat(this, text);
                    SendToStaffMessage(this, "[Guild]: {0}", text);
                }
            }
            else
            {
                base.DoSpeech(text, keywords, type, hue);
            }
        }

        private static void SendToStaffMessage(Mobile from, string text)
        {
            Packet p = null;

            foreach (NetState ns in from.GetClientsInRange(8))
            {
                Mobile mob = ns.Mobile;

                if (mob != null && mob.AccessLevel >= AccessLevel.GameMaster && mob.AccessLevel > from.AccessLevel)
                {
                    if (p == null)
                    {
                        p =
                            Packet.Acquire(
                                new UnicodeMessage(
                                    from.Serial, from.Body, MessageType.Regular, from.SpeechHue, 3, from.Language, from.Name, text));
                    }

                    ns.Send(p);
                }
            }

            Packet.Release(p);
        }

        private static void SendToStaffMessage(Mobile from, string format, params object[] args)
        {
            SendToStaffMessage(from, string.Format(format, args));
        }

        #region Poison
        public override void OnCured(Mobile from, Poison oldPoison)
        {
            BuffInfo.RemoveBuff(this, BuffIcon.Poison);
        }

        public override ApplyPoisonResult ApplyPoison(Mobile from, Poison poison)
        {
            if (!Alive || poison == null)
            {
                return ApplyPoisonResult.Immune;
            }

            //Skill Masteries
            if (Spells.SkillMasteries.ResilienceSpell.UnderEffects(this) && 0.25 > Utility.RandomDouble())
            {
                return ApplyPoisonResult.Immune;
            }

            if (EvilOmenSpell.TryEndEffect(this))
            {
                poison = PoisonImpl.IncreaseLevel(poison);
            }

            //Skill Masteries
            if ((Poison == null || Poison.Level < poison.Level) && ToleranceSpell.OnPoisonApplied(this))
            {
                poison = PoisonImpl.DecreaseLevel(poison);

                if (poison == null || poison.Level <= 0)
                {
                    PrivateOverheadMessage(MessageType.Regular, 0x3F, 1053092, NetState); // * You feel yourself resisting the effects of the poison *
                    return ApplyPoisonResult.Immune;
                }
            }

            ApplyPoisonResult result = base.ApplyPoison(from, poison);

            if (from != null && result == ApplyPoisonResult.Poisoned && PoisonTimer is PoisonImpl.PoisonTimer)
            {
                (PoisonTimer as PoisonImpl.PoisonTimer).From = from;
            }

            return result;
        }

        public override bool CheckPoisonImmunity(Mobile from, Poison poison)
        {
            if (Young)
            {
                return true;
            }

            return base.CheckPoisonImmunity(from, poison);
        }

        public override void OnPoisonImmunity(Mobile from, Poison poison)
        {
            if (Young)
            {
                SendLocalizedMessage(502808);
                // You would have been poisoned, were you not new to the land of Britannia. Be careful in the future.
            }
            else
            {
                base.OnPoisonImmunity(from, poison);
            }
        }
        #endregion

        public PlayerMobile(Serial s)
            : base(s)
        {
            Instances.Add(this);

            VisibilityList = new List<Mobile>();
            m_AntiMacroTable = new Hashtable();
        }

        public List<Mobile> VisibilityList { get; }

        public List<Mobile> PermaFlags { get; private set; }

        public override int Luck => AosAttributes.GetValue(this, AosAttribute.Luck) + TenthAnniversarySculpture.GetLuckBonus(this);

        public int RealLuck
        {
            get
            {
                int facetBonus = !Siege.SiegeShard && Map == Map.Felucca ? RandomItemGenerator.FeluccaLuckBonus : 0;

                return Luck + FountainOfFortune.GetLuckBonus(this) + facetBonus;
            }
        }

        public override bool IsHarmfulCriminal(IDamageable damageable)
        {
            Mobile target = damageable as Mobile;

            if (Stealing.ClassicMode && target is PlayerMobile && ((PlayerMobile)target).PermaFlags.Count > 0)
            {
                int noto = Notoriety.Compute(this, target);

                if (noto == Notoriety.Innocent)
                {
                    target.Delta(MobileDelta.Noto);
                }

                return false;
            }

            if (target is BaseCreature && ((BaseCreature)target).InitialInnocent && !((BaseCreature)target).Controlled)
            {
                return false;
            }

            if (Core.ML && target is BaseCreature && ((BaseCreature)target).Controlled && ((BaseCreature)target).ControlMaster == this)
            {
                return false;
            }

            if (target is BaseCreature && ((BaseCreature)target).Summoned && ((BaseCreature)target).SummonMaster == this)
            {
                return false;
            }

            return base.IsHarmfulCriminal(damageable);
        }

        public bool AntiMacroCheck(Skill skill, object obj)
        {
            if (obj == null || m_AntiMacroTable == null || IsStaff())
            {
                return true;
            }

            Hashtable tbl = (Hashtable)m_AntiMacroTable[skill];
            if (tbl == null)
            {
                m_AntiMacroTable[skill] = tbl = new Hashtable();
            }

            CountAndTimeStamp count = (CountAndTimeStamp)tbl[obj];
            if (count != null)
            {
                if (count.TimeStamp + SkillCheck.AntiMacroExpire <= DateTime.UtcNow)
                {
                    count.Count = 1;
                    return true;
                }
                else
                {
                    ++count.Count;
                    if (count.Count <= SkillCheck.Allowance)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                tbl[obj] = count = new CountAndTimeStamp();
                count.Count = 1;

                return true;
            }
        }

        private void RevertHair()
        {
            SetHairMods(-1, -1);
        }

        public BOBFilter BOBFilter => BulkOrderSystem.GetBOBFilter(this);

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 41:
                    {
                        if (reader.ReadBool())
                        {
                            m_StuckMenuUses = new DateTime[reader.ReadInt()];

                            for (int i = 0; i < m_StuckMenuUses.Length; ++i)
                            {
                                m_StuckMenuUses[i] = reader.ReadDateTime();
                            }
                        }
                        else
                        {
                            m_StuckMenuUses = null;
                        }

                        goto case 40;
                    }
                case 40: // Version 40, moved gauntlet points, virtua artys and TOT turn ins to PointsSystem
                case 39: // Version 39, removed ML quest save/load
                case 38:
                    NextGemOfSalvationUse = reader.ReadDateTime();
                    goto case 37;
                case 37:
                    ExtendedFlags = (ExtendedPlayerFlag)reader.ReadInt();
                    goto case 36;
                case 36:
                    RewardStableSlots = reader.ReadInt();
                    goto case 35;
                case 35: // Siege Blessed Item
                    BlessedItem = reader.ReadItem();
                    goto case 34;
                // Version 34 - new BOD System
                case 34:
                case 33:
                    {
                        ExploringTheDeepQuest = (ExploringTheDeepQuestChain)reader.ReadInt();
                        goto case 31;
                    }
                case 32:
                case 31:
                    {
                        DisplayGuildTitle = version > 31 && reader.ReadBool();
                        m_FameKarmaTitle = reader.ReadString();
                        m_PaperdollSkillTitle = reader.ReadString();
                        m_OverheadTitle = reader.ReadString();
                        m_SubtitleSkillTitle = reader.ReadString();

                        m_CurrentChampTitle = reader.ReadString();
                        m_CurrentVeteranTitle = reader.ReadInt();
                        goto case 30;
                    }
                case 30: goto case 29;
                case 29:
                    {
                        if (version < 40)
                        {
                            PointsSystem.DoomGauntlet.SetPoints(this, reader.ReadDouble());
                        }

                        SSNextSeed = reader.ReadDateTime();
                        SSSeedExpire = reader.ReadDateTime();
                        m_SSSeedLocation = reader.ReadPoint3D();
                        SSSeedMap = reader.ReadMap();

                        if (version < 30)
                        {
                            reader.ReadLong(); // Old m_LevelExp
                            int points = (int)reader.ReadLong();
                            if (points > 0)
                            {
                                Server.Engines.Points.PointsSystem.QueensLoyalty.ConvertFromOldSystem(this, points);
                            }

                            reader.ReadInt(); // Old m_Level
                            reader.ReadString(); // Old m_ExpTitle
                        }

                        if (version < 40)
                        {
                            PointsSystem.VirtueArtifacts.SetPoints(this, reader.ReadInt());
                        }

                        if (version < 39)
                        {
                            List<BaseQuest> quests = QuestReader.Quests(reader, this);
                            Dictionary<QuestChain, BaseChain> dic = QuestReader.Chains(reader);

                            if (quests != null && quests.Count > 0)
                                MondainQuestData.QuestData[this] = quests;

                            if (dic != null && dic.Count > 0)
                                MondainQuestData.ChainData[this] = dic;
                        }

                        Collections = new Dictionary<Collection, int>();
                        RewardTitles = new List<object>();

                        for (int i = reader.ReadInt(); i > 0; i--)
                        {
                            Collections.Add((Collection)reader.ReadInt(), reader.ReadInt());
                        }

                        for (int i = reader.ReadInt(); i > 0; i--)
                        {
                            RewardTitles.Add(QuestReader.Object(reader));
                        }

                        SelectedTitle = reader.ReadInt();

                        goto case 28;
                    }
                case 28:
                    {
                        PeacedUntil = reader.ReadDateTime();

                        goto case 27;
                    }
                case 27:
                    {
                        AnkhNextUse = reader.ReadDateTime();

                        goto case 26;
                    }
                case 26:
                    {
                        AutoStabled = reader.ReadStrongMobileList();

                        goto case 25;
                    }
                case 25:
                    {
                        int recipeCount = reader.ReadInt();

                        if (recipeCount > 0)
                        {
                            m_AcquiredRecipes = new Dictionary<int, bool>();

                            for (int i = 0; i < recipeCount; i++)
                            {
                                int r = reader.ReadInt();
                                if (reader.ReadBool()) //Don't add in recipies which we haven't gotten or have been removed
                                {
                                    m_AcquiredRecipes.Add(r, true);
                                }
                            }
                        }
                        goto case 24;
                    }
                case 24:
                    {
                        LastHonorLoss = reader.ReadDeltaTime();
                        goto case 23;
                    }
                case 23:
                    {
                        m_ChampionTitles = new ChampionTitleInfo(reader);
                        goto case 22;
                    }
                case 22:
                    {
                        LastValorLoss = reader.ReadDateTime();
                        goto case 21;
                    }
                case 21:
                    {
                        if (version < 40)
                        {
                            PointsSystem.TreasuresOfTokuno.Convert(this, reader.ReadEncodedInt(), reader.ReadInt());
                        }
                        goto case 20;
                    }
                case 20:
                    {
                        AllianceMessageHue = reader.ReadEncodedInt();
                        GuildMessageHue = reader.ReadEncodedInt();

                        goto case 19;
                    }
                case 19:
                    {
                        int rank = reader.ReadEncodedInt();
                        int maxRank = RankDefinition.Ranks.Length - 1;
                        if (rank > maxRank)
                        {
                            rank = maxRank;
                        }

                        m_GuildRank = RankDefinition.Ranks[rank];
                        LastOnline = reader.ReadDateTime();
                        goto case 18;
                    }
                case 18:
                    {
                        SolenFriendship = (SolenFriendship)reader.ReadEncodedInt();

                        goto case 17;
                    }
                case 17: // changed how DoneQuests is serialized
                case 16:
                    {
                        Quest = QuestSerializer.DeserializeQuest(reader);

                        if (Quest != null)
                        {
                            Quest.From = this;
                        }

                        int count = reader.ReadEncodedInt();

                        if (count > 0)
                        {
                            DoneQuests = new List<QuestRestartInfo>();

                            for (int i = 0; i < count; ++i)
                            {
                                Type questType = QuestSerializer.ReadType(QuestSystem.QuestTypes, reader);
                                DateTime restartTime;

                                if (version < 17)
                                {
                                    restartTime = DateTime.MaxValue;
                                }
                                else
                                {
                                    restartTime = reader.ReadDateTime();
                                }

                                DoneQuests.Add(new QuestRestartInfo(questType, restartTime));
                            }
                        }

                        Profession = reader.ReadEncodedInt();
                        goto case 15;
                    }
                case 15:
                    {
                        LastCompassionLoss = reader.ReadDeltaTime();
                        goto case 14;
                    }
                case 14:
                    {
                        CompassionGains = reader.ReadEncodedInt();

                        if (CompassionGains > 0)
                        {
                            NextCompassionDay = reader.ReadDeltaTime();
                        }

                        goto case 13;
                    }
                case 13: // just removed m_PayedInsurance list
                case 12:
                    {
                        if (version < 34)
                            BulkOrderSystem.SetBOBFilter(this, new BOBFilter(reader));
                        goto case 11;
                    }
                case 11:
                    {
                        if (version < 13)
                        {
                            var payed = reader.ReadStrongItemList();

                            for (int i = 0; i < payed.Count; ++i)
                            {
                                payed[i].PayedInsurance = true;
                            }
                        }

                        goto case 10;
                    }
                case 10:
                    {
                        if (reader.ReadBool())
                        {
                            m_HairModID = reader.ReadInt();
                            m_HairModHue = reader.ReadInt();
                            m_BeardModID = reader.ReadInt();
                            m_BeardModHue = reader.ReadInt();
                        }

                        goto case 9;
                    }
                case 9:
                    {
                        SavagePaintExpiration = reader.ReadTimeSpan();

                        if (SavagePaintExpiration > TimeSpan.Zero)
                        {
                            BodyMod = (Female ? 184 : 183);
                            HueMod = 0;
                        }

                        goto case 8;
                    }
                case 8:
                    {
                        NpcGuild = (NpcGuild)reader.ReadInt();
                        NpcGuildJoinTime = reader.ReadDateTime();
                        m_NpcGuildGameTime = reader.ReadTimeSpan();
                        goto case 7;
                    }
                case 7:
                    {
                        PermaFlags = reader.ReadStrongMobileList();
                        goto case 6;
                    }
                case 6:
                    {
                        if (version < 34)
                            reader.ReadTimeSpan();
                        goto case 5;
                    }
                case 5:
                    {
                        if (version < 34)
                            reader.ReadTimeSpan();
                        goto case 4;
                    }
                case 4:
                    {
                        LastJusticeLoss = reader.ReadDeltaTime();
                        JusticeProtectors = reader.ReadStrongMobileList();
                        goto case 3;
                    }
                case 3:
                    {
                        LastSacrificeGain = reader.ReadDeltaTime();
                        LastSacrificeLoss = reader.ReadDeltaTime();
                        AvailableResurrects = reader.ReadInt();
                        goto case 2;
                    }
                case 2:
                    {
                        Flags = (PlayerFlag)reader.ReadInt();
                        goto case 1;
                    }
                case 1:
                    {
                        m_LongTermElapse = reader.ReadTimeSpan();
                        m_ShortTermElapse = reader.ReadTimeSpan();
                        m_GameTime = reader.ReadTimeSpan();
                        goto case 0;
                    }
                case 0:
                    {
                        if (version < 26)
                        {
                            AutoStabled = new List<Mobile>();
                        }
                        break;
                    }
            }

            if (version < 29)
            {
                SSNextSeed = SSSeedExpire = DateTime.UtcNow;
                m_SSSeedLocation = Point3D.Zero;
            }

            if (RecentlyReported == null)
            {
                RecentlyReported = new List<Mobile>();
            }

            #region Mondain's Legacy
            /*if (m_Quests == null)
			{
				m_Quests = new List<BaseQuest>();
			}

			if (m_Chains == null)
			{
				m_Chains = new Dictionary<QuestChain, BaseChain>();
			}*/

            if (DoneQuests == null)
            {
                DoneQuests = new List<QuestRestartInfo>();
            }

            if (Collections == null)
            {
                Collections = new Dictionary<Collection, int>();
            }

            if (RewardTitles == null)
            {
                RewardTitles = new List<object>();
            }
            #endregion

            // Professions weren't verified on 1.0 RC0
            if (!CharacterCreation.VerifyProfession(Profession))
            {
                Profession = 0;
            }

            if (PermaFlags == null)
            {
                PermaFlags = new List<Mobile>();
            }

            if (JusticeProtectors == null)
            {
                JusticeProtectors = new List<Mobile>();
            }

            if (m_GuildRank == null)
            {
                m_GuildRank = RankDefinition.Member;
                //Default to member if going from older version to new version (only time it should be null)
            }

            if (LastOnline == DateTime.MinValue && Account != null)
            {
                LastOnline = ((Account)Account).LastLogin;
            }

            if (m_ChampionTitles == null)
            {
                m_ChampionTitles = new ChampionTitleInfo();
            }

            var list = Stabled;

            for (int i = 0; i < list.Count; ++i)
            {
                BaseCreature bc = list[i] as BaseCreature;

                if (bc != null)
                {
                    bc.IsStabled = true;
                    bc.StabledBy = this;
                }
            }

            CheckAtrophies(this);

            if (Hidden) //Hiding is the only buff where it has an effect that's serialized.
            {
                AddBuff(new BuffInfo(BuffIcon.HidingAndOrStealth, 1075655));
            }

            if (BlessedItem != null)
            {
                Timer.DelayCall(
                b =>
                {
                    if (BlessedItem == b && b.RootParent != this)
                    {
                        BlessedItem = null;
                    }
                },
                BlessedItem);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            //cleanup our anti-macro table
            foreach (Hashtable t in m_AntiMacroTable.Values)
            {
                ArrayList remove = new ArrayList();
                foreach (CountAndTimeStamp time in t.Values)
                {
                    if (time.TimeStamp + SkillCheck.AntiMacroExpire <= DateTime.UtcNow)
                    {
                        remove.Add(time);
                    }
                }

                for (int i = 0; i < remove.Count; ++i)
                {
                    t.Remove(remove[i]);
                }
            }

            CheckKillDecay();
            CheckAtrophies(this);

            base.Serialize(writer);

            writer.Write(41); // version

            if (m_StuckMenuUses != null)
            {
                writer.Write(true);

                writer.Write(m_StuckMenuUses.Length);

                for (int i = 0; i < m_StuckMenuUses.Length; ++i)
                {
                    writer.Write(m_StuckMenuUses[i]);
                }
            }
            else
            {
                writer.Write(false);
            }

            writer.Write(NextGemOfSalvationUse);

            writer.Write((int)ExtendedFlags);

            writer.Write(RewardStableSlots);

            if (BlessedItem != null && BlessedItem.RootParent != this)
            {
                BlessedItem = null;
            }

            writer.Write(BlessedItem);

            writer.Write((int)ExploringTheDeepQuest);

            // Version 31/32 Titles
            writer.Write(DisplayGuildTitle);
            writer.Write(m_FameKarmaTitle);
            writer.Write(m_PaperdollSkillTitle);
            writer.Write(m_OverheadTitle);
            writer.Write(m_SubtitleSkillTitle);
            writer.Write(m_CurrentChampTitle);
            writer.Write(m_CurrentVeteranTitle);

            // Version 30 open to take out old Queens Loyalty Info

            #region Plant System
            writer.Write(SSNextSeed);
            writer.Write(SSSeedExpire);
            writer.Write(m_SSSeedLocation);
            writer.Write(SSSeedMap);
            #endregion

            #region Mondain's Legacy

            if (Collections == null)
            {
                writer.Write(0);
            }
            else
            {
                writer.Write(Collections.Count);

                foreach (var pair in Collections)
                {
                    writer.Write((int)pair.Key);
                    writer.Write(pair.Value);
                }
            }

            if (RewardTitles == null)
            {
                writer.Write(0);
            }
            else
            {
                writer.Write(RewardTitles.Count);

                for (int i = 0; i < RewardTitles.Count; i++)
                {
                    QuestWriter.Object(writer, RewardTitles[i]);
                }
            }

            writer.Write(SelectedTitle);
            #endregion

            // Version 28
            writer.Write(PeacedUntil);
            writer.Write(AnkhNextUse);
            writer.Write(AutoStabled, true);

            if (m_AcquiredRecipes == null)
            {
                writer.Write(0);
            }
            else
            {
                writer.Write(m_AcquiredRecipes.Count);

                foreach (var kvp in m_AcquiredRecipes)
                {
                    writer.Write(kvp.Key);
                    writer.Write(kvp.Value);
                }
            }

            writer.WriteDeltaTime(LastHonorLoss);

            ChampionTitleInfo.Serialize(writer, m_ChampionTitles);

            writer.Write(LastValorLoss);

            writer.WriteEncodedInt(AllianceMessageHue);
            writer.WriteEncodedInt(GuildMessageHue);

            writer.WriteEncodedInt(m_GuildRank.Rank);
            writer.Write(LastOnline);

            writer.WriteEncodedInt((int)SolenFriendship);

            QuestSerializer.Serialize(Quest, writer);

            if (DoneQuests == null)
            {
                writer.WriteEncodedInt(0);
            }
            else
            {
                writer.WriteEncodedInt(DoneQuests.Count);

                for (int i = 0; i < DoneQuests.Count; ++i)
                {
                    QuestRestartInfo restartInfo = DoneQuests[i];

                    QuestSerializer.Write(restartInfo.QuestType, QuestSystem.QuestTypes, writer);
                    writer.Write(restartInfo.RestartTime);
                }
            }

            writer.WriteEncodedInt(Profession);

            writer.WriteDeltaTime(LastCompassionLoss);

            writer.WriteEncodedInt(CompassionGains);

            if (CompassionGains > 0)
            {
                writer.WriteDeltaTime(NextCompassionDay);
            }

            bool useMods = (m_HairModID != -1 || m_BeardModID != -1);

            writer.Write(useMods);

            if (useMods)
            {
                writer.Write(m_HairModID);
                writer.Write(m_HairModHue);
                writer.Write(m_BeardModID);
                writer.Write(m_BeardModHue);
            }

            writer.Write(SavagePaintExpiration);

            writer.Write((int)NpcGuild);
            writer.Write(NpcGuildJoinTime);
            writer.Write(m_NpcGuildGameTime);

            writer.Write(PermaFlags, true);

            writer.WriteDeltaTime(LastJusticeLoss);
            writer.Write(JusticeProtectors, true);

            writer.WriteDeltaTime(LastSacrificeGain);
            writer.WriteDeltaTime(LastSacrificeLoss);
            writer.Write(AvailableResurrects);

            writer.Write((int)Flags);

            writer.Write(m_LongTermElapse);
            writer.Write(m_ShortTermElapse);
            writer.Write(GameTime);
        }

        public static void CheckAtrophies(Mobile m)
        {
            SacrificeVirtue.CheckAtrophy(m);
            JusticeVirtue.CheckAtrophy(m);
            CompassionVirtue.CheckAtrophy(m);
            ValorVirtue.CheckAtrophy(m);

            if (m is PlayerMobile)
            {
                ChampionTitleInfo.CheckAtrophy((PlayerMobile)m);
            }
        }

        public void CheckKillDecay()
        {
            if (m_ShortTermElapse < GameTime)
            {
                m_ShortTermElapse += TimeSpan.FromHours(8);
                if (ShortTermMurders > 0)
                {
                    --ShortTermMurders;
                }
            }

            if (m_LongTermElapse < GameTime)
            {
                m_LongTermElapse += TimeSpan.FromHours(40);
                if (Kills > 0)
                {
                    --Kills;
                }
            }
        }

        public void ResetKillTime()
        {
            m_ShortTermElapse = GameTime + TimeSpan.FromHours(8);
            m_LongTermElapse = GameTime + TimeSpan.FromHours(40);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime SessionStart { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan GameTime
        {
            get
            {
                if (NetState != null)
                {
                    return m_GameTime + (DateTime.UtcNow - SessionStart);
                }
                else
                {
                    return m_GameTime;
                }
            }
        }

        public override bool CanSee(Mobile m)
        {
            if (m is IConditionalVisibility && !((IConditionalVisibility)m).CanBeSeenBy(this))
                return false;

            if (m is CharacterStatue)
            {
                ((CharacterStatue)m).OnRequestedAnimation(this);
            }

            if (m is PlayerMobile && ((PlayerMobile)m).VisibilityList.Contains(this))
            {
                return true;
            }

            return base.CanSee(m);
        }

        public override bool CanSee(Item item)
        {
            if (item is IConditionalVisibility && !((IConditionalVisibility)item).CanBeSeenBy(this))
                return false;

            if (DesignContext != null && DesignContext.Foundation.IsHiddenToCustomizer(this, item))
            {
                return false;
            }
            else if (AccessLevel == AccessLevel.Player)
            {
                Region r = item.GetRegion();

                if (r is BaseRegion && !((BaseRegion)r).CanSee(this, item))
                {
                    return false;
                }
            }

            return base.CanSee(item);
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            Instances.Remove(this);

            Faction faction = Faction.Find(this);

            if (faction != null)
            {
                faction.RemoveMember(this);
            }

            BaseHouse.HandleDeletion(this);

            DisguiseTimers.RemoveTimer(this);
        }

        public override bool NewGuildDisplay => Guilds.Guild.NewGuildSystem;

        public delegate void PlayerPropertiesEventHandler(PlayerPropertiesEventArgs e);

        public static event PlayerPropertiesEventHandler PlayerProperties;

        public class PlayerPropertiesEventArgs : EventArgs
        {
            public PlayerMobile Player = null;
            public ObjectPropertyList PropertyList = null;

            public PlayerPropertiesEventArgs(PlayerMobile player, ObjectPropertyList list)
            {
                Player = player;
                PropertyList = list;
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (Core.SA)
            {
                if (m_SubtitleSkillTitle != null)
                    list.Add(1042971, m_SubtitleSkillTitle);

                if (m_CurrentVeteranTitle > 0)
                    list.Add(m_CurrentVeteranTitle);
            }

            #region Mondain's Legacy Titles
            if (Core.ML && RewardTitles != null && SelectedTitle > -1)
            {
                if (SelectedTitle < RewardTitles.Count)
                {
                    if (RewardTitles[SelectedTitle] is int)
                    {
                        string cust = null;

                        if ((int)RewardTitles[SelectedTitle] == 1154017 && CityLoyaltySystem.HasCustomTitle(this, out cust))
                        {
                            list.Add(1154017, cust); // ~1_TITLE~ of ~2_CITY~
                        }
                        else
                            list.Add((int)RewardTitles[SelectedTitle]);
                    }
                    else if (RewardTitles[SelectedTitle] is string)
                    {
                        list.Add(1070722, (string)RewardTitles[SelectedTitle]);
                    }
                }
            }
            #endregion

            if (Map == Faction.Facet)
            {
                PlayerState pl = PlayerState.Find(this);

                if (pl != null)
                {
                    Faction faction = pl.Faction;

                    if (faction.Commander == this)
                    {
                        list.Add(1042733, faction.Definition.PropName); // Commanding Lord of the ~1_FACTION_NAME~
                    }
                    else if (pl.Sheriff != null)
                    {
                        list.Add(1042734, "{0}\t{1}", pl.Sheriff.Definition.FriendlyName, faction.Definition.PropName);
                        // The Sheriff of  ~1_CITY~, ~2_FACTION_NAME~
                    }
                    else if (pl.Finance != null)
                    {
                        list.Add(1042735, "{0}\t{1}", pl.Finance.Definition.FriendlyName, faction.Definition.PropName);
                        // The Finance Minister of ~1_CITY~, ~2_FACTION_NAME~
                    }
                    else if (pl.MerchantTitle != MerchantTitle.None)
                    {
                        list.Add(1060776, "{0}\t{1}", MerchantTitles.GetInfo(pl.MerchantTitle).Title, faction.Definition.PropName);
                        // ~1_val~, ~2_val~
                    }
                    else
                    {
                        list.Add(1060776, "{0}\t{1}", pl.Rank.Title, faction.Definition.PropName); // ~1_val~, ~2_val~
                    }
                }
            }

            if (Core.ML)
            {
                for (int i = AllFollowers.Count - 1; i >= 0; i--)
                {
                    BaseCreature c = AllFollowers[i] as BaseCreature;

                    if (c != null && c.ControlOrder == OrderType.Guard)
                    {
                        list.Add(501129); // guarded
                        break;
                    }
                }
            }

            if (TestCenter.Enabled && Core.TOL)
            {
                Server.Engines.VvV.VvVPlayerEntry entry = Server.Engines.Points.PointsSystem.ViceVsVirtue.GetPlayerEntry<Server.Engines.VvV.VvVPlayerEntry>(this);

                list.Add(string.Format("Kills: {0} / Deaths: {1} / Assists: {2}", // no cliloc for this!
                    entry == null ? "0" : entry.Kills.ToString(), entry == null ? "0" : entry.Deaths.ToString(), entry == null ? "0" : entry.Assists.ToString()));

                list.Add(1060415, AosAttributes.GetValue(this, AosAttribute.AttackChance).ToString()); // hit chance increase ~1_val~%
                list.Add(1060408, AosAttributes.GetValue(this, AosAttribute.DefendChance).ToString()); // defense chance increase ~1_val~%
                list.Add(1060486, AosAttributes.GetValue(this, AosAttribute.WeaponSpeed).ToString()); // swing speed increase ~1_val~%
                list.Add(1060401, AosAttributes.GetValue(this, AosAttribute.WeaponDamage).ToString()); // damage increase ~1_val~%
                list.Add(1060483, AosAttributes.GetValue(this, AosAttribute.SpellDamage).ToString()); // spell damage increase ~1_val~%
                list.Add(1060433, AosAttributes.GetValue(this, AosAttribute.LowerManaCost).ToString()); // lower mana cost
            }

            if (PlayerProperties != null)
            {
                PlayerProperties(new PlayerPropertiesEventArgs(this, list));
            }
        }

        public override void OnSingleClick(Mobile from)
        {
            if (Map == Faction.Facet)
            {
                PlayerState pl = PlayerState.Find(this);

                if (pl != null)
                {
                    string text;
                    bool ascii = false;

                    Faction faction = pl.Faction;

                    if (faction.Commander == this)
                    {
                        text = string.Concat(
                            Female ? "(Commanding Lady of the " : "(Commanding Lord of the ", faction.Definition.FriendlyName, ")");
                    }
                    else if (pl.Sheriff != null)
                    {
                        text = string.Concat(
                            "(The Sheriff of ", pl.Sheriff.Definition.FriendlyName, ", ", faction.Definition.FriendlyName, ")");
                    }
                    else if (pl.Finance != null)
                    {
                        text = string.Concat(
                            "(The Finance Minister of ", pl.Finance.Definition.FriendlyName, ", ", faction.Definition.FriendlyName, ")");
                    }
                    else
                    {
                        ascii = true;

                        if (pl.MerchantTitle != MerchantTitle.None)
                        {
                            text = string.Concat(
                                "(", MerchantTitles.GetInfo(pl.MerchantTitle).Title.String, ", ", faction.Definition.FriendlyName, ")");
                        }
                        else
                        {
                            text = string.Concat("(", pl.Rank.Title.String, ", ", faction.Definition.FriendlyName, ")");
                        }
                    }

                    int hue = (Faction.Find(from) == faction ? 98 : 38);

                    PrivateOverheadMessage(MessageType.Label, hue, ascii, text, from.NetState);
                }
            }

            base.OnSingleClick(from);
        }

        protected override bool OnMove(Direction d)
        {
            if (Party != null && NetState != null)
            {
                Waypoints.UpdateToParty(this);
            }

            if (!Core.SE)
            {
                return base.OnMove(d);
            }

            if (IsStaff())
            {
                return true;
            }

            if (Hidden && DesignContext.Find(this) == null) //Hidden & NOT customizing a house
            {
                if (!Mounted && Skills.Stealth.Value >= 25.0)
                {
                    bool running = (d & Direction.Running) != 0;

                    if (running)
                    {
                        if ((AllowedStealthSteps -= 2) <= 0)
                        {
                            RevealingAction();
                        }
                    }
                    else if (AllowedStealthSteps-- <= 0)
                    {
                        Stealth.OnUse(this);
                    }
                }
                else
                {
                    RevealingAction();
                }
            }

            #region Mondain's Legacy
            if (InvisibilityPotion.HasTimer(this))
            {
                InvisibilityPotion.Iterrupt(this);
            }
            #endregion

            return true;
        }

        public bool BedrollLogout { get; set; }
        public bool BlanketOfDarknessLogout { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public override bool Paralyzed
        {
            get => base.Paralyzed;
            set
            {
                base.Paralyzed = value;

                if (value)
                {
                    AddBuff(new BuffInfo(BuffIcon.Paralyze, 1075827)); //Paralyze/You are frozen and can not move
                }
                else
                {
                    RemoveBuff(BuffIcon.Paralyze);
                }
            }
        }

        #region Ethics

        [CommandProperty(AccessLevel.GameMaster)]
        public Player EthicPlayer { get; set; }
        #endregion

        #region Factions
        public PlayerState FactionPlayerState { get; set; }
        #endregion

        #region Quests

        public QuestSystem Quest { get; set; }

        public List<QuestRestartInfo> DoneQuests { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public SolenFriendship SolenFriendship { get; set; }
        #endregion

        #region Mondain's Legacy
        /*private List<BaseQuest> m_Quests;
		private Dictionary<QuestChain, BaseChain> m_Chains;

		public List<BaseQuest> Quests { get { return m_Quests; } }
        public Dictionary<QuestChain, BaseChain> Chains { get { return m_Chains; } }*/
        public List<BaseQuest> Quests => MondainQuestData.GetQuests(this);

        public Dictionary<QuestChain, BaseChain> Chains => MondainQuestData.GetChains(this);

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Peaced
        {
            get
            {
                if (PeacedUntil > DateTime.UtcNow)
                {
                    return true;
                }

                return false;
            }
        }

        public Dictionary<Collection, int> Collections { get; private set; }

        public List<object> RewardTitles { get; private set; }

        public int SelectedTitle { get; private set; }

        public bool RemoveRewardTitle(object o, bool silent)
        {
            if (RewardTitles.Contains(o))
            {
                int i = RewardTitles.IndexOf(o);

                if (i == SelectedTitle)
                    SelectRewardTitle(-1, silent);
                else if (i > SelectedTitle)
                    SelectRewardTitle(SelectedTitle - 1, silent);

                RewardTitles.Remove(o);

                return true;
            }

            return false;
        }

        public int GetCollectionPoints(Collection collection)
        {
            if (Collections == null)
            {
                Collections = new Dictionary<Collection, int>();
            }

            int points = 0;

            if (Collections.ContainsKey(collection))
            {
                Collections.TryGetValue(collection, out points);
            }

            return points;
        }

        public void AddCollectionPoints(Collection collection, int points)
        {
            if (Collections == null)
            {
                Collections = new Dictionary<Collection, int>();
            }

            if (Collections.ContainsKey(collection))
            {
                Collections[collection] += points;
            }
            else
            {
                Collections.Add(collection, points);
            }
        }

        public void SelectRewardTitle(int num, bool silent = false)
        {
            if (num == -1)
            {
                SelectedTitle = num;

                if (!silent)
                    SendLocalizedMessage(1074010); // You elect to hide your Reward Title.
            }
            else if (num < RewardTitles.Count && num >= -1)
            {
                if (SelectedTitle != num)
                {
                    SelectedTitle = num;

                    if (RewardTitles[num] is int && !silent)
                    {
                        SendLocalizedMessage(1074008, "#" + (int)RewardTitles[num]);
                        // You change your Reward Title to "~1_TITLE~".
                    }
                    else if (RewardTitles[num] is string && !silent)
                    {
                        SendLocalizedMessage(1074008, (string)RewardTitles[num]); // You change your Reward Title to "~1_TITLE~".
                    }
                }
                else if (!silent)
                {
                    SendLocalizedMessage(1074009); // You decide to leave your title as it is.
                }
            }

            InvalidateProperties();
        }

        public bool AddRewardTitle(object title)
        {
            if (RewardTitles == null)
            {
                RewardTitles = new List<object>();
            }

            if (title != null && !RewardTitles.Contains(title))
            {
                RewardTitles.Add(title);

                InvalidateProperties();
                return true;
            }

            return false;
        }

        public void ShowChangeTitle()
        {
            SendGump(new SelectTitleGump(this, SelectedTitle));
        }
        #endregion

        #region Titles
        private string m_FameKarmaTitle;
        private string m_PaperdollSkillTitle;
        private string m_SubtitleSkillTitle;
        private string m_CurrentChampTitle;
        private string m_OverheadTitle;
        private int m_CurrentVeteranTitle;

        public string FameKarmaTitle
        {
            get => m_FameKarmaTitle;
            set { m_FameKarmaTitle = value; InvalidateProperties(); }
        }

        public string PaperdollSkillTitle
        {
            get => m_PaperdollSkillTitle;
            set { m_PaperdollSkillTitle = value; InvalidateProperties(); }
        }

        public string SubtitleSkillTitle
        {
            get => m_SubtitleSkillTitle;
            set { m_SubtitleSkillTitle = value; InvalidateProperties(); }
        }

        public string CurrentChampTitle
        {
            get => m_CurrentChampTitle;
            set { m_CurrentChampTitle = value; InvalidateProperties(); }
        }

        public string OverheadTitle
        {
            get => m_OverheadTitle;
            set { m_OverheadTitle = value; InvalidateProperties(); }
        }

        public int CurrentVeteranTitle
        {
            get => m_CurrentVeteranTitle;
            set { m_CurrentVeteranTitle = value; InvalidateProperties(); }
        }

        public override bool DisplayAccessTitle
        {
            get
            {
                switch (AccessLevel)
                {
                    case AccessLevel.VIP:
                    case AccessLevel.Counselor:
                    case AccessLevel.GameMaster:
                    case AccessLevel.Seer:
                        return true;
                }

                return false;
            }
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            string prefix = "";

            if (DisplayFameTitle && Fame >= 10000)
            {
                prefix = Female ? "Lady" : "Lord";
            }

            string suffix = "";

            if (PropertyTitle && Title != null && Title.Length > 0)
            {
                suffix = Title;
            }

            BaseGuild guild = Guild;
            bool vvv = ViceVsVirtueSystem.IsVvV(this) && (ViceVsVirtueSystem.EnhancedRules || Map == Faction.Facet);

            if (m_OverheadTitle != null)
            {
                if (vvv)
                {
                    suffix = "[VvV]";
                }
                else
                {
                    int loc = Utility.ToInt32(m_OverheadTitle);

                    if (loc > 0)
                    {
                        if (CityLoyaltySystem.ApplyCityTitle(this, list, prefix, loc))
                            return;
                    }
                    else if (suffix.Length > 0)
                    {
                        suffix = string.Format("{0} {1}", suffix, m_OverheadTitle);
                    }
                    else
                    {
                        suffix = string.Format("{0}", m_OverheadTitle);
                    }
                }
            }
            else if (guild != null && DisplayGuildAbbr)
            {
                if (vvv)
                {
                    suffix = string.Format("[{0}] [VvV]", Utility.FixHtml(guild.Abbreviation));
                }
                else if (suffix.Length > 0)
                {
                    suffix = string.Format("{0} [{1}]", suffix, Utility.FixHtml(guild.Abbreviation));
                }
                else
                {
                    suffix = string.Format("[{0}]", Utility.FixHtml(guild.Abbreviation));
                }
            }
            else if (vvv)
            {
                suffix = "[VvV]";
            }

            suffix = ApplyNameSuffix(suffix);
            string name = Name;

            list.Add(1050045, "{0} \t{1}\t {2}", prefix, name, suffix); // ~1_PREFIX~~2_NAME~~3_SUFFIX~

            if (guild != null && DisplayGuildTitle)
            {
                string title = GuildTitle;

                if (title == null)
                {
                    title = "";
                }
                else
                {
                    title = title.Trim();
                }

                if (title.Length > 0)
                {
                    list.Add("{0}, {1}", Utility.FixHtml(title), Utility.FixHtml(guild.Name));
                }
            }
        }

        public override void OnAfterNameChange(string oldName, string newName)
        {
            if (m_FameKarmaTitle != null)
            {
                FameKarmaTitle = FameKarmaTitle.Replace(oldName, newName);
            }
        }
        #endregion

        public override void OnKillsChange(int oldValue)
        {
            if (Young && Kills > oldValue)
            {
                Account acc = Account as Account;

                if (acc != null)
                {
                    acc.RemoveYoungStatus(0);
                }
            }
        }

        public override void OnKarmaChange(int oldValue)
        {
            EpiphanyHelper.OnKarmaChange(this);
        }

        public override void OnSkillChange(SkillName skill, double oldBase)
        {
            if (Young)
            {
                if (SkillsTotal >= 4500 && (!Core.AOS && Skills[skill].Base >= 80.0))
                {
                    Account acc = Account as Account;

                    if (acc != null)
                    {
                        acc.RemoveYoungStatus(1019036);
                        // You have successfully obtained a respectable skill level, and have outgrown your status as a young player!
                    }
                }
            }

            if (skill != SkillName.Alchemy && Skills.CurrentMastery == skill && Skills[skill].Value < MasteryInfo.MinSkillRequirement)
            {
                //SendLocalizedMessage(1156236, String.Format("{0}\t{1}", MasteryInfo.MinSkillRequirement.ToString(), Skills[skill].Info.Name)); // You need at least ~1_SKILL_REQUIREMENT~ ~2_SKILL_NAME~ skill to use that mastery.

                SkillName mastery = Skills.CurrentMastery;
                Skills.CurrentMastery = SkillName.Alchemy;

                MasteryInfo.OnMasteryChanged(this, mastery);
            }

            TransformContext context = TransformationSpellHelper.GetContext(this);

            if (context != null)
            {
                TransformationSpellHelper.CheckCastSkill(this, context);
            }
        }

        public override void OnAccessLevelChanged(AccessLevel oldLevel)
        {
            if (IsPlayer())
            {
                IgnoreMobiles = TransformationSpellHelper.UnderTransformation(this, typeof(WraithFormSpell));
            }
            else
            {
                IgnoreMobiles = true;
            }
        }

        public override void OnDelete()
        {
            Instances.Remove(this);

            if (ReceivedHonorContext != null)
            {
                ReceivedHonorContext.Cancel();
            }
            if (SentHonorContext != null)
            {
                SentHonorContext.Cancel();
            }
        }

        #region Fastwalk Prevention
        private static bool FastwalkPrevention = true; // Is fastwalk prevention enabled?

        private static int FastwalkThreshold = 400; // Fastwalk prevention will become active after 0.4 seconds
        private bool m_HasMoved;

        public long NextMovementTime { get; private set; }

        public virtual bool UsesFastwalkPrevention => IsPlayer();

        public override int ComputeMovementSpeed(Direction dir, bool checkTurning)
        {
            if (checkTurning && (dir & Direction.Mask) != (Direction & Direction.Mask))
            {
                return RunMount; // We are NOT actually moving (just a direction change)
            }

            TransformContext context = TransformationSpellHelper.GetContext(this);

            if (context != null)
            {
                if ((!Core.SA && context.Type == typeof(ReaperFormSpell)) || (!Core.HS && context.Type == typeof(Server.Spells.Mysticism.StoneFormSpell)))
                {
                    return WalkFoot;
                }
            }

            bool running = ((dir & Direction.Running) != 0);

            bool onHorse = Mount != null || Flying;

            AnimalFormContext animalContext = AnimalForm.GetContext(this);

            if (onHorse || (animalContext != null && animalContext.SpeedBoost))
            {
                return (running ? RunMount : WalkMount);
            }

            return (running ? RunFoot : WalkFoot);
        }

        public static bool MovementThrottle_Callback(NetState ns, out bool drop)
        {
            drop = false;

            PlayerMobile pm = ns.Mobile as PlayerMobile;

            if (pm == null || !pm.UsesFastwalkPrevention)
            {
                return true;
            }

            if (!pm.m_HasMoved)
            {
                // has not yet moved
                pm.NextMovementTime = Core.TickCount;
                pm.m_HasMoved = true;
                return true;
            }

            long ts = pm.NextMovementTime - Core.TickCount;

            if (ts < 0)
            {
                // been a while since we've last moved
                pm.NextMovementTime = Core.TickCount;
                return true;
            }

            return (ts < FastwalkThreshold);
        }
        #endregion

        #region Hair and beard mods
        private int m_HairModID = -1, m_HairModHue;
        private int m_BeardModID = -1, m_BeardModHue;

        public void SetHairMods(int hairID, int beardID)
        {
            if (hairID == -1)
            {
                InternalRestoreHair(true, ref m_HairModID, ref m_HairModHue);
            }
            else if (hairID != -2)
            {
                InternalChangeHair(true, hairID, ref m_HairModID, ref m_HairModHue);
            }

            if (beardID == -1)
            {
                InternalRestoreHair(false, ref m_BeardModID, ref m_BeardModHue);
            }
            else if (beardID != -2)
            {
                InternalChangeHair(false, beardID, ref m_BeardModID, ref m_BeardModHue);
            }
        }

        private void CreateHair(bool hair, int id, int hue)
        {
            if (hair)
            {
                //TODO Verification?
                HairItemID = id;
                HairHue = hue;
            }
            else
            {
                FacialHairItemID = id;
                FacialHairHue = hue;
            }
        }

        private void InternalRestoreHair(bool hair, ref int id, ref int hue)
        {
            if (id == -1)
            {
                return;
            }

            if (hair)
            {
                HairItemID = 0;
            }
            else
            {
                FacialHairItemID = 0;
            }

            //if( id != 0 )
            CreateHair(hair, id, hue);

            id = -1;
            hue = 0;
        }

        private void InternalChangeHair(bool hair, int id, ref int storeID, ref int storeHue)
        {
            if (storeID == -1)
            {
                storeID = hair ? HairItemID : FacialHairItemID;
                storeHue = hair ? HairHue : FacialHairHue;
            }
            CreateHair(hair, id, 0);
        }
        #endregion

        #region Virtues

        public DateTime LastSacrificeGain { get; set; }
        public DateTime LastSacrificeLoss { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int AvailableResurrects { get; set; }

        private DateTime m_NextJustAward;

        public DateTime LastJusticeLoss { get; set; }
        public List<Mobile> JusticeProtectors { get; set; }

        public DateTime LastCompassionLoss { get; set; }
        public DateTime NextCompassionDay { get; set; }
        public int CompassionGains { get; set; }

        public DateTime LastValorLoss { get; set; }

        public DateTime m_hontime;

        public DateTime LastHonorLoss { get; set; }

        public DateTime LastHonorUse { get; set; }

        public bool HonorActive { get; set; }

        public HonorContext ReceivedHonorContext { get; set; }
        public HonorContext SentHonorContext { get; set; }
        #endregion

        #region Young system
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Young
        {
            get => GetFlag(PlayerFlag.Young);
            set
            {
                SetFlag(PlayerFlag.Young, value);
                InvalidateProperties();
            }
        }

        public override string ApplyNameSuffix(string suffix)
        {
            if (Young)
            {
                if (suffix.Length == 0)
                {
                    suffix = "(Young)";
                }
                else
                {
                    suffix = string.Concat(suffix, " (Young)");
                }
            }

            #region Ethics
            if (EthicPlayer != null)
            {
                if (suffix.Length == 0)
                {
                    suffix = EthicPlayer.Ethic.Definition.Adjunct.String;
                }
                else
                {
                    suffix = string.Concat(suffix, " ", EthicPlayer.Ethic.Definition.Adjunct.String);
                }
            }
            #endregion

            if (Core.ML && Map == Faction.Facet)
            {
                Faction faction = Faction.Find(this);

                if (faction != null)
                {
                    string adjunct = string.Format("[{0}]", faction.Definition.Abbreviation);
                    if (suffix.Length == 0)
                    {
                        suffix = adjunct;
                    }
                    else
                    {
                        suffix = string.Concat(suffix, " ", adjunct);
                    }
                }
            }

            return base.ApplyNameSuffix(suffix);
        }

        public override TimeSpan GetLogoutDelay()
        {
            if (Young || BedrollLogout || BlanketOfDarknessLogout || TestCenter.Enabled)
            {
                return TimeSpan.Zero;
            }

            return base.GetLogoutDelay();
        }

        private DateTime m_LastYoungMessage = DateTime.MinValue;

        public bool CheckYoungProtection(Mobile from)
        {
            if (!Young)
            {
                return false;
            }

            if (Region is BaseRegion && !((BaseRegion)Region).YoungProtected)
            {
                return false;
            }

            if (from is BaseCreature && ((BaseCreature)from).IgnoreYoungProtection)
            {
                return false;
            }

            if (Quest != null && Quest.IgnoreYoungProtection(from))
            {
                return false;
            }

            if (DateTime.UtcNow - m_LastYoungMessage > TimeSpan.FromMinutes(1.0))
            {
                m_LastYoungMessage = DateTime.UtcNow;
                SendLocalizedMessage(1019067);
                // A monster looks at you menacingly but does not attack.  You would be under attack now if not for your status as a new citizen of Britannia.
            }

            return true;
        }

        private DateTime m_LastYoungHeal = DateTime.MinValue;

        public bool CheckYoungHealTime()
        {
            if (DateTime.UtcNow - m_LastYoungHeal > TimeSpan.FromMinutes(5.0))
            {
                m_LastYoungHeal = DateTime.UtcNow;
                return true;
            }

            return false;
        }

        private static readonly Point3D[] m_TrammelDeathDestinations = new[]
        {
            new Point3D(1481, 1612, 20), new Point3D(2708, 2153, 0), new Point3D(2249, 1230, 0), new Point3D(5197, 3994, 37),
            new Point3D(1412, 3793, 0), new Point3D(3688, 2232, 20), new Point3D(2578, 604, 0), new Point3D(4397, 1089, 0),
            new Point3D(5741, 3218, -2), new Point3D(2996, 3441, 15), new Point3D(624, 2225, 0), new Point3D(1916, 2814, 0),
            new Point3D(2929, 854, 0), new Point3D(545, 967, 0), new Point3D(3469, 2559, 36)
        };

        private static readonly Point3D[] m_IlshenarDeathDestinations = new[]
        {
            new Point3D(1216, 468, -13), new Point3D(723, 1367, -60), new Point3D(745, 725, -28), new Point3D(281, 1017, 0),
            new Point3D(986, 1011, -32), new Point3D(1175, 1287, -30), new Point3D(1533, 1341, -3), new Point3D(529, 217, -44),
            new Point3D(1722, 219, 96)
        };

        private static readonly Point3D[] m_MalasDeathDestinations = new[]
        {new Point3D(2079, 1376, -70), new Point3D(944, 519, -71)};

        private static readonly Point3D[] m_TokunoDeathDestinations = new[]
        {new Point3D(1166, 801, 27), new Point3D(782, 1228, 25), new Point3D(268, 624, 15)};

        public bool YoungDeathTeleport()
        {
            if (Region.IsPartOf<Jail>() || Region.IsPartOf("Samurai start location") ||
                Region.IsPartOf("Ninja start location") || Region.IsPartOf("Ninja cave"))
            {
                return false;
            }

            Point3D loc;
            Map map;

            DungeonRegion dungeon = (DungeonRegion)Region.GetRegion(typeof(DungeonRegion));
            if (dungeon != null && dungeon.EntranceLocation != Point3D.Zero)
            {
                loc = dungeon.EntranceLocation;
                map = dungeon.EntranceMap;
            }
            else
            {
                loc = Location;
                map = Map;
            }

            Point3D[] list;

            if (map == Map.Trammel)
            {
                list = m_TrammelDeathDestinations;
            }
            else if (map == Map.Ilshenar)
            {
                list = m_IlshenarDeathDestinations;
            }
            else if (map == Map.Malas)
            {
                list = m_MalasDeathDestinations;
            }
            else if (map == Map.Tokuno)
            {
                list = m_TokunoDeathDestinations;
            }
            else
            {
                return false;
            }

            Point3D dest = Point3D.Zero;
            int sqDistance = int.MaxValue;

            for (int i = 0; i < list.Length; i++)
            {
                Point3D curDest = list[i];

                int width = loc.X - curDest.X;
                int height = loc.Y - curDest.Y;
                int curSqDistance = width * width + height * height;

                if (curSqDistance < sqDistance)
                {
                    dest = curDest;
                    sqDistance = curSqDistance;
                }
            }

            MoveToWorld(dest, map);
            return true;
        }

        private void SendYoungDeathNotice()
        {
            SendGump(new YoungDeathNotice());
        }
        #endregion

        #region Speech

        public SpeechLog SpeechLog { get; private set; }

        [CommandProperty(AccessLevel.Administrator)]
        public bool TempSquelched { get; set; }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (SpeechLog.Enabled && NetState != null)
            {
                if (SpeechLog == null)
                {
                    SpeechLog = new SpeechLog();
                }

                SpeechLog.Add(e.Mobile, e.Speech);
            }
        }

        public override void OnSaid(SpeechEventArgs e)
        {
            if (TempSquelched)
            {
                if (Core.ML)
                {
                    SendLocalizedMessage(500168); // You can not say anything, you have been muted.
                }
                else
                {
                    SendMessage("You can not say anything, you have been squelched."); //Cliloc ITSELF changed during ML.
                }

                e.Blocked = true;
            }
            else
            {
                base.OnSaid(e);
            }
        }
        #endregion

        #region Champion Titles
        [CommandProperty(AccessLevel.GameMaster)]
        public bool DisplayChampionTitle { get => GetFlag(PlayerFlag.DisplayChampionTitle); set => SetFlag(PlayerFlag.DisplayChampionTitle, value); }

        private ChampionTitleInfo m_ChampionTitles;

        [CommandProperty(AccessLevel.GameMaster)]
        public ChampionTitleInfo ChampionTitles { get => m_ChampionTitles; set { } }

        private void ToggleChampionTitleDisplay()
        {
            if (!CheckAlive())
            {
                return;
            }

            if (DisplayChampionTitle)
            {
                SendLocalizedMessage(1062419, "", 0x23); // You have chosen to hide your monster kill title.
            }
            else
            {
                SendLocalizedMessage(1062418, "", 0x23); // You have chosen to display your monster kill title.
            }

            DisplayChampionTitle = !DisplayChampionTitle;
        }

        [PropertyObject]
        public class ChampionTitleInfo
        {
            public static TimeSpan LossDelay = TimeSpan.FromDays(1.0);
            public const int LossAmount = 90;

            private class TitleInfo
            {
                public int Value { get; set; }
                public DateTime LastDecay { get; set; }

                public TitleInfo()
                { }

                public TitleInfo(GenericReader reader)
                {
                    int version = reader.ReadEncodedInt();

                    switch (version)
                    {
                        case 0:
                            {
                                Value = reader.ReadEncodedInt();
                                LastDecay = reader.ReadDateTime();
                                break;
                            }
                    }
                }

                public static void Serialize(GenericWriter writer, TitleInfo info)
                {
                    writer.WriteEncodedInt(0); // version

                    writer.WriteEncodedInt(info.Value);
                    writer.Write(info.LastDecay);
                }
            }

            private TitleInfo[] m_Values;

            public int GetValue(ChampionSpawnType type)
            {
                return GetValue((int)type);
            }

            public void SetValue(ChampionSpawnType type, int value)
            {
                SetValue((int)type, value);
            }

            public void Award(ChampionSpawnType type, int value)
            {
                Award((int)type, value);
            }

            public int GetValue(int index)
            {
                if (m_Values == null || index < 0 || index >= m_Values.Length)
                {
                    return 0;
                }

                if (m_Values[index] == null)
                {
                    m_Values[index] = new TitleInfo();
                }

                return m_Values[index].Value;
            }

            public DateTime GetLastDecay(int index)
            {
                if (m_Values == null || index < 0 || index >= m_Values.Length)
                {
                    return DateTime.MinValue;
                }

                if (m_Values[index] == null)
                {
                    m_Values[index] = new TitleInfo();
                }

                return m_Values[index].LastDecay;
            }

            public void SetValue(int index, int value)
            {
                if (m_Values == null)
                {
                    m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];
                }

                if (value < 0)
                {
                    value = 0;
                }

                if (index < 0 || index >= m_Values.Length)
                {
                    return;
                }

                if (m_Values[index] == null)
                {
                    m_Values[index] = new TitleInfo();
                }

                m_Values[index].Value = value;
            }

            public void Award(int index, int value)
            {
                if (m_Values == null)
                {
                    m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];
                }

                if (index < 0 || index >= m_Values.Length || value <= 0)
                {
                    return;
                }

                if (m_Values[index] == null)
                {
                    m_Values[index] = new TitleInfo();
                }

                m_Values[index].Value += value;
            }

            public void Atrophy(int index, int value)
            {
                if (m_Values == null)
                {
                    m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];
                }

                if (index < 0 || index >= m_Values.Length || value <= 0)
                {
                    return;
                }

                if (m_Values[index] == null)
                {
                    m_Values[index] = new TitleInfo();
                }

                int before = m_Values[index].Value;

                if ((m_Values[index].Value - value) < 0)
                {
                    m_Values[index].Value = 0;
                }
                else
                {
                    m_Values[index].Value -= value;
                }

                if (before != m_Values[index].Value)
                {
                    m_Values[index].LastDecay = DateTime.UtcNow;
                }
            }

            public override string ToString()
            {
                return "...";
            }

            [CommandProperty(AccessLevel.GameMaster)]
            public int Abyss { get => GetValue(ChampionSpawnType.Abyss); set => SetValue(ChampionSpawnType.Abyss, value); }

            [CommandProperty(AccessLevel.GameMaster)]
            public int Arachnid { get => GetValue(ChampionSpawnType.Arachnid); set => SetValue(ChampionSpawnType.Arachnid, value); }

            [CommandProperty(AccessLevel.GameMaster)]
            public int ColdBlood { get => GetValue(ChampionSpawnType.ColdBlood); set => SetValue(ChampionSpawnType.ColdBlood, value); }

            [CommandProperty(AccessLevel.GameMaster)]
            public int ForestLord { get => GetValue(ChampionSpawnType.ForestLord); set => SetValue(ChampionSpawnType.ForestLord, value); }

            [CommandProperty(AccessLevel.GameMaster)]
            public int SleepingDragon { get => GetValue(ChampionSpawnType.SleepingDragon); set => SetValue(ChampionSpawnType.SleepingDragon, value); }

            [CommandProperty(AccessLevel.GameMaster)]
            public int UnholyTerror { get => GetValue(ChampionSpawnType.UnholyTerror); set => SetValue(ChampionSpawnType.UnholyTerror, value); }

            [CommandProperty(AccessLevel.GameMaster)]
            public int VerminHorde { get => GetValue(ChampionSpawnType.VerminHorde); set => SetValue(ChampionSpawnType.VerminHorde, value); }

            [CommandProperty(AccessLevel.GameMaster)]
            public int Harrower { get; set; }

            #region Mondain's Legacy Peerless Champion
            [CommandProperty(AccessLevel.GameMaster)]
            public int Glade { get => GetValue(ChampionSpawnType.Glade); set => SetValue(ChampionSpawnType.Glade, value); }

            [CommandProperty(AccessLevel.GameMaster)]
            public int Corrupt { get => GetValue(ChampionSpawnType.Corrupt); set => SetValue(ChampionSpawnType.Corrupt, value); }
            #endregion

            public ChampionTitleInfo()
            { }

            public ChampionTitleInfo(GenericReader reader)
            {
                int version = reader.ReadEncodedInt();

                switch (version)
                {
                    case 0:
                        {
                            Harrower = reader.ReadEncodedInt();

                            int length = reader.ReadEncodedInt();
                            m_Values = new TitleInfo[length];

                            for (int i = 0; i < length; i++)
                            {
                                m_Values[i] = new TitleInfo(reader);
                            }

                            if (m_Values.Length != ChampionSpawnInfo.Table.Length)
                            {
                                var oldValues = m_Values;
                                m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

                                for (int i = 0; i < m_Values.Length && i < oldValues.Length; i++)
                                {
                                    m_Values[i] = oldValues[i];
                                }
                            }
                            break;
                        }
                }
            }

            public static void Serialize(GenericWriter writer, ChampionTitleInfo titles)
            {
                writer.WriteEncodedInt(0); // version

                writer.WriteEncodedInt(titles.Harrower);

                int length = titles.m_Values.Length;
                writer.WriteEncodedInt(length);

                for (int i = 0; i < length; i++)
                {
                    if (titles.m_Values[i] == null)
                    {
                        titles.m_Values[i] = new TitleInfo();
                    }

                    TitleInfo.Serialize(writer, titles.m_Values[i]);
                }
            }

            public static void CheckAtrophy(PlayerMobile pm)
            {
                ChampionTitleInfo t = pm.m_ChampionTitles;
                if (t == null)
                {
                    return;
                }

                if (t.m_Values == null)
                {
                    t.m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];
                }

                for (int i = 0; i < t.m_Values.Length; i++)
                {
                    if ((t.GetLastDecay(i) + LossDelay) < DateTime.UtcNow)
                    {
                        t.Atrophy(i, LossAmount);
                    }
                }
            }

            public static void AwardHarrowerTitle(PlayerMobile pm)
            //Called when killing a harrower.  Will give a minimum of 1 point.
            {
                ChampionTitleInfo t = pm.m_ChampionTitles;
                if (t == null)
                {
                    return;
                }

                if (t.m_Values == null)
                {
                    t.m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];
                }

                int count = 1;

                for (int i = 0; i < t.m_Values.Length; i++)
                {
                    if (t.m_Values[i].Value > 900)
                    {
                        count++;
                    }
                }

                t.Harrower = Math.Max(count, t.Harrower); //Harrower titles never decay.
            }

            public bool HasChampionTitle(PlayerMobile pm)
            {
                if (Harrower > 0)
                    return true;

                if (m_Values == null)
                    return false;

                foreach (TitleInfo info in m_Values)
                {
                    if (info.Value > 300)
                        return true;
                }

                return false;
            }
        }
        #endregion

        #region Recipes
        private Dictionary<int, bool> m_AcquiredRecipes;

        public virtual bool HasRecipe(Recipe r)
        {
            if (r == null)
            {
                return false;
            }

            return HasRecipe(r.ID);
        }

        public virtual bool HasRecipe(int recipeID)
        {
            if (m_AcquiredRecipes != null && m_AcquiredRecipes.ContainsKey(recipeID))
            {
                return m_AcquiredRecipes[recipeID];
            }

            return false;
        }

        public virtual void AcquireRecipe(Recipe r)
        {
            if (r != null)
            {
                AcquireRecipe(r.ID);
            }
        }

        public virtual void AcquireRecipe(int recipeID)
        {
            if (m_AcquiredRecipes == null)
            {
                m_AcquiredRecipes = new Dictionary<int, bool>();
            }

            m_AcquiredRecipes[recipeID] = true;
        }

        public virtual void ResetRecipes()
        {
            m_AcquiredRecipes = null;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int KnownRecipes
        {
            get
            {
                if (m_AcquiredRecipes == null)
                {
                    return 0;
                }

                return m_AcquiredRecipes.Count;
            }
        }
        #endregion

        #region Buff Icons
        public void ResendBuffs()
        {
            if (!BuffInfo.Enabled || m_BuffTable == null)
            {
                return;
            }

            NetState state = NetState;

            if (state != null && state.BuffIcon)
            {
                foreach (BuffInfo info in m_BuffTable.Values)
                {
                    state.Send(new AddBuffPacket(this, info));
                }
            }
        }

        private Dictionary<BuffIcon, BuffInfo> m_BuffTable;

        public void AddBuff(BuffInfo b)
        {
            if (!BuffInfo.Enabled || b == null)
            {
                return;
            }

            RemoveBuff(b); //Check & subsequently remove the old one.

            if (m_BuffTable == null)
            {
                m_BuffTable = new Dictionary<BuffIcon, BuffInfo>();
            }

            m_BuffTable.Add(b.ID, b);

            NetState state = NetState;

            if (state != null && state.BuffIcon)
            {
                state.Send(new AddBuffPacket(this, b));
            }
        }

        public void RemoveBuff(BuffInfo b)
        {
            if (b == null)
            {
                return;
            }

            RemoveBuff(b.ID);
        }

        public void RemoveBuff(BuffIcon b)
        {
            if (m_BuffTable == null || !m_BuffTable.ContainsKey(b))
            {
                return;
            }

            BuffInfo info = m_BuffTable[b];

            if (info.Timer != null && info.Timer.Running)
            {
                info.Timer.Stop();
            }

            m_BuffTable.Remove(b);

            NetState state = NetState;

            if (state != null && state.BuffIcon)
            {
                state.Send(new RemoveBuffPacket(this, b));
            }

            if (m_BuffTable.Count <= 0)
            {
                m_BuffTable = null;
            }
        }
        #endregion

        [CommandProperty(AccessLevel.GameMaster)]
        public ExploringTheDeepQuestChain ExploringTheDeepQuest { get; set; }

        public static bool PetAutoStable => Core.SE;

        public void AutoStablePets()
        {
            if (PetAutoStable && AllFollowers.Count > 0)
            {
                for (int i = m_AllFollowers.Count - 1; i >= 0; --i)
                {
                    BaseCreature pet = AllFollowers[i] as BaseCreature;

                    if (pet == null)
                    {
                        continue;
                    }

                    if (pet.Summoned && pet.Map != Map)
                    {
                        pet.PlaySound(pet.GetAngerSound());

                        Timer.DelayCall(pet.Delete);

                        continue;
                    }

                    if (!pet.CanAutoStable)
                    {
                        continue;
                    }

                    pet.ControlTarget = null;
                    pet.ControlOrder = OrderType.Stay;
                    pet.Internalize();

                    pet.SetControlMaster(null);
                    pet.SummonMaster = null;

                    pet.IsStabled = true;
                    pet.StabledBy = this;

                    //pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully happy

                    Stabled.Add(pet);
                    AutoStabled.Add(pet);
                }
            }
        }

        public void ClaimAutoStabledPets()
        {
            if (!PetAutoStable || !Region.AllowAutoClaim(this) || AutoStabled == null || AutoStabled.Count <= 0)
            {
                return;
            }

            if (!Alive)
            {
                SendGump(new ReLoginClaimGump());
                return;
            }

            for (int i = AutoStabled.Count - 1; i >= 0; --i)
            {
                BaseCreature pet = AutoStabled[i] as BaseCreature;
                if (pet == null) continue;

                if (pet.Deleted)
                {
                    pet.IsStabled = false;
                    pet.StabledBy = null;

                    if (Stabled.Contains(pet)) Stabled.Remove(pet);

                    continue;
                }

                if ((Followers + pet.ControlSlots) <= FollowersMax)
                {
                    pet.SetControlMaster(this);

                    if (pet.Summoned)
                    {
                        pet.SummonMaster = this;
                    }

                    pet.ControlTarget = this;
                    pet.ControlOrder = OrderType.Follow;

                    pet.MoveToWorld(Location, Map);

                    pet.IsStabled = false;
                    pet.StabledBy = null;

                    //pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy

                    if (Stabled.Contains(pet))
                    {
                        Stabled.Remove(pet);
                    }
                }
                else
                {
                    SendLocalizedMessage(1049612, pet.Name); // ~1_NAME~ remained in the stables because you have too many followers.
                }
            }

            AutoStabled.Clear();
        }
    }
}
