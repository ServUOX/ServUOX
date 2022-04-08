using Server.Gumps;

namespace Server.Items
{
    public class MapleTreeAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new MapleTreeDeed();

        [Constructible]
        public MapleTreeAddon(bool trunk)
        {
            AddComponent(new LocalizedAddonComponent(0x247D, 1071104), 0, 0, 0);

            if (!trunk)
            {
                AddComponent(new LocalizedAddonComponent(0x36A1, 1071104), 0, 0, 0);
            }
        }

        public MapleTreeAddon(Serial serial)
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
