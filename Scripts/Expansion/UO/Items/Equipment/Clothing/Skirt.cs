namespace Server.Items
{
    [Flipable(0x1516, 0x1531)]
    public class Skirt : BaseOuterLegs
    {
        [Constructible]
        public Skirt()
            : this(0)
        {
        }

        [Constructible]
        public Skirt(int hue)
            : base(0x1516, hue)
        {
            Weight = 4.0;
        }

        public Skirt(Serial serial)
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
