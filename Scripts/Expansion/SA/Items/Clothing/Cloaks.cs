using System;
using Server.Engines.VeteranRewards;
using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable(0x45A4, 0x45A5)]
    public class GargishClothWingArmor : BaseClothing
    {
        [Constructible]
        public GargishClothWingArmor()
            : this(0)
        {
        }

        [Constructible]
        public GargishClothWingArmor(int hue)
            : base(0x45A4, Layer.Cloak, hue)
        {
            Weight = 2.0;
        }

        public override int AosStrReq => 10;
        public override Race RequiredRace => Race.Gargoyle;
        public override bool CanBeWornByGargoyles => true;

        public GargishClothWingArmor(Serial serial)
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

    [Flipable(0x4002, 0x4003)]
    public class GargishFancyRobe : BaseClothing
    {
        public override Race RequiredRace => Race.Gargoyle;
        public override bool CanBeWornByGargoyles => true;

        [Constructible]
        public GargishFancyRobe()
            : this(0)
        {
        }

        [Constructible]
        public GargishFancyRobe(int hue)
            : base(0x4002, Layer.OuterTorso, hue)
        {
            Weight = 3.0;
        }

        public GargishFancyRobe(Serial serial)
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

    [Flipable(0x4000, 0x4001)]
    public class GargishRobe : BaseClothing
    {
        public override Race RequiredRace => Race.Gargoyle;
        public override bool CanBeWornByGargoyles => true;

        [Constructible]
        public GargishRobe()
            : this(0)
        {
        }

        [Constructible]
        public GargishRobe(int hue)
            : base(0x4000, Layer.OuterTorso, hue)
        {
            Weight = 3.0;
        }

        public GargishRobe(Serial serial)
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
