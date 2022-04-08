namespace Server.Items
{
    public class RawRibs : CookableFood
    {
        [Constructible]
        public RawRibs()
            : this(1)
        {
        }

        [Constructible]
        public RawRibs(int amount)
            : base(0x9F1, 10)
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        public RawRibs(Serial serial)
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

        public override Food Cook()
        {
            return new Ribs();
        }
    }
}
