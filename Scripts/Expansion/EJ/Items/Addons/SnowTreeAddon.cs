using Server.Gumps;

namespace Server.Items
{
    public class SnowTreeAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new SnowTreeDeed();

        [Constructible]
        public SnowTreeAddon(bool trunk)
        {
            AddComponent(new LocalizedAddonComponent(0xDA0, 1071103), 0, 0, 0);

            if (!trunk)
            {
                var comp = new LocalizedAddonComponent(0xD9D, 1071103)
                {
                    Hue = 1153
                };
                AddComponent(comp, 0, 0, 0);
            }
        }

        public SnowTreeAddon(Serial serial)
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
