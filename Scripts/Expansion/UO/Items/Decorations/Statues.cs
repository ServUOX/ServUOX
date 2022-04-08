namespace Server.Items
{
    public class StatueSouth : Item
    {
        [Constructible]
        public StatueSouth()
            : base(0x139A)
        {
            Weight = 10;
        }

        public StatueSouth(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    public class StatueSouth2 : Item
    {
        [Constructible]
        public StatueSouth2()
            : base(0x1227)
        {
            Weight = 10;
        }

        public StatueSouth2(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    public class StatueNorth : Item
    {
        [Constructible]
        public StatueNorth()
            : base(0x139B)
        {
            Weight = 10;
        }

        public StatueNorth(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    public class StatueWest : Item
    {
        [Constructible]
        public StatueWest()
            : base(0x1226)
        {
            Weight = 10;
        }

        public StatueWest(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    public class StatueEast : Item
    {
        [Constructible]
        public StatueEast()
            : base(0x139C)
        {
            Weight = 10;
        }

        public StatueEast(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    public class StatueEast2 : Item
    {
        [Constructible]
        public StatueEast2()
            : base(0x1224)
        {
            Weight = 10;
        }

        public StatueEast2(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    public class StatueSouthEast : Item
    {
        [Constructible]
        public StatueSouthEast()
            : base(0x1225)
        {
            Weight = 10;
        }

        public StatueSouthEast(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    public class BustSouth : Item
    {
        [Constructible]
        public BustSouth()
            : base(0x12CB)
        {
            Weight = 10;
        }

        public BustSouth(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    public class BustEast : Item
    {
        [Constructible]
        public BustEast()
            : base(0x12CA)
        {
            Weight = 10;
        }

        public BustEast(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    [TypeAlias("Server.Items.StatuePegasus")]
    public class StatuePegasusSouth : Item
    {
        public override int LabelNumber => 1044510;  // pegasus statuette

        [Constructible]
        public StatuePegasusSouth()
            : base(0x139D)
        {
            Weight = 1.0;
        }

        public StatuePegasusSouth(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    [TypeAlias("Server.Items.StatuePegasus2")]
    public class StatuePegasusEast : Item
    {
        public override int LabelNumber => 1044510;  // pegasus statuette

        [Constructible]
        public StatuePegasusEast()
            : base(0x1228)
        {
            Weight = 1.0;
        }

        public StatuePegasusEast(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    public class SmallTowerSculpture : Item
    {
        [Constructible]
        public SmallTowerSculpture()
            : base(0x241A)
        {
            Weight = 1.0;
        }

        public SmallTowerSculpture(Serial serial)
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

    [Flipable(0x493C, 0x493B)]
    public class StatueGryphonEast : Item
    {
        [Constructible]
        public StatueGryphonEast()
            : base(0x493C)
        {
            Weight = 1.0;
        }

        public StatueGryphonEast(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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

    [Flipable(0x493B, 0x493C)]
    public class StatueGryphonSouth : Item
    {
        [Constructible]
        public StatueGryphonSouth()
            : base(0x493B)
        {
            Weight = 1.0;
        }

        public StatueGryphonSouth(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;

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
