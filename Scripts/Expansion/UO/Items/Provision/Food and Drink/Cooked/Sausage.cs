namespace Server.Items
{
    public class Sausage : Food
    {
        [Constructible]
        public Sausage()
            : this(1)
        {
        }

        [Constructible]
        public Sausage(int amount)
            : base(amount, 0x9C0)
        {
            Weight = 1.0;
            FillFactor = 4;
        }

        public Sausage(Serial serial)
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
