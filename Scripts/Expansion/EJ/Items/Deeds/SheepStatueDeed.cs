using Server.Engines.VeteranRewards;

namespace Server.Items
{

    public class SheepStatueDeed : BaseAddonDeed, IRewardItem
    {
        private bool m_IsRewardItem;
        private int m_ResourceCount;

        [Constructible]
        public SheepStatueDeed()
            : base()
        {
            LootType = LootType.Blessed;
        }

        public SheepStatueDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1151835;// Sheep Statue Deed

        public override BaseAddon Addon
        {
            get
            {
                SheepStatue addon = new SheepStatue(m_ResourceCount > 0 ? 0x4A94 : 0x4A95)
                {
                    IsRewardItem = m_IsRewardItem,
                    ResourceCount = m_ResourceCount
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
        public int ResourceCount
        {
            get => m_ResourceCount;
            set
            {
                m_ResourceCount = value;
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

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.
            }
            else
            {
                base.OnDoubleClick(from);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version

            writer.Write(m_IsRewardItem);
            writer.Write(m_ResourceCount);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();

            m_IsRewardItem = reader.ReadBool();
            m_ResourceCount = reader.ReadInt();
        }
    }
}
