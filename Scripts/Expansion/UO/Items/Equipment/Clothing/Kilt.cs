namespace Server.Items
{
    [Flipable(0x1537, 0x1538)]
    public class Kilt : BaseOuterLegs
    {
        [Constructible]
        public Kilt()
            : this(0)
        {
        }

        [Constructible]
        public Kilt(int hue)
            : base(0x1537, hue)
        {
            Weight = 2.0;
        }

        public Kilt(Serial serial)
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
