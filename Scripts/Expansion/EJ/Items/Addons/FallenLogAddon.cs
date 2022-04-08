using Server.Gumps;

namespace Server.Items
{
    public class FallenLogAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new FallenLogDeed();

        [Constructible]
        public FallenLogAddon()
            : this(true)
        {
        }

        [Constructible]
        public FallenLogAddon(bool east)
            : base()
        {
            if (east)
            {
                AddComponent(new AddonComponent(0x0CF5), -1, 0, 0);
                AddComponent(new AddonComponent(0x0CF6), 0, 0, 0);
                AddComponent(new AddonComponent(0x0CF7), 1, 0, 0);
            }
            else
            {
                AddComponent(new AddonComponent(0x0CF4), 0, 0, 0);
                AddComponent(new AddonComponent(0x0CF3), 0, -1, 0);
            }
        }

        public FallenLogAddon(Serial serial)
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
