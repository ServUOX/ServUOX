namespace Server.Items
{
    public class PlainWoodenShelfSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new PlainWoodenShelfSouthDeed();
        public override bool RetainDeedHue => true;

        [Constructible]
        public PlainWoodenShelfSouthAddon()
        {
            AddComponent(new AddonComponent(0x4C3A), 0, 0, 0);
        }

        public PlainWoodenShelfSouthAddon(Serial serial)
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

    public class PlainWoodenShelfSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new PlainWoodenShelfSouthAddon();
        public override int LabelNumber => 1154160;  // Plain Wooden Shelf (South)

        [Constructible]
        public PlainWoodenShelfSouthDeed()
        {
        }

        public PlainWoodenShelfSouthDeed(Serial serial)
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
