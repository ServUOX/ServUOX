namespace Server.Items
{
    public class RawFishSteak : CookableFood, ICommodity
    {
        public override double DefaultWeight => 0.1;

        [Constructible]
        public RawFishSteak()
            : this(1)
        {
        }

        [Constructible]
        public RawFishSteak(int amount)
            : base(0x097A, 10)
        {
            Stackable = true;
            Amount = amount;
        }

        public RawFishSteak(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override Food Cook()
        {
            return new FishSteak();
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
