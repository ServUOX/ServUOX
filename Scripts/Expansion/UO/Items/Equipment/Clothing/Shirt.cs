namespace Server.Items
{
    [Flipable(0x1517, 0x1518)]
    public class Shirt : BaseShirt
    {
        [Constructible]
        public Shirt()
            : this(0)
        {
        }

        [Constructible]
        public Shirt(int hue)
            : base(0x1517, hue)
        {
            Weight = 1.0;
        }

        public Shirt(Serial serial)
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
