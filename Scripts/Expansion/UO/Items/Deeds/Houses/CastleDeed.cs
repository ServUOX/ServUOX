using Server.Multis;

namespace Server.Items
{
    public class CastleDeed : HouseDeed
    {
        [Constructible]
        public CastleDeed()
            : base(0x7E, new Point3D(0, 16, 0))
        {
        }

        public CastleDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1041224;
        public override Rectangle2D[] Area => Castle.AreaArray;
        public override BaseHouse GetHouse(Mobile owner)
        {
            return new Castle(owner);
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
