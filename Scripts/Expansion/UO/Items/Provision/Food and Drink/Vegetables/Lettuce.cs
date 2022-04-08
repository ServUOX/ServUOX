namespace Server.Items
{
    [Flipable(0xC70, 0xC71)]
    public class Lettuce : Food
    {
        [Constructible]
        public Lettuce()
            : this(1)
        {
        }

        [Constructible]
        public Lettuce(int amount)
            : base(amount, 0xc70)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Lettuce(Serial serial)
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
