using Server.Multis;

namespace Server.Items
{
    public class LargeMarbleDeed : HouseDeed
    {
        [Constructible]
        public LargeMarbleDeed()
            : base(0x96, new Point3D(-4, 7, 0))
        {
        }

        public LargeMarbleDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1041236;
        public override Rectangle2D[] Area => LargeMarbleHouse.AreaArray;
        public override BaseHouse GetHouse(Mobile owner)
        {
            return new LargeMarbleHouse(owner);
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
