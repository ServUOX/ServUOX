namespace Server.Items
{
    [Flipable]
    public class MarbelBarSink2 : BaseBarSink
    {
        private static readonly int vItemID = 0x9A16;
        private static readonly int fItemID = 0x9A18;

        [Constructible]
        public MarbelBarSink2()
            : this(false)
        {
        }

        [Constructible]
        public MarbelBarSink2(bool filled)
            : base((filled) ? MarbelBarSink2.fItemID : MarbelBarSink2.vItemID, filled)
        {
        }

        public MarbelBarSink2(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber { get { return 1123469; } }/* Marble Bar */
        public override int voidItem_ID { get { if (ItemID == 0x9A18) { return vItemID; } else return 0x9A17; } }
        public override int fullItem_ID { get { if (ItemID == 0x9A16) { return fItemID; } else return 0x9A19; } }
        public override int MaxQuantity { get { return 100; } }

        public void Flip()
        {
            switch (ItemID)
            {
                case 0x9A16: ItemID = 0x9A17; break;
                case 0x9A17: ItemID = 0x9A16; break;
                case 0x9A18: ItemID = 0x9A19; break;
                case 0x9A19: ItemID = 0x9A18; break;
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