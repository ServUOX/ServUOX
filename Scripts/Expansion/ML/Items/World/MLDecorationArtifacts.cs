using System;
using Server;

namespace Server.Items
{
    public class AncientShipModelOfTheHMSCape : Item
    {
        public override bool IsArtifact => true;
        public override int LabelNumber => 1063476;

        [Constructible]
        public AncientShipModelOfTheHMSCape()
            : base(0x14F3)
        {
            Name = "an Ancient Ship Model Of The HMS Cape";
            Hue = 0x37B;
        }

        public AncientShipModelOfTheHMSCape(Serial serial)
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

            int version = reader.ReadInt();
        }
    }
}
