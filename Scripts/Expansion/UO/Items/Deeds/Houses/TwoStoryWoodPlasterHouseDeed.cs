using Server.Multis;

namespace Server.Items
{
    public class TwoStoryWoodPlasterHouseDeed : HouseDeed
    {
        [Constructible]
        public TwoStoryWoodPlasterHouseDeed()
            : base(0x76, new Point3D(-3, 7, 0))
        {
        }

        public TwoStoryWoodPlasterHouseDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1041220;
        public override Rectangle2D[] Area => TwoStoryHouse.AreaArray;
        public override BaseHouse GetHouse(Mobile owner)
        {
            return new TwoStoryHouse(owner, 0x76);
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
