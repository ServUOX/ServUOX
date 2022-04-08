using Server.Gumps;

namespace Server.Items
{
    public class SkullTiledFloorAddon : BaseAddon
    {
        [Constructible]
        public SkullTiledFloorAddon(bool east)
            : base()
        {
            if (!east)
            {
                AddComponent(new LocalizedAddonComponent(0xA34F, 1125827), 0, 0, 0);
                AddComponent(new LocalizedAddonComponent(0xA350, 1125827), -1, 0, 0);
                AddComponent(new LocalizedAddonComponent(0xA34E, 1125827), 1, 0, 0);
                AddComponent(new LocalizedAddonComponent(0xA34B, 1125827), 1, 1, 0);
                AddComponent(new LocalizedAddonComponent(0xA34C, 1125827), 0, 1, 0);
                AddComponent(new LocalizedAddonComponent(0xA34D, 1125827), -1, 1, 0);
                AddComponent(new LocalizedAddonComponent(0xA351, 1125827), 1, -1, 0);
                AddComponent(new LocalizedAddonComponent(0xA352, 1125827), 0, -1, 0);
                AddComponent(new LocalizedAddonComponent(0xA353, 1125827), -1, -1, 0);
            }
            else
            {
                AddComponent(new LocalizedAddonComponent(0xA358, 1125827), 0, 0, 0);
                AddComponent(new LocalizedAddonComponent(0xA357, 1125827), 0, 1, 0);
                AddComponent(new LocalizedAddonComponent(0xA359, 1125827), 0, -1, 0);
                AddComponent(new LocalizedAddonComponent(0xA354, 1125827), 1, 1, 0);
                AddComponent(new LocalizedAddonComponent(0xA355, 1125827), 1, 0, 0);
                AddComponent(new LocalizedAddonComponent(0xA356, 1125827), 1, -1, 0);
                AddComponent(new LocalizedAddonComponent(0xA35A, 1125827), -1, 1, 0);
                AddComponent(new LocalizedAddonComponent(0xA35B, 1125827), -1, 0, 0);
                AddComponent(new LocalizedAddonComponent(0xA35C, 1125827), -1, -1, 0);
            }
        }

        public SkullTiledFloorAddon(Serial serial)
            : base(serial)
        {
        }

        public override BaseAddonDeed Deed => new SkullTiledFloorAddonDeed();

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
