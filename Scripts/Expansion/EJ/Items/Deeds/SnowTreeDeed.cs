using Server.Gumps;

namespace Server.Items
{
    public class SnowTreeDeed : BaseAddonDeed, IRewardOption
    {
        public override BaseAddon Addon => new SnowTreeAddon(m_Trunk);
        public override int LabelNumber => 1071103;  // Snow Tree

        private bool m_Trunk;

        [Constructible]
        public SnowTreeDeed()
        {
            LootType = LootType.Blessed;
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add(0, 1071103); // Snow Tree
            list.Add(1, 1071300); // Snow Tree (trunk)
        }

        public void OnOptionSelected(Mobile from, int choice)
        {
            m_Trunk = choice == 1;

            if (!Deleted)
            {
                base.OnDoubleClick(from);
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
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

        public SnowTreeDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }
    }
}
