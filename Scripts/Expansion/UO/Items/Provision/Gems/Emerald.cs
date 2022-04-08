namespace Server.Items
{
    public class Emerald : Item, IGem
    {
        [Constructible]
        public Emerald()
            : this(1)
        {
        }

        [Constructible]
        public Emerald(int amount)
            : base(0xF10)
        {
            Stackable = true;
            Amount = amount;
        }

        public Emerald(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight => 0.1;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
