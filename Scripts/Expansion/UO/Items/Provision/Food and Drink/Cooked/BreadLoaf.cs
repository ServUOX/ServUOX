namespace Server.Items
{
    public class BreadLoaf : Food
    {
        public override ItemQuality Quality { get => ItemQuality.Normal; set { } }

        [Constructible]
        public BreadLoaf()
            : this(1)
        {
        }

        [Constructible]
        public BreadLoaf(int amount)
            : base(amount, 0x103B)
        {
            Weight = 1.0;
            FillFactor = 3;
        }

        public BreadLoaf(Serial serial)
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
