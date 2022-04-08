using Server.Gumps;

namespace Server.Items
{
    public class MetalLadderDeed2 : BaseAddonDeed, IRewardOption
    {
        public override BaseAddon Addon => new MetalLadderAddon2(_Direction);
        public override int LabelNumber => 1159445;  // metal ladder

        private MetalLadderType _Direction;

        [Constructible]
        public MetalLadderDeed2()
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.CloseGump(typeof(AddonOptionGump));
                from.SendGump(new AddonOptionGump(this, 1154194, 300, 260)); // Choose a Facing:
            }
            else
            {
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.
            }
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add((int)MetalLadderType.SouthCastle, 1076794);
            list.Add((int)MetalLadderType.EastCastle, 1076795);
            list.Add((int)MetalLadderType.NorthCastle, 1076792);
            list.Add((int)MetalLadderType.WestCastle, 1076793);
            list.Add((int)MetalLadderType.South, 1075386);
            list.Add((int)MetalLadderType.East, 1075387);
            list.Add((int)MetalLadderType.North, 1075389);
            list.Add((int)MetalLadderType.West, 1075390);
        }

        public void OnOptionSelected(Mobile from, int choice)
        {
            _Direction = (MetalLadderType)choice;

            if (!Deleted)
            {
                base.OnDoubleClick(from);
            }
        }

        public MetalLadderDeed2(Serial serial)
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
