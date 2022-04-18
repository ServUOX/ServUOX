using System;
using Server.Engines.VeteranRewards;
using Server.Gumps;

namespace Server.Items
{
    [TypeAlias("Server.Items.SkullRugSouthAddonDeed", "Server.Items.SkullRugEastAddonDeed")]
    public class SkullRugAddonDeed : BaseAddonDeed, IRewardOption, IRewardItem
    {
        public override BaseAddon Addon
        {
            get
            {
                SkullRugAddon addon = new SkullRugAddon(RugType, m_ResourceCount, NextResourceCount)
                {
                    IsRewardItem = m_IsRewardItem
                };

                return addon;
            }
        }

        public override int LabelNumber
        {
            get
            {
                switch ((int)RugType)
                {
                    default: return 1150046;
                    case 0:
                    case 2: return 1150047;
                }
            }
        }

        private DateTime NextResourceCount;
        private int m_ResourceCount;
        private bool m_IsRewardItem;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem
        {
            get
            {
                return m_IsRewardItem;
            }
            set
            {
                m_IsRewardItem = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ResourceCount
        {
            get
            {
                return m_ResourceCount;
            }
            set
            {
                m_ResourceCount = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public RugType RugType { get; set; }

        [Constructible]
        public SkullRugAddonDeed()
            : this(RugType.EastLarge)
        {
        }

        [Constructible]
        public SkullRugAddonDeed(RugType type)
            : this(type, 0, DateTime.UtcNow)
        {
        }

        [Constructible]
        public SkullRugAddonDeed(RugType type, int resCount, DateTime nextuse)
        {
            RugType = type;
            NextResourceCount = nextuse;
            ResourceCount = resCount;

            LootType = LootType.Blessed;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.CloseGump(typeof(RewardOptionGump));
                from.SendGump(new RewardOptionGump(this, 1076583)); // Please select your rug size
            }
            else
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.       	
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add(1, "Skull Rug East 7x7");
            list.Add(2, "Skull Rug South 7x7");
            list.Add(3, "Skull Rug East 3x5");
            list.Add(4, "Skull Rug South 3x5");
        }

        public void OnOptionSelected(Mobile from, int choice)
        {
            RugType = (RugType)choice - 1;

            if (!Deleted && IsChildOf(from.Backpack))
                base.OnDoubleClick(from);
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_IsRewardItem)
                list.Add(1080457); // 10th Year Veteran Reward
        }

        public SkullRugAddonDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(3); // Version

            writer.Write(m_ResourceCount);

            writer.Write(m_IsRewardItem);
            writer.Write(NextResourceCount);
            writer.Write((int)RugType);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 3:
                    m_ResourceCount = reader.ReadInt();
                    goto case 2;
                case 2:
                    m_IsRewardItem = reader.ReadBool();
                    NextResourceCount = reader.ReadDateTime();
                    RugType = (RugType)reader.ReadInt();
                    break;
                case 1:
                    NextResourceCount = reader.ReadDateTime();
                    RugType = (RugType)reader.ReadInt();
                    break;
                case 0:
                    NextResourceCount = reader.ReadDateTime();
                    break;
                default:
                    break;
            }
        }
    }
}
