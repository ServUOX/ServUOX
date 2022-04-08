using Server.Gumps;

namespace Server.Items
{
    public class MetalLadderAddon2 : BaseAddon, IDyable
    {
        public override BaseAddonDeed Deed => new MetalLadderDeed2();

        [Constructible]
        public MetalLadderAddon2(MetalLadderType type)
        {
            switch (type)
            {
                case MetalLadderType.SouthCastle:
                    AddComponent(new LocalizedAddonComponent(0x3F28, 1076791), 0, 1, 28);
                    AddComponent(new LocalizedAddonComponent(0xA559, 1076791), 0, 2, 20);
                    AddComponent(new AddonComponent(0xA557), 0, 0, 0);
                    break;
                case MetalLadderType.EastCastle:
                    AddComponent(new LocalizedAddonComponent(0x3F28, 1076791), 1, 0, 28);
                    AddComponent(new LocalizedAddonComponent(0xA55A, 1076791), 2, 0, 20);
                    AddComponent(new AddonComponent(0xA558), 0, 0, 0);
                    break;
                case MetalLadderType.NorthCastle:
                    AddComponent(new LocalizedAddonComponent(0x3F28, 1076791), 0, -1, 28);
                    AddComponent(new LocalizedAddonComponent(0x3DB6, 1076791), 0, -2, 20);
                    AddComponent(new LocalizedAddonComponent(0xA55C, 1076791), 0, 0, 0);
                    break;
                case MetalLadderType.WestCastle:
                    AddComponent(new LocalizedAddonComponent(0x3F28, 1076791), -1, 0, 28);
                    AddComponent(new LocalizedAddonComponent(0x3DB7, 1076791), -2, 0, 20);
                    AddComponent(new LocalizedAddonComponent(0xA55B, 1076791), 0, 0, 0);
                    break;
                case MetalLadderType.South:
                    AddComponent(new LocalizedAddonComponent(0xA557, 1076287), 0, 0, 0);
                    break;
                case MetalLadderType.East:
                    AddComponent(new LocalizedAddonComponent(0xA558, 1076287), 0, 0, 0);
                    break;
                case MetalLadderType.North:
                    AddComponent(new LocalizedAddonComponent(0xA55C, 1076287), 0, 0, 0);
                    break;
                case MetalLadderType.West:
                    AddComponent(new LocalizedAddonComponent(0xA55B, 1076287), 0, 0, 0);
                    break;
            }
        }

        public virtual bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
            {
                return false;
            }

            Hue = sender.DyedHue;
            return true;
        }

        public MetalLadderAddon2(Serial serial)
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
