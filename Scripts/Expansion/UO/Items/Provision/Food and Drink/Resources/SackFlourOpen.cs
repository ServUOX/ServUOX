using Server.Targeting;

namespace Server.Items
{
    public class SackFlourOpen : Item
    {
        public override int LabelNumber => 1024166;  // open sack of flour

        [Constructible]
        public SackFlourOpen() : base(0x103A)
        {
            Weight = 4.0;
        }

        public SackFlourOpen(Serial serial) : base(serial)
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
