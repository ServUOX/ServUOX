using Server.Gumps;

namespace Server.Items
{
    public class WoodworkersBenchDeed : BaseAddonDeed, IRewardOption
    {
        public override int LabelNumber => 1026641;  // Woodworker's Bench
        public override BaseAddon Addon => new WoodworkersBench(m_East);

        private bool m_East;

        [Constructible]
        public WoodworkersBenchDeed()
            : base()
        {
            LootType = LootType.Blessed;
        }

        public WoodworkersBenchDeed(Serial serial)
            : base(serial)
        {
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add(0, 1071502); // Woodworker's Bench (South)
            list.Add(1, 1071503); // Woodworker's Bench (East)
        }

        public void OnOptionSelected(Mobile from, int choice)
        {
            m_East = choice == 1;

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
            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }
    }
}
