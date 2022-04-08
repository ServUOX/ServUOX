using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefTailoring), typeof(GargishApron), true)]
    [Flipable(0x153b, 0x153c)]
    public class HalfApron : BaseWaist
    {
        [Constructible]
        public HalfApron()
            : this(0)
        {
        }

        [Constructible]
        public HalfApron(int hue)
            : base(0x153b, hue)
        {
            Weight = 2.0;
        }

        public HalfApron(Serial serial)
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
