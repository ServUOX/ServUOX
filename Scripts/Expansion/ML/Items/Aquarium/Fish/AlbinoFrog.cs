namespace Server.Items
{
    public class AlbinoFrog : BaseFish
    {
        [Constructible]
        public AlbinoFrog()
            : base(0x3B0D)
        {
            Hue = 0x47E;
        }

        public AlbinoFrog(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1073824; // an albino frog

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
