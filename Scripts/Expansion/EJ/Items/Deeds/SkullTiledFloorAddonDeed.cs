using Server.Gumps;

namespace Server.Items
{
    public class SkullTiledFloorAddonDeed : BaseAddonDeed, IRewardOption
    {
        private bool m_East;

        [Constructible]
        public SkullTiledFloorAddonDeed()
        {
        }

        public SkullTiledFloorAddonDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1159020;  // Skull Tiled Floor

        public override BaseAddon Addon => new SkullTiledFloorAddon(m_East);

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

        public void GetOptions(RewardOptionList list)
        {
            list.Add(1, 1150124); // South
            list.Add(2, 1150123); // East
        }

        public void OnOptionSelected(Mobile from, int option)
        {
            switch (option)
            {
                case 1:
                    m_East = false;
                    break;
                case 2:
                    m_East = true;
                    break;
            }

            if (!Deleted)
            {
                base.OnDoubleClick(from);
            }
        }
    }
}
