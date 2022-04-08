using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Multis;
using Server.Network;
using System;
using System.Collections.Generic;

namespace Server.Engines.VeteranRewards
{
    public class EnchantedGraniteCartComponent : LocalizedAddonComponent, IDyable
    {
        public override bool ForceShowProperties => true;

        [CommandProperty(AccessLevel.GameMaster)]
        public int RewardCount => ((EnchantedGraniteCartAddon)Addon).RewardCount;

        public EnchantedGraniteCartComponent(int id)
            : base(id, 1126338) // cart
        {
        }

        public virtual bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
            {
                return false;
            }

            Hue = sender.DyedHue;
            return true;
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            SetSecureLevelEntry.AddTo(from, Addon, list);
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1159435, RewardCount.ToString()); // Granite: ~1_COUNT~
        }

        public EnchantedGraniteCartComponent(Serial serial)
            : base(serial)
        {
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

    public class EnchantedGraniteCartAddon : BaseAddon, ISecurable
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem { get; set; }

        public override BaseAddonDeed Deed
        {
            get
            {
                EnchantedGraniteCartAddonDeed deed = new EnchantedGraniteCartAddonDeed
                {
                    IsRewardItem = IsRewardItem
                };

                return deed;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NextUse { get; set; }

        private int m_RewardCount;

        [CommandProperty(AccessLevel.GameMaster)]
        public int RewardCount { get => m_RewardCount; set { m_RewardCount = value; OnRefreshProperties(); } }

        private Timer Timer { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level { get; set; }

        public void OnRefreshProperties()
        {
            InvalidateProperties();

            Components.ForEach(x => x.InvalidateProperties());
        }

        [Constructible]
        public EnchantedGraniteCartAddon(DirectionType type)
           : this(type, DateTime.Now + TimeSpan.FromDays(1.0))
        {
        }

        [Constructible]
        public EnchantedGraniteCartAddon(DirectionType type, DateTime nexuse)
        {
            NextUse = nexuse;

            switch (type)
            {
                case DirectionType.South:
                    AddComponent(new EnchantedGraniteCartComponent(0xA54B), 0, 0, 0);
                    AddComponent(new AddonComponent(0xA54D), 1, 1, 0);
                    break;
                case DirectionType.East:
                    AddComponent(new EnchantedGraniteCartComponent(0xA54A), 0, 0, 0);
                    AddComponent(new AddonComponent(0xA54C), 1, 1, 0);
                    break;
            }

            StartTimer();
        }

        public void StartTimer()
        {
            if (Timer == null)
            {
                Timer = new EnchantedGraniteCartAddonTimer(this);
            }

            Timer.Start();
        }

        public void OnTick()
        {
            if (NextUse < DateTime.Now && RewardCount <= 20)
            {
                RewardCount += 2;
                NextUse = DateTime.Now + TimeSpan.FromDays(1.0);
            }
        }

        private class EnchantedGraniteCartAddonTimer : Timer
        {
            private EnchantedGraniteCartAddon i_item;

            public EnchantedGraniteCartAddonTimer(EnchantedGraniteCartAddon item)
                : base(TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(10.0))
            {
                Priority = TimerPriority.OneSecond;
                i_item = item;
            }

            protected override void OnTick()
            {
                if (i_item == null || i_item.Deleted)
                {
                    Stop();
                    return;
                }

                i_item.OnTick();
            }
        }

        public bool CheckAccessible(Mobile from, Item item)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
            {
                return true; // Staff can access anything
            }

            BaseHouse house = BaseHouse.FindHouseAt(item);

            if (house == null)
            {
                return false;
            }

            switch (Level)
            {
                case SecureLevel.Owner: return house.IsOwner(from);
                case SecureLevel.CoOwners: return house.IsCoOwner(from);
                case SecureLevel.Friends: return house.IsFriend(from);
                case SecureLevel.Anyone: return true;
                case SecureLevel.Guild: return house.IsGuildMember(from);
            }

            return false;
        }

        private static readonly Type[] GraniteType = new Type[]
        {
            typeof(Granite),        typeof(DullCopperGranite),  typeof(ShadowIronGranite),
            typeof(CopperGranite),  typeof(BronzeGranite),      typeof(GoldGranite),
            typeof(AgapiteGranite), typeof(VeriteGranite),      typeof(ValoriteGranite)
        };

        public override void OnComponentUsed(AddonComponent c, Mobile from)
        {
            if (from.InRange(c.Location, 3))
            {
                BaseHouse house = BaseHouse.FindHouseAt(from);

                if (house != null)
                {
                    if (CheckAccessible(from, this))
                    {
                        if (RewardCount >= 2)
                        {
                            if (Activator.CreateInstance(GraniteType[Utility.Random(GraniteType.Length)]) is Item granite)
                            {
                                granite.Amount = 2;

                                if (from.Backpack != null)
                                {
                                    from.AddToBackpack(granite);
                                    RewardCount -= 2;
                                }
                            }
                        }
                        else
                        {
                            from.SendLocalizedMessage(1094725); // There are no more resources available at this time.
                        }
                    }
                    else
                    {
                        from.SendLocalizedMessage(1061637); // You are not allowed to access this.
                    }
                }
                else
                {
                    from.SendLocalizedMessage(502092); // You must be in your house to do this.
                }
            }
            else
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
            }
        }

        public EnchantedGraniteCartAddon(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            writer.Write(m_RewardCount);
            writer.Write(NextUse);
            writer.Write((int)Level);
            writer.Write(IsRewardItem);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();


            m_RewardCount = reader.ReadInt();
            NextUse = reader.ReadDateTime();
            Level = (SecureLevel)reader.ReadInt();
            IsRewardItem = reader.ReadBool();

            StartTimer();
        }
    }
}
