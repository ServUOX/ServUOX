using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable(0x230A, 0x2309)]
    public class FurCape : BaseCloak
    {
        [Constructible]
        public FurCape()
            : this(0)
        {
        }

        [Constructible]
        public FurCape(int hue)
            : base(0x230A, hue)
        {
            Weight = 4.0;
        }

        public FurCape(Serial serial)
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
