using Server.Engines.VeteranRewards;
using Server.Gumps;

namespace Server.Items
{
    public class TreeStumpDeed : BaseAddonDeed, IRewardItem, IRewardOption
    {
        private int m_ItemID;
        private bool m_IsRewardItem;
        private int m_Logs;

        [Constructible]
        public TreeStumpDeed()
            : base()
        {
            LootType = LootType.Blessed;
        }

        public TreeStumpDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1080406;// a deed for a tree stump decoration
        public override BaseAddon Addon
        {
            get
            {
                TreeStump addon = new TreeStump(m_ItemID)
                {
                    IsRewardItem = m_IsRewardItem,
                    Logs = m_Logs
                };

                return addon;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem
        {
            get => m_IsRewardItem;
            set
            {
                m_IsRewardItem = value;
                InvalidateProperties();
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Logs
        {
            get => m_Logs;
            set
            {
                m_Logs = value;
                InvalidateProperties();
            }
        }
        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_IsRewardItem)
            {
                list.Add(1076223); // 7th Year Veteran Reward
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (m_IsRewardItem && !RewardSystem.CheckIsUsableBy(from, this, null))
            {
                return;
            }

            if (IsChildOf(from.Backpack))
            {
                from.CloseGump(typeof(RewardOptionGump));
                from.SendGump(new RewardOptionGump(this));
            }
            else
            {
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version

            writer.Write(m_IsRewardItem);
            writer.Write(m_Logs);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();

            m_IsRewardItem = reader.ReadBool();
            m_Logs = reader.ReadInt();
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add(1, 1080403); // Tree Stump with Axe West
            list.Add(2, 1080404); // Tree Stump with Axe North
            list.Add(3, 1080401); // Tree Stump East
            list.Add(4, 1080402); // Tree Stump South
        }

        public void OnOptionSelected(Mobile from, int option)
        {
            switch (option)
            {
                case 1:
                    m_ItemID = 0xE56;
                    break;
                case 2:
                    m_ItemID = 0xE58;
                    break;
                case 3:
                    m_ItemID = 0xE57;
                    break;
                case 4:
                    m_ItemID = 0xE59;
                    break;
            }

            if (!Deleted)
            {
                base.OnDoubleClick(from);
            }
        }
    }
}
