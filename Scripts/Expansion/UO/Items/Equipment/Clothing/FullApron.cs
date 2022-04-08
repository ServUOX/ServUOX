using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefTailoring), typeof(GargoyleHalfApron))]
    [Flipable(0x153d, 0x153e)]
    public class FullApron : BaseMiddleTorso
    {
        [Constructible]
        public FullApron()
            : this(0)
        {
        }

        [Constructible]
        public FullApron(int hue)
            : base(0x153d, hue)
        {
            Weight = 4.0;
        }

        public FullApron(Serial serial)
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
