using Server.Mobiles;

namespace Server.Items
{
    public class EtherealReptalon : EtherealMount
    {
        [Constructible]
        public EtherealReptalon()
            : base(0x2d95, 0x3e90, 0x3e90, DefaultEtherealHue)
        {
            Transparent = false;
        }

        public EtherealReptalon(Serial serial)
            : base(serial)
        { }

        public override int LabelNumber => 1113812;  // Ethereal Reptalon Statuette
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
