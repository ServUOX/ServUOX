using Server.ContextMenus;
using Server.Gumps;
using Server.Mobiles;
using Server.Multis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Items
{
    public class SpiralStaircaseAddon : BaseAddon, ISecurable, IDyable
    {
        public override BaseAddonDeed Deed => new SpiralStaircaseDeed();

        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level { get; set; }

        [Constructible]
        public SpiralStaircaseAddon(bool topper)
        {
            if (topper)
            {
                AddComponent(new AddonComponent(42319), 0, 1, 0);
                AddComponent(new AddonComponent(42320), 1, 0, 0);
                AddComponent(new AddonComponent(42333), 1, 1, 20);
                AddComponent(new AddonComponent(42334), 0, 1, 20);
                AddComponent(new AddonComponent(42335), 1, 0, 20);
                AddComponent(new TeleporterComponent(42336, Direction.Up), 0, 0, 20);
                AddComponent(new TeleporterComponent(42318, Direction.Down), 1, 1, 0);
            }
            else
            {
                AddComponent(new TeleporterComponent(42321, Direction.Up), 0, 0, 0);
                AddComponent(new AddonComponent(42319), 0, 1, 0);
                AddComponent(new AddonComponent(42320), 1, 0, 0);
                AddComponent(new TeleporterComponent(42318, Direction.Down), 1, 1, 0);
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

        public bool CheckAccessible(Mobile from, Item item)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
            {
                return true; // Staff can access anything
            }

            BaseHouse house = BaseHouse.FindHouseAt(item);

            if (house == null)
            {
                return false;
            }

            switch (Level)
            {
                case SecureLevel.Owner: return house.IsOwner(from);
                case SecureLevel.CoOwners: return house.IsCoOwner(from);
                case SecureLevel.Friends: return house.IsFriend(from);
                case SecureLevel.Anyone: return true;
                case SecureLevel.Guild: return house.IsGuildMember(from);
            }

            return false;
        }

        public SpiralStaircaseAddon(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            writer.Write((int)Level);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            Level = (SecureLevel)reader.ReadInt();
        }
    }
}
