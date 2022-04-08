namespace Server.Items
{
    public class WillowTreeDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WillowTreeAddon();
        public override int LabelNumber => 1071105;  // Willow Tree

        [Constructible]
        public WillowTreeDeed()
        {
            LootType = LootType.Blessed;
        }

        public WillowTreeDeed(Serial serial)
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
