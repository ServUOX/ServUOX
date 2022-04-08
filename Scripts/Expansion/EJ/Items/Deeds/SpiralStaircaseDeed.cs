using Server.ContextMenus;
using Server.Gumps;

namespace Server.Items
{
    public class SpiralStaircaseDeed : BaseAddonDeed, IRewardOption
    {
        public override BaseAddon Addon => new SpiralStaircaseAddon(m_Topper);
        public override int LabelNumber => 1159429;  // spiral staircase

        private bool m_Topper;

        [Constructible]
        public SpiralStaircaseDeed()
        {
            LootType = LootType.Blessed;
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add(0, 1159431); // Without Topper
            list.Add(1, 1159432); // With Topper
        }

        public void OnOptionSelected(Mobile from, int choice)
        {
            m_Topper = choice == 1;

            if (!Deleted)
            {
                base.OnDoubleClick(from);
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.CloseGump(typeof(AddonOptionGump));
                from.SendGump(new AddonOptionGump(this, 1159430, 300, 180)); // Choose an option:
            }
            else
            {
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.       	
            }
        }

        public SpiralStaircaseDeed(Serial serial)
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
}
