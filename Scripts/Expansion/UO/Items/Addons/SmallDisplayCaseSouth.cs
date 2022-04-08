namespace Server.Items
{
    public class SmallDisplayCaseSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new SmallDisplayCaseSouthDeed();

        [Constructible]
        public SmallDisplayCaseSouthAddon()
        {
            AddComponent(new AddonComponent(0x0B0A), 0, 0, 0);
            AddComponent(new AddonComponent(0x0B0C), 0, 0, 3);
        }

        public SmallDisplayCaseSouthAddon(Serial serial)
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

    public class SmallDisplayCaseSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new SmallDisplayCaseSouthAddon();
        public override int LabelNumber => 1155842;  // Small Display Case (South)

        [Constructible]
        public SmallDisplayCaseSouthDeed()
        {
        }

        public SmallDisplayCaseSouthDeed(Serial serial)
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
