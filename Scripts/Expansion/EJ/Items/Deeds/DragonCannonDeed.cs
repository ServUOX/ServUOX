using Server.Gumps;

namespace Server.Items
{
    public class DragonCannonDeed : BaseAddonDeed, IRewardOption
    {
        public override int LabelNumber => 1158926;  // Decorative Dragon Cannon
        public override BaseAddon Addon => new DragonCannon(_Direction);

        private DirectionType _Direction;

        [Constructible]
        public DragonCannonDeed()
        {
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add((int)DirectionType.North, 1075389); // North
            list.Add((int)DirectionType.West, 1075390); // West
            list.Add((int)DirectionType.South, 1075386); // South
            list.Add((int)DirectionType.East, 1075387); // East
        }

        public void OnOptionSelected(Mobile from, int choice)
        {
            _Direction = (DirectionType)choice;

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
                from.SendGump(new RewardOptionGump(this, 1076783)); // Please select your shadow altar position
            }
            else
            {
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.
            }
        }

        public DragonCannonDeed(Serial serial)
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
            reader.ReadInt();
        }
    }
}
