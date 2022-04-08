namespace Server.Items
{
    [Flipable(0x152e, 0x152f)]
    public class ShortPants : BasePants
    {
        [Constructible]
        public ShortPants()
            : this(0)
        {
        }

        [Constructible]
        public ShortPants(int hue)
            : base(0x152E, hue)
        {
            Weight = 2.0;
        }

        public ShortPants(Serial serial)
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
