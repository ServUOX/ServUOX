namespace Server.Items
{
    public class AquariumNorthDeed : BaseAddonContainerDeed
    {
        public override BaseAddonContainer Addon => new Aquarium(0x3060);
        public override int LabelNumber => 1074497;// Large Aquarium (north)

        [Constructible]
        public AquariumNorthDeed()
            : base()
        {
        }

        public AquariumNorthDeed(Serial serial)
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
