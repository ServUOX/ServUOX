namespace Server.Items
{
    public class CheeseSlice : Food
    {
        public override double DefaultWeight => 0.1;

        [Constructible]
        public CheeseSlice()
            : this(1)
        {
        }

        [Constructible]
        public CheeseSlice(int amount)
            : base(amount, 0x97C)
        {
            FillFactor = 1;
        }

        public CheeseSlice(Serial serial)
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
