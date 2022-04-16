namespace Server.Mobiles
{
    public class EtherealUnicorn : EtherealMount
    {
        [Constructible]
        public EtherealUnicorn()
            : base(0x25CE, 0x3E9B, 0x3EB4, DefaultEtherealHue)
        { }

        public EtherealUnicorn(Serial serial)
            : base(serial)
        { }

        public override int LabelNumber => 1049745;  // Ethereal Unicorn Statuette
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
