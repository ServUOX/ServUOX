namespace Server.Items
{
    [Flipable(0x1539, 0x153a)]
    public class LongPants : BasePants
    {
        [Constructible]
        public LongPants()
            : this(0)
        {
        }

        [Constructible]
        public LongPants(int hue)
            : base(0x1539, hue)
        {
            Weight = 2.0;
        }

        public LongPants(Serial serial)
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
