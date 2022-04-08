using Server.Multis;

namespace Server.Items
{
    public class TwoStoryStonePlasterHouseDeed : HouseDeed
    {
        [Constructible]
        public TwoStoryStonePlasterHouseDeed()
            : base(0x78, new Point3D(-3, 7, 0))
        {
        }

        public TwoStoryStonePlasterHouseDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1041221;
        public override Rectangle2D[] Area => TwoStoryHouse.AreaArray;
        public override BaseHouse GetHouse(Mobile owner)
        {
            return new TwoStoryHouse(owner, 0x78);
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
