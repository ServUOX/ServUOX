using Server.Engines.VeteranRewards;
using Server.Gumps;

namespace Server.Items
{

    public class MiningCartDeed : BaseAddonDeed, IRewardItem, IRewardOption
    {
        public override int LabelNumber => 1080385;// deed for a mining cart decoration

        public override BaseAddon Addon
        {
            get
            {
                MiningCart addon = new MiningCart(m_CartType)
                {
                    IsRewardItem = m_IsRewardItem,
                    Gems = m_Gems,
                    Ore = m_Ore
                };

                return addon;
            }
        }

        private MiningCartType m_CartType;

        private bool m_IsRewardItem;

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

        private int m_Gems;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Gems
        {
            get => m_Gems;
            set
            {
                m_Gems = value;
                InvalidateProperties();
            }
        }

        private int m_Ore;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Ore
        {
            get => m_Ore;
            set
            {
                m_Ore = value;
                InvalidateProperties();
            }
        }

        [Constructible]
        public MiningCartDeed()
            : base()
        {
            LootType = LootType.Blessed;
        }

        public MiningCartDeed(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_IsRewardItem)
            {
                list.Add(1080457); // 10th Year Veteran Reward
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

            writer.WriteEncodedInt(0);

            writer.Write(m_IsRewardItem);
            writer.Write(m_Gems);
            writer.Write(m_Ore);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();

            m_IsRewardItem = reader.ReadBool();
            m_Gems = reader.ReadInt();
            m_Ore = reader.ReadInt();
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add((int)MiningCartType.OreSouth, 1080391);
            list.Add((int)MiningCartType.OreEast, 1080390);
            list.Add((int)MiningCartType.GemSouth, 1080500);
            list.Add((int)MiningCartType.GemEast, 1080499);
        }

        public void OnOptionSelected(Mobile from, int choice)
        {
            m_CartType = (MiningCartType)choice;

            if (!Deleted)
            {
                base.OnDoubleClick(from);
            }
        }
    }
}
