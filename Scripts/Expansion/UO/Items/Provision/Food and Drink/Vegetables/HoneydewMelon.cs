namespace Server.Items
{
    [Flipable(0xC74, 0xC75)]
    public class HoneydewMelon : Food
    {
        [Constructible]
        public HoneydewMelon()
            : this(1)
        {
        }

        [Constructible]
        public HoneydewMelon(int amount)
            : base(amount, 0xC74)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public HoneydewMelon(Serial serial)
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
