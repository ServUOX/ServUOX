using System;
using Server.Engines.Craft;

namespace Server.Items
{

    [Flipable(0x27A1, 0x27EC)]
    public class JinBaori : BaseMiddleTorso
    {
        [Constructible]
        public JinBaori()
            : this(0)
        {
        }

        [Constructible]
        public JinBaori(int hue)
            : base(0x27A1, hue)
        {
            Weight = 3.0;
        }

        public JinBaori(Serial serial)
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
