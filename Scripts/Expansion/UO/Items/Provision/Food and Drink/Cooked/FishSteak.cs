namespace Server.Items
{
    public class FishSteak : Food
    {
        public override ItemQuality Quality { get => ItemQuality.Normal; set { } }

        public override double DefaultWeight => 0.1;

        [Constructible]
        public FishSteak()
            : this(1)
        {
        }

        [Constructible]
        public FishSteak(int amount)
            : base(amount, 0x97B)
        {
            FillFactor = 3;
        }

        public FishSteak(Serial serial)
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
