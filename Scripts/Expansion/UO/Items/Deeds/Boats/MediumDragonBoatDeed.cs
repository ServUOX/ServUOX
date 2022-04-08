using Server.Multis;

namespace Server.Items
{
    public class MediumDragonBoatDeed : BaseBoatDeed
    {
        public override int LabelNumber => 1041208;  // medium dragon ship deed
        public override BaseBoat Boat => new MediumDragonBoat(BoatDirection);

        [Constructible]
        public MediumDragonBoatDeed() : base(0xC, Point3D.Zero)
        {
        }

        public MediumDragonBoatDeed(Serial serial) : base(serial)
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
