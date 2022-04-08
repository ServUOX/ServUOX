namespace Server.Items
{
    public class Cookies : Food
    {
        [Constructible]
        public Cookies()
            : base(0x160b)
        {
            Stackable = Core.ML;
            Weight = 1.0;
            FillFactor = 4;
        }

        public Cookies(Serial serial)
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
