using Server.Engines.Harvest;

namespace Server.Items
{
    [Flipable(0x9E7E, 0x9E7F)]
    public class ImprovedRockHammer : BaseHarvestTool
    {
        public override int LabelNumber => 1157177;  // Improved Rock Hammer

        [Constructible]
        public ImprovedRockHammer()
            : this(500)
        {
        }

        [Constructible]
        public ImprovedRockHammer(int uses)
            : base(uses, 0x9E7E)
        {
            Hue = 2721;
            LootType = LootType.Blessed;
        }

        public ImprovedRockHammer(Serial serial)
            : base(serial)
        {
        }

        public override HarvestSystem HarvestSystem => Mining.System;

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
