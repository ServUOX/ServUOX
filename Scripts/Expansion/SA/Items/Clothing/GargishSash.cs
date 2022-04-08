using System;
using Server.Engines.Craft;

namespace Server.Items
{

    [Flipable(0x46B4, 0x46B5)]
    public class GargishSash : BaseClothing
    {
        [Constructible]
        public GargishSash()
            : this(0)
        {
        }

        [Constructible]
        public GargishSash(int hue)
            : base(0x46B4, Layer.MiddleTorso, hue)
        {
            Weight = 1.0;
        }

        public GargishSash(Serial serial)
            : base(serial)
        {
        }

        public override Race RequiredRace => Race.Gargoyle;
        public override bool CanBeWornByGargoyles => true;
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
