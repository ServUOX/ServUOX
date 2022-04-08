using Server.Gumps;

namespace Server.Items
{
    public class FallenLogDeed : BaseAddonDeed, IRewardOption
    {
        public override int LabelNumber => 1071088;  // Fallen Log
        public override BaseAddon Addon => new FallenLogAddon(m_East);

        private bool m_East;

        [Constructible]
        public FallenLogDeed()
            : base()
        {
            LootType = LootType.Blessed;
        }

        public FallenLogDeed(Serial serial)
            : base(serial)
        {
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add(0, 1116332); // South 
            list.Add(1, 1116333); // East
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
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
