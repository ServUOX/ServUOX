namespace Server.Items
{
    public class Ribs : Food
    {
        public override ItemQuality Quality { get => ItemQuality.Normal; set { } }

        [Constructible]
        public Ribs()
            : this(1)
        {
        }

        [Constructible]
        public Ribs(int amount)
            : base(amount, 0x9F2)
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        public Ribs(Serial serial)
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
