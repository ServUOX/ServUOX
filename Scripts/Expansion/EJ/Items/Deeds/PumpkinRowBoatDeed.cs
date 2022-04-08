using Server.Items;

namespace Server.Multis
{
    public class PumpkinRowBoatDeed : BaseBoatDeed
    {
        public override int LabelNumber => 1159233;  // Pumpkin Rowboat
        public override BaseBoat Boat => new PumpkinRowBoat(BoatDirection);

        [Constructible]
        public PumpkinRowBoatDeed()
            : base(0x50, Point3D.Zero)
        {
        }

        public PumpkinRowBoatDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }
    }
}
