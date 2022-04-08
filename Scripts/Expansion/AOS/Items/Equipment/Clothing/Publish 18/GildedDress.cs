using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable(0x230E, 0x230D)]
    public class GildedDress : BaseOuterTorso
    {
        [Constructible]
        public GildedDress()
            : this(0)
        {
        }

        [Constructible]
        public GildedDress(int hue)
            : base(0x230E, hue)
        {
            Weight = 3.0;
        }

        public GildedDress(Serial serial)
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
