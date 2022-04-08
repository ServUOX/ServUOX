namespace Server.Items
{
    public class PlantTapestryEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new PlantTapestryEastDeed();
        public override bool RetainDeedHue => true;

        [Constructible]
        public PlantTapestryEastAddon()
        {
            AddComponent(new AddonComponent(0x4C9F), 0, 0, 0);
            AddComponent(new AddonComponent(0x4C9E), 0, 1, 0);
        }

        public PlantTapestryEastAddon(Serial serial)
            : base(serial)
        {
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

    public class PlantTapestryEastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new PlantTapestryEastAddon();
        public override int LabelNumber => 1154147;  // Plant Tapestry (East)

        [Constructible]
        public PlantTapestryEastDeed()
        {
        }

        public PlantTapestryEastDeed(Serial serial)
            : base(serial)
        {
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
