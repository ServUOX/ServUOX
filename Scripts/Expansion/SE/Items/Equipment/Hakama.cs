using System;
using Server.Engines.Craft;

namespace Server.Items
{

    [Flipable(0x279A, 0x27E5)]
    public class Hakama : BaseOuterLegs
    {
        [Constructible]
        public Hakama()
            : this(0)
        {
        }

        [Constructible]
        public Hakama(int hue)
            : base(0x279A, hue)
        {
            Weight = 2.0;
        }

        public Hakama(Serial serial)
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
