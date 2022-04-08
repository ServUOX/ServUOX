namespace Server.Items
{
    public class DecorativeBlackwidowAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new DecorativeBlackwidowDeed();

        [Constructible]
        public DecorativeBlackwidowAddon()
        {
            AddComponent(new AddonComponent(40360), 1, 1, 0);
            AddComponent(new AddonComponent(40362), 1, 0, 0);
            AddComponent(new AddonComponent(40361), 0, 1, 0);
            AddComponent(new AddonComponent(40363), 0, 0, 0);
        }

        public DecorativeBlackwidowAddon(Serial serial)
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
