namespace Server.Items
{
    public class StarSapphire : Item, IGem
    {
        [Constructible]
        public StarSapphire()
            : this(1)
        {
        }

        [Constructible]
        public StarSapphire(int amount)
            : base(0x0F0F)
        {
            Stackable = true;
            Amount = amount;
        }

        public StarSapphire(Serial serial)
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
