namespace Server.Mobiles
{
    public class EtherealKirin : EtherealMount
    {
        [Constructible]
        public EtherealKirin()
            : base(0x25A0, 0x3E9C, 0x3EAD, DefaultEtherealHue)
        { }

        public EtherealKirin(Serial serial)
            : base(serial)
        { }

        public override int LabelNumber => 1049746;  // Ethereal Ki-Rin Statuette
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
