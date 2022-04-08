using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable(0x27A0, 0x27EB)]
    public class Obi : BaseWaist
    {
        [Constructible]
        public Obi()
            : this(0)
        {
        }

        [Constructible]
        public Obi(int hue)
            : base(0x27A0, hue)
        {
            Weight = 1.0;
        }

        public Obi(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
