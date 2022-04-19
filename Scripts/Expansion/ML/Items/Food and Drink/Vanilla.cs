using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public class Vanilla : Item
    {
        public override int LabelNumber => 1080009;  // Vanilla
        public override double DefaultWeight => 1.0;

        [Constructible]
        public Vanilla()
            : this(1)
        { }

        [Constructible]
        public Vanilla(int amount)
            : base(0xE2A)
        {
            Hue = 1122;
            Stackable = true;
            Amount = amount;
        }

        public Vanilla(Serial serial)
            : base(serial)
        { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            reader.ReadInt();
        }
    }
}
