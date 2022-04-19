namespace Server.Items
{
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
}
