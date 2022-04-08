namespace Server.Items
{
    [Furniture]
    [DynamicFliping]
    [Flipable(0x2DE3, 0x2DE4, 0x2DE5, 0x2DE6)]
    public class OrnateElvenChair : CraftableFurniture
    {
        [Constructible]
        public OrnateElvenChair()
            : base(0x2DE3)
        {
            Weight = 1.0;
        }

        public OrnateElvenChair(Serial serial)
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
    [Flipable(0x2DEB, 0x2DEC, 0x2DED, 0x2DEE)]
    public class BigElvenChair : CraftableFurniture
    {
        [Constructible]
        public BigElvenChair()
            : base(0x2DEB)
        {
        }

        public BigElvenChair(Serial serial)
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
    [Flipable(0x2DF5, 0x2DF6)]
    public class ElvenReadingChair : CraftableFurniture
    {
        [Constructible]
        public ElvenReadingChair()
            : base(0x2DF5)
        {
        }

        public ElvenReadingChair(Serial serial)
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
