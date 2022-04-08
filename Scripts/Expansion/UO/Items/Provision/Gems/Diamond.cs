namespace Server.Items
{
    public class Diamond : Item, IGem
    {
        [Constructible]
        public Diamond()
            : this(1)
        {
        }

        [Constructible]
        public Diamond(int amount)
            : base(0xF26)
        {
            Stackable = true;
            Amount = amount;
        }

        public Diamond(Serial serial)
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
