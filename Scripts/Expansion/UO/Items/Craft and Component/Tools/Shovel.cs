using Server.Engines.Harvest;

namespace Server.Items
{
    public class Shovel : BaseHarvestTool
    {
        public override HarvestSystem HarvestSystem => Mining.System;

        [Constructible]
        public Shovel()
            : this(50)
        {
        }

        [Constructible]
        public Shovel(int uses)
            : base(uses, 0xF39)
        {
            Weight = 5.0;
        }

        public Shovel(Serial serial)
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
