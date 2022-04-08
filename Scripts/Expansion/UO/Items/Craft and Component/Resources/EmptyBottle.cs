namespace Server.Items
{
    public class EmptyBottle : Item, ICommodity
    {
        [Constructible]
        public EmptyBottle()
            : this(1)
        {
        }

        [Constructible]
        public EmptyBottle(int amount)
            : base(0xF0E)
        {
            Stackable = true;
            Weight = 1.0;
            Amount = amount;
        }

        public EmptyBottle(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => (Core.ML);
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
