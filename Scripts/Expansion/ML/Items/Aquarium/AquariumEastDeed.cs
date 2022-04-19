namespace Server.Items
{
    public class AquariumEastDeed : BaseAddonContainerDeed
    {
        public override BaseAddonContainer Addon => new Aquarium(0x3062);
        public override int LabelNumber => 1074501;// Large Aquarium (east)

        [Constructible]
        public AquariumEastDeed()
            : base()
        {
        }

        public AquariumEastDeed(Serial serial)
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
