namespace Server.Items
{
    public class CheeseWheel : Food
    {
        public override double DefaultWeight => 0.1;

        [Constructible]
        public CheeseWheel()
            : this(1)
        {
        }

        [Constructible]
        public CheeseWheel(int amount)
            : base(amount, 0x97E)
        {
            FillFactor = 3;
        }

        public CheeseWheel(Serial serial)
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
