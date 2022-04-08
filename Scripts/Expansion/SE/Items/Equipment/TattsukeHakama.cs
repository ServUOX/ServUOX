using System;

namespace Server.Items
{

    [Flipable(0x279B, 0x27E6)]
    public class TattsukeHakama : BasePants
    {
        [Constructible]
        public TattsukeHakama()
            : this(0)
        {
        }

        [Constructible]
        public TattsukeHakama(int hue)
            : base(0x279B, hue)
        {
            Weight = 2.0;
        }

        public TattsukeHakama(Serial serial)
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
