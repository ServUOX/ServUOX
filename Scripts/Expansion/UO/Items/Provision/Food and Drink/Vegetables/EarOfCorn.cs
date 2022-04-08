namespace Server.Items
{
    [Flipable(0xC7F, 0xC81)]
    public class EarOfCorn : Food
    {
        [Constructible]
        public EarOfCorn()
            : this(1)
        {
        }

        [Constructible]
        public EarOfCorn(int amount)
            : base(amount, 0xC81)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public EarOfCorn(Serial serial)
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
