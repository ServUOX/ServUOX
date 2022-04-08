namespace Server.Items
{
    public class Sapphire : Item, IGem
    {
        [Constructible]
        public Sapphire()
            : this(1)
        {
        }

        [Constructible]
        public Sapphire(int amount)
            : base(0xF11)
        {
            Stackable = true;
            Amount = amount;
        }

        public Sapphire(Serial serial)
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
