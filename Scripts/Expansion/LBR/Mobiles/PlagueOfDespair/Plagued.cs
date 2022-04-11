using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a plague beast corpse")]
    public class PlagueBeast : BaseCreature, IDevourer
    {
        private int m_DevourGoal;

        [CommandProperty(AccessLevel.GameMaster)]
        public int TotalDevoured { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DevourGoal
        {
            get => (IsParagon ? m_DevourGoal + 25 : m_DevourGoal);
            set => m_DevourGoal = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasMetalChest { get; private set; } = false;

        [Constructible]
        public PlagueBeast()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a plague beast";
            Body = 775;

            SetStr(302, 500);
            SetDex(80);
            SetInt(16, 20);

            SetHits(318, 404);

            SetDamage(20, 24);

            SetDamageType(ResistType.Physical, 60);
            SetDamageType(ResistType.Poison, 40);

            SetResist(ResistType.Physical, 45, 55);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Poison, 65, 75);
            SetResist(ResistType.Energy, 25, 35);

            SetSkill(SkillName.MagicResist, 35.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 100.0);

            Fame = 13000;
            Karma = -13000;

            VirtualArmor = 30;

            TotalDevoured = 0;
            m_DevourGoal = Utility.RandomMinMax(15, 25); // How many corpses must be devoured before a metal chest is awarded

            SetSpecialAbility(SpecialAbility.PoisonSpit);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Utility.RandomDouble() < 0.80)
                CorpseLoot.DropItem(new PlagueBeastGland());

            if (Core.ML && Utility.RandomDouble() < 0.33)
                CorpseLoot.DropItem(Engines.Plants.Seed.RandomPeculiarSeed(2));

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Gems, Utility.Random(1, 3));
        }

        public override void OnDamagedBySpell(Mobile caster)
        {
            if (Map != null && caster != this && 0.25 > Utility.RandomDouble())
            {
                BaseCreature spawn = new PlagueSpawn(this);

                spawn.Team = Team;
                spawn.MoveToWorld(Location, Map);
                spawn.Combatant = caster;

                Say(1053034); // * The plague beast creates another beast from its flesh! *
            }

            base.OnDamagedBySpell(caster);
        }

        public override bool AutoDispel => true;
        public override Poison PoisonImmunity => Poison.Lethal;

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            if (Map != null && attacker != this && 0.25 > Utility.RandomDouble())
            {
                BaseCreature spawn = new PlagueSpawn(this);

                spawn.Team = Team;
                spawn.MoveToWorld(Location, Map);
                spawn.Combatant = attacker;

                Say(1053034); // * The plague beast creates another beast from its flesh! *
            }

            base.OnGotMeleeAttack(attacker);
        }

        public PlagueBeast(Serial serial)
            : base(serial)
        {
        }

        public override int GetIdleSound() { return 0x1BF; }
        public override int GetAttackSound() { return 0x1C0; }
        public override int GetHurtSound() { return 0x1C1; }
        public override int GetDeathSound() { return 0x1C2; }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);

            writer.Write(HasMetalChest);
            writer.Write(TotalDevoured);
            writer.Write(m_DevourGoal);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        HasMetalChest = reader.ReadBool();
                        TotalDevoured = reader.ReadInt();
                        m_DevourGoal = reader.ReadInt();
                        break;
                    }
            }
        }

        public override void OnThink()
        {
            base.OnThink();

            // Check to see if we need to devour any corpses
            IPooledEnumerable eable = GetItemsInRange(3); // Get all corpses in range

            foreach (Item item in eable)
            {
                if (item is Corpse) // For each Corpse
                {
                    Corpse corpse = item as Corpse;

                    // Ensure that the corpse was killed by us
                    if (corpse != null && corpse.Killer == this && corpse.Owner != null)
                    {
                        if (!corpse.DevourCorpse() && !corpse.Devoured)
                            PublicOverheadMessage(MessageType.Emote, 0x3B2, 1053032); // * The plague beast attempts to absorb the remains, but cannot! *
                    }
                }
            }
            eable.Free();
        }

        #region IDevourer Members

        public bool Devour(Corpse corpse)
        {
            if (corpse == null || corpse.Owner == null) // sorry we can't devour because the corpse's owner is null
                return false;

            if (corpse.Owner.Body.IsHuman)
                corpse.TurnToBones(); // Not bones yet, and we are a human body therefore we turn to bones.

            IncreaseHits((int)Math.Ceiling(corpse.Owner.HitsMax * 0.75));
            TotalDevoured++;

            PublicOverheadMessage(MessageType.Emote, 0x3B2, 1053033); // * The plague beast absorbs the fleshy remains of the corpse *

            if (!HasMetalChest && TotalDevoured >= DevourGoal)
            {
                PackItem(new MetalChest());
                HasMetalChest = true;
            }

            return true;
        }

        #endregion

        private void IncreaseHits(int hp)
        {
            int maxhits = 2000;

            if (IsParagon)
                maxhits = (int)(maxhits * Paragon.HitsBuff);

            if (hp < 1000 && !Core.AOS)
                hp = (hp * 100) / 60;

            if (HitsMaxSeed >= maxhits)
            {
                HitsMaxSeed = maxhits;

                int newHits = Hits + hp + Utility.RandomMinMax(10, 20); // increase the hp until it hits if it goes over it'll max at 2000

                Hits = Math.Min(maxhits, newHits);
                // Also provide heal for each devour on top of the hp increase
            }
            else
            {
                int min = (hp / 2) + 10;
                int max = hp + 20;
                int hpToIncrease = Utility.RandomMinMax(min, max);

                HitsMaxSeed += hpToIncrease;
                Hits += hpToIncrease;
                // Also provide heal for each devour
            }
        }
    }

    [CorpseName("a plague beast lord corpse")]
    public class PlagueBeastLord : BaseCreature, ICarvable, IScissorable
    {
        private DecayTimer m_Timer;
        [Constructible]
        public PlagueBeastLord()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a plague beast lord";
            Body = 775;
            BaseSoundID = 679;
            SpeechHue = 0x3B2;

            SetStr(500);
            SetDex(100);
            SetInt(30);

            SetHits(1800);

            SetDamage(20, 25);

            SetDamageType(ResistType.Physical, 50);
            SetDamageType(ResistType.Fire, 25);
            SetDamageType(ResistType.Poison, 25);

            SetResist(ResistType.Physical, 45, 55);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Poison, 75, 85);
            SetResist(ResistType.Energy, 25, 35);

            SetSkill(SkillName.Tactics, 100);
            SetSkill(SkillName.Wrestling, 100);

            Fame = 2000;
            Karma = -2000;

            VirtualArmor = 50;
        }

        public PlagueBeastLord(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Lethal;

        public override int GetIdleSound() { return 0x1BF; }
        public override int GetAttackSound() { return 0x1C0; }
        public override int GetHurtSound() { return 0x1C1; }
        public override int GetDeathSound() { return 0x1C2; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile OpenedBy { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsBleeding
        {
            get
            {
                Container pack = Backpack;

                if (pack != null)
                {
                    for (int i = 0; i < pack.Items.Count; i++)
                    {
                        PlagueBeastBlood blood = pack.Items[i] as PlagueBeastBlood;

                        if (blood != null && !blood.Patched)
                            return true;
                    }
                }

                return false;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsAccessibleTo(from))
            {
                if (OpenedBy != null && Backpack != null)
                    Backpack.DisplayTo(from);
                else
                    PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1071917, from.NetState); // * You attempt to tear open the amorphous flesh, but it resists *
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (IsAccessibleTo(from) && (dropped is PlagueBeastInnard || dropped is PlagueBeastGland))
                return base.OnDragDrop(from, dropped);

            return false;
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            for (int i = c.Items.Count - 1; i >= 0; i--)
                c.Items[i].Delete();
        }

        public override void OnDelete()
        {
            if (OpenedBy != null && OpenedBy.Holding is PlagueBeastInnard)
                OpenedBy.Holding.Delete();

            if (Backpack != null)
            {
                for (int i = Backpack.Items.Count - 1; i >= 0; i--)
                    Backpack.Items[i].Delete();

                Backpack.Delete();
            }

            base.OnDelete();
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);

            if (Backpack != null && IsAccessibleTo(m) && m.InRange(oldLocation, 3) && !m.InRange(this, 3))
                Backpack.SendRemovePacket();
        }

        public override bool CheckNonlocalLift(Mobile from, Item item)
        {
            return true;
        }

        public override bool CheckNonlocalDrop(Mobile from, Item item, Item target)
        {
            return true;
        }

        public override bool IsSnoop(Mobile from)
        {
            return false;
        }

        public virtual void OnParalyzed(Mobile from)
        {
            FightMode = FightMode.None;
            Frozen = true;
            Blessed = true;
            Combatant = null;
            Hue = 0x480;
            from.Combatant = null;
            from.Warmode = false;

            m_Timer = new DecayTimer(this);
            m_Timer.Start();

            Timer.DelayCall(TimeSpan.Zero, new TimerCallback(BroadcastMessage));
        }

        public virtual bool IsAccessibleTo(Mobile check)
        {
            if (check.AccessLevel >= AccessLevel.GameMaster)
                return true;

            if (!InRange(check, 2))
                PrivateOverheadMessage(MessageType.Label, 0x3B2, 500446, check.NetState); // That is too far away.
            else if (OpenedBy != null && OpenedBy != check)
                PrivateOverheadMessage(MessageType.Label, 0x3B2, 500365, check.NetState); // That is being used by someone else
            else if (Frozen)
                return true;

            return false;
        }

        public virtual bool Carve(Mobile from, Item item)
        {
            if (OpenedBy == null && IsAccessibleTo(from))
            {
                OpenedBy = from;

                if (m_Timer == null)
                    m_Timer = new DecayTimer(this);

                if (!m_Timer.Running)
                    m_Timer.Start();

                m_Timer.StartDissolving();

                PlagueBeastBackpack pack = new PlagueBeastBackpack();
                AddItem(pack);
                pack.Initialize();

                foreach (NetState state in GetClientsInRange(12))
                {
                    Mobile m = state.Mobile;

                    if (m != null && m.Player && m != from)
                        PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1071919, from.Name, m.NetState); // * ~1_VAL~ slices through the plague beast's amorphous tissue *
                }

                from.LocalOverheadMessage(MessageType.Regular, 0x21, 1071904); // * You slice through the plague beast's amorphous tissue *
                Timer.DelayCall(TimeSpan.Zero, new TimerStateCallback<Mobile>(pack.Open), from);

                return true;
            }

            return false;
        }

        public virtual bool Scissor(Mobile from, Scissors scissors)
        {
            if (IsAccessibleTo(from))
                scissors.PublicOverheadMessage(MessageType.Regular, 0x3B2, 1071918);  // You can't cut through the plague beast's amorphous skin with scissors!

            return false;
        }

        public void Unfreeze()
        {
            FightMode = FightMode.Closest;
            Frozen = false;
            Blessed = false;

            if (OpenedBy == null)
                Hue = 0;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0);

            writer.Write(OpenedBy);

            if (m_Timer != null)
            {
                writer.Write(true);
                writer.Write(m_Timer.Count);
                writer.Write(m_Timer.Deadline);
            }
            else
                writer.Write(false);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();

            OpenedBy = reader.ReadMobile();

            if (reader.ReadBool())
            {
                int count = reader.ReadInt();
                int deadline = reader.ReadInt();

                m_Timer = new DecayTimer(this, count, deadline);
                m_Timer.Start();
            }

            if (FightMode == FightMode.None)
                Frozen = true;
        }

        private void BroadcastMessage()
        {
            PublicOverheadMessage(MessageType.Regular, 0x3B2, 1071920); // * The plague beast's amorphous flesh hardens and becomes immobilized *
        }

        private class DecayTimer : Timer
        {
            private readonly PlagueBeastLord m_Lord;

            public DecayTimer(PlagueBeastLord lord)
                : this(lord, 0, 120)
            {
            }

            public DecayTimer(PlagueBeastLord lord, int count, int deadline)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            {
                m_Lord = lord;
                Count = count;
                Deadline = deadline;
            }

            public int Count { get; private set; }
            public int Deadline { get; private set; }

            public void StartDissolving()
            {
                Deadline = Math.Min(Count + 60, Deadline);
            }

            protected override void OnTick()
            {
                if (m_Lord == null || m_Lord.Deleted)
                {
                    Stop();
                    return;
                }

                if (Count + 15 == Deadline)
                {
                    if (m_Lord.OpenedBy != null)
                        m_Lord.PublicOverheadMessage(MessageType.Regular, 0x3B2, 1071921); // * The plague beast begins to bubble and dissolve! *

                    m_Lord.PlaySound(0x103);
                }
                else if (Count + 10 == Deadline)
                {
                    m_Lord.PlaySound(0x21);
                }
                else if (Count + 5 == Deadline)
                {
                    m_Lord.PlaySound(0x1C2);
                }
                else if (Count == Deadline)
                {
                    m_Lord.Unfreeze();

                    if (m_Lord.OpenedBy != null)
                        m_Lord.Kill();

                    Stop();
                }
                else if (Count % 15 == 0)
                    m_Lord.PlaySound(0x1BF);

                Count++;
            }
        }
    }

    [CorpseName("a plague rat corpse")]
    public class PlagueRat : BaseCreature
    {
        [Constructible]
        public PlagueRat()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a Clan Ribbon Plague Rat";
            Body = 0xD7;
            Hue = 1710;
            BaseSoundID = 0x188;

            SetStr(59);
            SetDex(51);
            SetInt(17);

            SetHits(92);
            SetMana(0);

            SetDamage(4, 8);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 35, 40);
            SetResist(ResistType.Fire, 20, 25);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Energy, 35, 40);

            SetSkill(SkillName.MagicResist, 25.1, 30.0);
            SetSkill(SkillName.Tactics, 34.5, 40.0);
            SetSkill(SkillName.Wrestling, 40.5, 45.0);

            Fame = 300;
            Karma = -300;

            VirtualArmor = 38;
        }

        public PlagueRat(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int Hides => 6;
        public override FoodType FavoriteFood => FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies | FoodType.Eggs;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a plague spawn corpse")]
    public class PlagueSpawn : BaseCreature
    {
        [Constructible]
        public PlagueSpawn()
            : this(null)
        {
        }

        public PlagueSpawn(Mobile owner)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Owner = owner;
            ExpireTime = DateTime.UtcNow + TimeSpan.FromMinutes(1.0);

            Name = "a plague spawn";
            Hue = Utility.Random(0x11, 15);

            switch (Utility.Random(12))
            {
                case 0: // earth elemental
                    Body = 14;
                    BaseSoundID = 268;
                    break;
                case 1: // headless one
                    Body = 31;
                    BaseSoundID = 0x39D;
                    break;
                case 2: // person
                    Body = Utility.RandomList(400, 401);
                    break;
                case 3: // gorilla
                    Body = 0x1D;
                    BaseSoundID = 0x9E;
                    break;
                case 4: // serpent
                    Body = 0x15;
                    BaseSoundID = 0xDB;
                    break;
                default:
                case 5: // slime
                    Body = 51;
                    BaseSoundID = 456;
                    break;
            }

            SetStr(201, 300);
            SetDex(80);
            SetInt(16, 20);

            SetHits(121, 180);

            SetDamage(11, 17);
            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 35, 45);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Poison, 65, 75);
            SetResist(ResistType.Energy, 25, 35);

            SetSkill(SkillName.MagicResist, 25.0);
            SetSkill(SkillName.Tactics, 25.0);
            SetSkill(SkillName.Wrestling, 50.0);

            Fame = 1000;
            Karma = -1000;

            VirtualArmor = 20;
        }

        public PlagueSpawn(Serial serial)
            : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime ExpireTime { get; set; }

        public override bool AlwaysMurderer => true;

        public override void DisplayPaperdollTo(Mobile to)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is PaperdollEntry)
                    list.RemoveAt(i--);
            }
        }

        public override void OnThink()
        {
            if (Owner != null && (DateTime.UtcNow >= ExpireTime || Owner.Deleted || Map != Owner.Map || !InRange(Owner, 16)))
            {
                PlaySound(GetIdleSound());
                Delete();
            }
            else
            {
                base.OnThink();
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLoot(LootPack.Gems);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
