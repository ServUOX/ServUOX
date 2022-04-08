using Server.Multis;

namespace Server.Items
{
    public class TowerDeed : HouseDeed
    {
        [Constructible]
        public TowerDeed()
            : base(0x7A, new Point3D(0, 7, 0))
        {
        }

        public TowerDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1041222;
        public override Rectangle2D[] Area => Tower.AreaArray;
        public override BaseHouse GetHouse(Mobile owner)
        {
            return new Tower(owner);
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
