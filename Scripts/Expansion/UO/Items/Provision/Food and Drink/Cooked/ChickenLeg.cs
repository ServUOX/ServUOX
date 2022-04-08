namespace Server.Items
{
    public class ChickenLeg : Food
    {
        public override ItemQuality Quality { get => ItemQuality.Normal; set { } }

        [Constructible]
        public ChickenLeg()
            : this(1)
        {
        }

        [Constructible]
        public ChickenLeg(int amount)
            : base(amount, 0x1608)
        {
            Weight = 1.0;
            FillFactor = 4;
        }

        public ChickenLeg(Serial serial)
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
