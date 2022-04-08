using Server.Multis;

namespace Server.Items
{
    public class MediumBoatDeed : BaseBoatDeed
    {
        public override int LabelNumber => 1041207;  // medium ship deed
        public override BaseBoat Boat => new MediumBoat(BoatDirection);

        [Constructible]
        public MediumBoatDeed() : base(0x8, Point3D.Zero)
        {
        }

        public MediumBoatDeed(Serial serial) : base(serial)
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
