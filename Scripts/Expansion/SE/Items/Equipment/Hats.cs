using System;
using System.Collections.Generic;
using Server.Engines.Craft;
using Server.Network;

namespace Server.Items
{
    [Flipable(0x2798, 0x27E3)]
    public class Kasa : BaseHat
    {
        public override int BasePhysicalResistance => 0;
        public override int BaseFireResistance => 5;
        public override int BaseColdResistance => 9;
        public override int BasePoisonResistance => 5;
        public override int BaseEnergyResistance => 5;

        public override int InitMinHits => 20;
        public override int InitMaxHits => 30;

        [Constructible]
        public Kasa()
            : this(0)
        {
        }

        [Constructible]
        public Kasa(int hue)
            : base(0x2798, hue)
        {
        }

        public Kasa(Serial serial)
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

    [Flipable(0x278F, 0x27DA)]
    public class ClothNinjaHood : BaseHat
    {
        public override int BasePhysicalResistance => 3;
        public override int BaseFireResistance => 3;
        public override int BaseColdResistance => 6;
        public override int BasePoisonResistance => 9;
        public override int BaseEnergyResistance => 9;

        public override int InitMinHits => 20;
        public override int InitMaxHits => 30;

        [Constructible]
        public ClothNinjaHood()
            : this(0)
        {
        }

        [Constructible]
        public ClothNinjaHood(int hue)
            : base(0x278F, hue)
        {
        }

        public ClothNinjaHood(Serial serial)
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
