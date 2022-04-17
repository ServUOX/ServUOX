using Server.Mobiles;

namespace Server.Items
{
    public class EtherealCuSidhe : EtherealMount
    {
        [Constructible]
        public EtherealCuSidhe()
            : base(0x2D96, 0x3E91, 0x3E91, DefaultEtherealHue)
        {
            Transparent = false;
        }

        public EtherealCuSidhe(Serial serial)
            : base(serial)
        { }

        public override int LabelNumber => 1080386;  // Ethereal Cu Sidhe Statuette
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
