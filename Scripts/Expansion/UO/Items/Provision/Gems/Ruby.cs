namespace Server.Items
{
    public class Ruby : Item, IGem
    {
        [Constructible]
        public Ruby()
            : this(1)
        {
        }

        [Constructible]
        public Ruby(int amount)
            : base(0xF13)
        {
            Stackable = true;
            Amount = amount;
        }

        public Ruby(Serial serial)
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
