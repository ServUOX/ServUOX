namespace Server.Items
{
    [Flipable(0xC6D, 0xC6E)]
    public class Onion : Food
    {
        [Constructible]
        public Onion()
            : this(1)
        {
        }

        [Constructible]
        public Onion(int amount)
            : base(amount, 0xc6d)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Onion(Serial serial)
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
