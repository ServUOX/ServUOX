
namespace Server.Items
{
    public class SplitCoconut : Food
    {
        [Constructible]
        public SplitCoconut()
            : this(1)
        {
        }

        [Constructible]
        public SplitCoconut(int amount)
            : base(amount, 0x1725)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public SplitCoconut(Serial serial)
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
