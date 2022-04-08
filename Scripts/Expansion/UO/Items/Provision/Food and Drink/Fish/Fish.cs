namespace Server.Items
{
    public class Fish : Item, ICarvable
    {
        [Constructible]
        public Fish()
            : this(1)
        {
        }

        [Constructible]
        public Fish(int amount)
            : base(Utility.Random(0x09CC, 4))
        {
            Stackable = true;
            Weight = 1.0;
            Amount = amount;
        }

        public Fish(Serial serial)
            : base(serial)
        {
        }

        public bool Carve(Mobile from, Item item)
        {
            var fish = new RawFishSteak();

            if (HasSocket<Caddellite>())
            {
                fish.AttachSocket(new Caddellite());
            }

            base.ScissorHelper(from, fish, 4);

            return true;
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
