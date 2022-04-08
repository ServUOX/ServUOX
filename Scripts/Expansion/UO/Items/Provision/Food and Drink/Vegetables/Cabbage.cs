namespace Server.Items
{
    [Flipable(0xC7B, 0xC7C)]
    public class Cabbage : Food
    {
        [Constructible]
        public Cabbage()
            : this(1)
        {
        }

        [Constructible]
        public Cabbage(int amount)
            : base(amount, 0xc7b)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Cabbage(Serial serial)
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
