using System;
using Server.Engines.VeteranRewards;
using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable(0x2306, 0x2305)]
    public class FlowerGarland : BaseHat
    {
        public override int BasePhysicalResistance => 3;
        public override int BaseFireResistance => 3;
        public override int BaseColdResistance => 6;
        public override int BasePoisonResistance => 9;
        public override int BaseEnergyResistance => 9;

        public override int InitMinHits => 20;
        public override int InitMaxHits => 30;

        [Constructible]
        public FlowerGarland()
            : this(0)
        {
        }

        [Constructible]
        public FlowerGarland(int hue)
            : base(0x2306, hue)
        {

        }

        public FlowerGarland(Serial serial)
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
