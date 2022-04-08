namespace Server.Items
{
    [Flipable(0x2310, 0x230F)]
    public class FormalShirt : BaseMiddleTorso
    {
        [Constructible]
        public FormalShirt()
            : this(0)
        {
        }

        [Constructible]
        public FormalShirt(int hue)
            : base(0x2310, hue)
        {
            Weight = 1.0;
        }

        public FormalShirt(Serial serial)
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
