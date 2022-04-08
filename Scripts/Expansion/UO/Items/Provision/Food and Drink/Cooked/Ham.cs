namespace Server.Items
{
    public class Ham : Food
    {
        [Constructible]
        public Ham()
            : this(1)
        {
        }

        [Constructible]
        public Ham(int amount)
            : base(amount, 0x9C9)
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        public Ham(Serial serial)
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
