namespace Server.Items
{
    [Furniture]
    [TypeAlias("Server.Items.TallMusicStand")]
    public class TallMusicStandLeft : Item
    {
        [Constructible]
        public TallMusicStandLeft()
            : base(0xEBB)
        {
            Weight = 10.0;
        }

        public TallMusicStandLeft(Serial serial)
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
    public class TallMusicStandRight : Item
    {
        [Constructible]
        public TallMusicStandRight()
            : base(0xEBC)
        {
            Weight = 10.0;
        }

        public TallMusicStandRight(Serial serial)
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
    [TypeAlias("Server.Items.ShortMusicStand")]
    public class ShortMusicStandLeft : Item
    {
        [Constructible]
        public ShortMusicStandLeft()
            : base(0xEB6)
        {
            Weight = 10.0;
        }

        public ShortMusicStandLeft(Serial serial)
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
    public class ShortMusicStandRight : Item
    {
        [Constructible]
        public ShortMusicStandRight()
            : base(0xEB8)
        {
            Weight = 10.0;
        }

        public ShortMusicStandRight(Serial serial)
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
