namespace Server.Items
{
    public class WhiteChocolate : BaseSweet
    {
        public override int LabelNumber => 1079996;  // White chocolate
        public override double DefaultWeight => 1.0;

        [Constructible]
        public WhiteChocolate()
            : base(0xF11)
        {
            Hue = 1150;
            LootType = LootType.Regular;
        }

        public WhiteChocolate(Serial serial)
            : base(serial)
        { }

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
