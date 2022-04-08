namespace Server.Items
{
    [Flipable(0xA0D8, 0xA0D9)]
    public class HotDog : Food
    {
        public override int LabelNumber => 1125201;  // hot dog

        [Constructible]
        public HotDog()
            : this(1)
        {
        }

        [Constructible]
        public HotDog(int amount)
            : base(amount, 0xA0D8)
        {
            FillFactor = 2;
        }

        public HotDog(Serial serial)
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
