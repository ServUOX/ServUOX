namespace Server.Items
{
    public class CheeseWedge : Food
    {
        public override double DefaultWeight => 0.1;

        [Constructible]
        public CheeseWedge()
            : this(1)
        {
        }

        [Constructible]
        public CheeseWedge(int amount)
            : base(amount, 0x97D)
        {
            FillFactor = 3;
        }

        public CheeseWedge(Serial serial)
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
