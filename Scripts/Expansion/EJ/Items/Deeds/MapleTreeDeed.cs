using Server.Gumps;

namespace Server.Items
{
    public class MapleTreeDeed : BaseAddonDeed, IRewardOption
    {
        public override BaseAddon Addon => new MapleTreeAddon(m_Trunk);
        public override int LabelNumber => 1071104;  // Maple Tree

        private bool m_Trunk;

        [Constructible]
        public MapleTreeDeed()
        {
            LootType = LootType.Blessed;
        }

        public MapleTreeDeed(Serial serial)
            : base(serial)
        {
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add(0, 1071104); // Maple Tree 
            list.Add(1, 1071301); // Maple Tree (trunk)
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
