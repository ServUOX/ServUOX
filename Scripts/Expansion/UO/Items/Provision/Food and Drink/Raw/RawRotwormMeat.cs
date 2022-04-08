namespace Server.Items
{
    public class RawRotwormMeat : CookableFood
    {
        [Constructible]
        public RawRotwormMeat()
            : this(1)
        {
        }

        [Constructible]
        public RawRotwormMeat(int amount)
            : base(0x2DB9, 10)
        {
            Stackable = true;
            Weight = 0.1;
            Amount = amount;
        }

        public RawRotwormMeat(Serial serial)
            : base(serial)
        {
        }

        public override Food Cook()
        {
            return null;
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
