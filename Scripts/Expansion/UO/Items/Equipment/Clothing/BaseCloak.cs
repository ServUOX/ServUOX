using Server.Engines.Craft;
using System;

namespace Server.Items
{
    [Alterable(typeof(DefTailoring), typeof(GargishLeatherWingArmor), true)]
    public abstract class BaseCloak : BaseClothing
    {
        public BaseCloak(int itemID)
            : this(itemID, 0)
        {
        }

        public BaseCloak(int itemID, int hue)
            : base(itemID, Layer.Cloak, hue)
        {
        }

        public BaseCloak(Serial serial)
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
