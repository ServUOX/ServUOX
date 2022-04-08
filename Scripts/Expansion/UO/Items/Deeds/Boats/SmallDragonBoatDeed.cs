using Server.Multis;

namespace Server.Items
{
    public class SmallDragonBoatDeed : BaseBoatDeed
    {
        public override int LabelNumber => 1041206;  // small dragon ship deed
        public override BaseBoat Boat => new SmallDragonBoat(BoatDirection);

        [Constructible]
        public SmallDragonBoatDeed() : base(0x4, Point3D.Zero)
        {
        }

        public SmallDragonBoatDeed(Serial serial) : base(serial)
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
