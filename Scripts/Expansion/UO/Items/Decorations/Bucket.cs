namespace Server.Items
{
    class Bucket : BaseWaterContainer
    {
        private static readonly int vItemID = 0x14e0;
        private static readonly int fItemID = 0x2004;
        [Constructible]
        public Bucket()
            : this(false)
        {
        }

        [Constructible]
        public Bucket(bool filled)
            : base(filled ? fItemID : vItemID, filled)
        {
        }

        public Bucket(Serial serial)
            : base(serial)
        {
        }

        public override int voidItem_ID => vItemID;
        public override int fullItem_ID => fItemID;
        public override int MaxQuantity => 25;
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
