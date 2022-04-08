namespace Server.Items
{
    [Flipable(0xC64, 0xC65)]
    public class YellowGourd : Food
    {
        [Constructible]
        public YellowGourd()
            : this(1)
        {
        }

        [Constructible]
        public YellowGourd(int amount)
            : base(amount, 0xC64)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public YellowGourd(Serial serial)
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
