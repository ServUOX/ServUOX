namespace Server.Items
{
    [Flipable(0xA0D6, 0xA0D7)]
    public class CookableSausage : Food
    {
        public override int LabelNumber => 1125198;  // sausage

        [Constructible]
        public CookableSausage()
            : base(0xA0D6)
        {
            FillFactor = 2;
        }

        public CookableSausage(Serial serial)
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
    }
}
