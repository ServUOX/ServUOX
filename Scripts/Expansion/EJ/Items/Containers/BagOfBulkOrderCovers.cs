using Server.Engines.BulkOrders;
using Server.Targeting;
using System.Collections.Generic;
using System.Linq;

namespace Server.Items
{
    public class BagOfBulkOrderCovers : Bag
    {
        public override int LabelNumber => 1071116;  // Bag of bulk order covers

        public BagOfBulkOrderCovers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                if (i >= 0 && i < CoverInfo.Infos.Count)
                {
                    DropItem(new BulkOrderBookCover(CoverInfo.Infos[i].Type));
                }
            }
        }

        public BagOfBulkOrderCovers(Serial serial)
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
