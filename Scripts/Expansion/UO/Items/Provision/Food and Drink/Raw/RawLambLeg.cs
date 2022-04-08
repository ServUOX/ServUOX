namespace Server.Items
{
    public class RawLambLeg : CookableFood
    {
        [Constructible]
        public RawLambLeg()
            : this(1)
        {
        }

        [Constructible]
        public RawLambLeg(int amount)
            : base(0x1609, 10)
        {
            Stackable = true;
            Amount = amount;
        }

        public RawLambLeg(Serial serial)
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
            return new LambLeg();
        }
    }
}
