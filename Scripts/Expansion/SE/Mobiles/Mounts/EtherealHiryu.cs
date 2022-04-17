namespace Server.Mobiles
{
    public class EtherealHiryu : EtherealMount
    {
        [Constructible]
        public EtherealHiryu()
            : base(0x276A, 0x3E94, 0x3E94, DefaultEtherealHue)
        {
            Transparent = false;
        }

        public EtherealHiryu(Serial serial)
            : base(serial)
        { }

        public override int LabelNumber => 1113813;  // Ethereal Hiryu Statuette
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }
    }
}
