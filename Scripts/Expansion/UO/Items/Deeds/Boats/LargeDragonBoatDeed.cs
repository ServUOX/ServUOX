using Server.Multis;

namespace Server.Items
{
    public class LargeDragonBoatDeed : BaseBoatDeed
    {
        public override int LabelNumber => 1041210; // large dragon ship deed
        public override BaseBoat Boat => new LargeDragonBoat(BoatDirection);

        [Constructible]
        public LargeDragonBoatDeed() : base(0x14, new Point3D(0, -1, 0))
        {
        }

        public LargeDragonBoatDeed(Serial serial) : base(serial)
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
