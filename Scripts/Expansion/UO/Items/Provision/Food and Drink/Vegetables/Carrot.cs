namespace Server.Items
{
    [Flipable(0xC77, 0xC78)]
    public class Carrot : Food
    {
        [Constructible]
        public Carrot()
            : this(1)
        {
        }

        [Constructible]
        public Carrot(int amount)
            : base(amount, 0xc78)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Carrot(Serial serial)
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
