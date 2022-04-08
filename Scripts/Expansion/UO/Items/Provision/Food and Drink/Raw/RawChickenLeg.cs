namespace Server.Items
{
    public class RawChickenLeg : CookableFood
    {
        [Constructible]
        public RawChickenLeg()
            : base(0x1607, 10)
        {
            Weight = 1.0;
            Stackable = true;
        }

        public RawChickenLeg(Serial serial)
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
            return new ChickenLeg();
        }
    }
}
