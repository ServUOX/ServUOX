using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefTailoring), typeof(GargishSash))]
    [Flipable(0x1541, 0x1542)]
    public class BodySash : BaseMiddleTorso
    {
        [Constructible]
        public BodySash()
            : this(0)
        {
        }

        [Constructible]
        public BodySash(int hue)
            : base(0x1541, hue)
        {
            Weight = 1.0;
        }

        public BodySash(Serial serial)
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
