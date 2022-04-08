namespace Server.Items
{
    public class DragonCannon : BaseAddon
    {
        public override BaseAddonDeed Deed => new DragonCannonDeed();

        [Constructible]
        public DragonCannon()
            : this(DirectionType.South)
        {
        }

        [Constructible]
        public DragonCannon(DirectionType direction)
        {
            switch (direction)
            {
                case DirectionType.North:
                    AddComponent(new AddonComponent(0x44F4), 0, 0, 0);
                    AddComponent(new AddonComponent(0x44F3), 0, 1, 0);
                    AddComponent(new AddonComponent(0x44F5), 0, -1, 0);
                    break;
                case DirectionType.West:
                    AddComponent(new AddonComponent(0x424A), 0, 0, 0);
                    AddComponent(new AddonComponent(0x4223), 1, 0, 0);
                    AddComponent(new AddonComponent(0x418F), -1, 0, 0);
                    break;
                case DirectionType.South:
                    AddComponent(new AddonComponent(0x4221), 0, 0, 0);
                    AddComponent(new AddonComponent(0x4222), 0, 1, 0);
                    AddComponent(new AddonComponent(0x4220), 0, -1, 0);
                    break;
                case DirectionType.East:
                    AddComponent(new AddonComponent(0x44F7), 0, 0, 0);
                    AddComponent(new AddonComponent(0x44F6), 1, 0, 0);
                    AddComponent(new AddonComponent(0x44F8), -1, 0, 0);
                    break;
            }
        }

        public DragonCannon(Serial serial)
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
            reader.ReadInt();
        }
    }
}
