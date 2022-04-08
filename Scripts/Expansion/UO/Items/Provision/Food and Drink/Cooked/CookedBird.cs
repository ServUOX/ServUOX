namespace Server.Items
{
    public class CookedBird : Food
    {
        public override ItemQuality Quality { get => ItemQuality.Normal; set { } }

        [Constructible]
        public CookedBird()
            : this(1)
        {
        }

        [Constructible]
        public CookedBird(int amount)
            : base(amount, 0x9B7)
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        public CookedBird(Serial serial)
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
