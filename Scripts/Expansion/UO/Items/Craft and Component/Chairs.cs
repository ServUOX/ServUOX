namespace Server.Items
{
    [Furniture]
    [Flipable(0xB4F, 0xB4E, 0xB50, 0xB51)]
    public class FancyWoodenChairCushion : CraftableFurniture
    {
        [Constructible]
        public FancyWoodenChairCushion()
            : base(0xB4F)
        {
            Weight = 20.0;
        }

        public FancyWoodenChairCushion(Serial serial)
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

    [Furniture]
    [Flipable(0xB53, 0xB52, 0xB54, 0xB55)]
    public class WoodenChairCushion : CraftableFurniture
    {
        [Constructible]
        public WoodenChairCushion()
            : base(0xB53)
        {
            Weight = 20.0;
        }

        public WoodenChairCushion(Serial serial)
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

    [Furniture]
    [Flipable(0xB57, 0xB56, 0xB59, 0xB58)]
    public class WoodenChair : CraftableFurniture
    {
        [Constructible]
        public WoodenChair()
            : base(0xB57)
        {
            Weight = 20.0;
        }

        public WoodenChair(Serial serial)
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

    [Furniture]
    [Flipable(0xB5B, 0xB5A, 0xB5C, 0xB5D)]
    public class BambooChair : CraftableFurniture
    {
        [Constructible]
        public BambooChair()
            : base(0xB5B)
        {
            Weight = 20.0;
        }

        public BambooChair(Serial serial)
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

    [Furniture]
    [DynamicFliping]
    [Flipable(0x1218, 0x1219, 0x121A, 0x121B)]
    public class StoneChair : Item
    {
        [Constructible]
        public StoneChair()
            : base(0x1218)
        {
            Weight = 1.0;
        }

        public StoneChair(Serial serial)
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
