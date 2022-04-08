using Server.Multis;

namespace Server.Items
{
    public class LargeBoatDeed : BaseBoatDeed
    {
        public override int LabelNumber => 1041209;  // large ship deed
        public override BaseBoat Boat => new LargeBoat(BoatDirection);

        [Constructible]
        public LargeBoatDeed() : base(0x10, new Point3D(0, -1, 0))
        {
        }

        public LargeBoatDeed(Serial serial) : base(serial)
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
