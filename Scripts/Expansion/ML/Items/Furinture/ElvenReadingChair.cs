namespace Server.Items
{
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
