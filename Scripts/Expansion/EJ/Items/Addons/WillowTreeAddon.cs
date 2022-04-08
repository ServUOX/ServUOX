namespace Server.Items
{
    public class WillowTreeAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WillowTreeDeed();

        [Constructible]
        public WillowTreeAddon()
        {
            AddComponent(new LocalizedAddonComponent(0x224A, 1071105), 0, 0, 0);
        }

        public WillowTreeAddon(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }
    }
}
