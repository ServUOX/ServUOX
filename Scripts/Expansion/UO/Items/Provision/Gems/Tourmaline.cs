namespace Server.Items
{
    public class Tourmaline : Item, IGem
    {
        [Constructible]
        public Tourmaline()
            : this(1)
        {
        }

        [Constructible]
        public Tourmaline(int amount)
            : base(0x0F18)
        {
            Stackable = true;
            Amount = amount;
        }

        public Tourmaline(Serial serial)
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
