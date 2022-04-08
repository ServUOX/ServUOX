namespace Server.Items
{
    [Furniture]
    public class ElegantLowTable : CraftableFurniture
    {
        [Constructible]
        public ElegantLowTable()
            : base(0x2819)
        {
            Weight = 1.0;
        }

        public ElegantLowTable(Serial serial)
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
    public class PlainLowTable : CraftableFurniture
    {
        [Constructible]
        public PlainLowTable()
            : base(0x281A)
        {
            Weight = 1.0;
        }

        public PlainLowTable(Serial serial)
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
    [Flipable(0xB90, 0xB7D)]
    public class LargeTable : CraftableFurniture
    {
        [Constructible]
        public LargeTable()
            : base(0xB90)
        {
            Weight = 1.0;
        }

        public LargeTable(Serial serial)
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
    [Flipable(0xB35, 0xB34)]
    public class Nightstand : CraftableFurniture
    {
        [Constructible]
        public Nightstand()
            : base(0xB35)
        {
            Weight = 1.0;
        }

        public Nightstand(Serial serial)
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
    [Flipable(0xB8F, 0xB7C)]
    public class YewWoodTable : CraftableFurniture
    {
        [Constructible]
        public YewWoodTable()
            : base(0xB8F)
        {
            Weight = 1.0;
        }

        public YewWoodTable(Serial serial)
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
