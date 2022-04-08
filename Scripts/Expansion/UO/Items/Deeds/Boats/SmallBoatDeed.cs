using Server.Multis;

namespace Server.Items
{
    public class SmallBoatDeed : BaseBoatDeed
    {
        public override int LabelNumber => 1041205;  // small ship deed
        public override BaseBoat Boat => new SmallBoat(BoatDirection);

        [Constructible]
        public SmallBoatDeed() : base(0x0, Point3D.Zero)
        {
        }

        public SmallBoatDeed(Serial serial) : base(serial)
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
