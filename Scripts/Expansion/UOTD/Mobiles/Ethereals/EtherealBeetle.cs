namespace Server.Mobiles
{
    public class EtherealBeetle : EtherealMount
    {
        [Constructible]
        public EtherealBeetle()
            : base(0x260F, 0x3E97, 0x3EBC, DefaultEtherealHue)
        { }

        public EtherealBeetle(Serial serial)
            : base(serial)
        { }

        public override int LabelNumber => 1049748;  // Ethereal Beetle Statuette
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
