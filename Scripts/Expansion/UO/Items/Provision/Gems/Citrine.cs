namespace Server.Items
{
    public class Citrine : Item, IGem
    {
        [Constructible]
        public Citrine()
            : this(1)
        {
        }

        [Constructible]
        public Citrine(int amount)
            : base(0xF15)
        {
            Stackable = true;
            Amount = amount;
        }

        public Citrine(Serial serial)
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
