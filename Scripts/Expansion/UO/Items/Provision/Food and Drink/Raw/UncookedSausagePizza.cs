namespace Server.Items
{
    public class UncookedSausagePizza : CookableFood
    {
        public override int LabelNumber => 1041337;// uncooked sausage pizza

        [Constructible]
        public UncookedSausagePizza()
            : base(0x1083, 20)
        {
            Weight = 1.0;
        }

        public UncookedSausagePizza(Serial serial)
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
            return new SausagePizza();
        }
    }
}
