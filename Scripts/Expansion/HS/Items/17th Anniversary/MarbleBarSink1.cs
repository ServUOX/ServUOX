namespace Server.Items
{
    [Flipable]
    public class MarbelBarSink1 : BaseBarSink
    {
        private static readonly int vItemID = 0x99CA;
        private static readonly int fItemID = 0x9A14;

        [Constructible]
        public MarbelBarSink1()
            : this(false)
        {
        }

        [Constructible]
        public MarbelBarSink1(bool filled)
            : base((filled) ? MarbelBarSink1.fItemID : MarbelBarSink1.vItemID, filled)
        {
        }

        public MarbelBarSink1(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber { get { return 1123469; } }/* Marble Bar */
        public override int voidItem_ID { get { if (ItemID == 0x9A14) { return vItemID; } else return 0x99CB; } }
        public override int fullItem_ID { get { if (ItemID == 0x99CA) { return fItemID; } else return 0x9A15; } }
        public override int MaxQuantity { get { return 100; } }

        public void Flip()
        {
            switch (ItemID)
            {
                case 0x99CA: ItemID = 0x99CB; break;
                case 0x99CB: ItemID = 0x99CA; break;
                case 0x9A14: ItemID = 0x9A15; break;
                case 0x9A15: ItemID = 0x9A14; break;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}