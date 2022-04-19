namespace Server.Items
{
    public class SackOfSugar : Item
    {
        public override int LabelNumber => 1080003;  // Sack of sugar
        public override double DefaultWeight => 1.0;

        [Constructible]
        public SackOfSugar()
            : this(1)
        { }

        [Constructible]
        public SackOfSugar(int amount)
            : base(0x1039)
        {
            Hue = 1121;
            Stackable = true;
            Amount = amount;
        }

        public SackOfSugar(Serial serial)
            : base(serial)
        { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }
}
