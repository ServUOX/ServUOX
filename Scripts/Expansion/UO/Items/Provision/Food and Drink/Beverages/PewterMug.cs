namespace Server.Items
{
    public class PewterMug : BaseBeverage
    {
        public override int BaseLabelNumber => 1042994;// a pewter mug with Ale
        public override int MaxQuantity => 1;

        public override int ComputeItemID()
        {
            if (ItemID >= 0xFFF && ItemID <= 0x1002)
            {
                return ItemID;
            }

            return 0xFFF;
        }

        [Constructible]
        public PewterMug()
        {
            Weight = 1.0;
        }

        [Constructible]
        public PewterMug(BeverageType type)
            : base(type)
        {
            Weight = 1.0;
        }

        public PewterMug(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
