namespace Server.Items
{
    [Flipable(0xC66, 0xC67)]
    public class GreenGourd : Food
    {
        [Constructible]
        public GreenGourd()
            : this(1)
        {
        }

        [Constructible]
        public GreenGourd(int amount)
            : base(amount, 0xC66)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public GreenGourd(Serial serial)
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
