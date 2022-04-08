namespace Server.Items
{
    public class Bacon : Food
    {
        [Constructible]
        public Bacon()
            : this(1)
        {
        }

        [Constructible]
        public Bacon(int amount)
            : base(amount, 0x979)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Bacon(Serial serial)
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
