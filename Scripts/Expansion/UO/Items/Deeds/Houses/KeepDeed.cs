using Server.Multis;

namespace Server.Items
{
    public class KeepDeed : HouseDeed
    {
        [Constructible]
        public KeepDeed()
            : base(0x7C, new Point3D(0, 11, 0))
        {
        }

        public KeepDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1041223;
        public override Rectangle2D[] Area => Keep.AreaArray;
        public override BaseHouse GetHouse(Mobile owner)
        {
            return new Keep(owner);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            _ = reader.ReadInt();
        }
    }
}
