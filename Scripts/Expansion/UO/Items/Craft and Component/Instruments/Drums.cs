namespace Server.Items
{
    public class Drums : BaseInstrument
    {
        [Constructible]
        public Drums()
            : base(0xE9C, 0x38, 0x39)
        {
            Weight = 4.0;
        }

        public Drums(Serial serial)
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

            if (Weight == 3.0)
            {
                Weight = 4.0;
            }
        }
    }
}
