namespace Server.Items
{
    public class Eggs : CookableFood
    {
        [Constructible]
        public Eggs()
            : this(1)
        {
        }

        [Constructible]
        public Eggs(int amount)
            : base(0x9B5, 15)
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        public Eggs(Serial serial)
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

        public override Food Cook()
        {
            return new FriedEggs();
        }
    }
}
