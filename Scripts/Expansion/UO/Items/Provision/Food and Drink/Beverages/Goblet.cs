namespace Server.Items
{
    public class Goblet : BaseBeverage
    {
        public override int BaseLabelNumber => 1043000;// a goblet of Ale
        public override int MaxQuantity => 1;

        public override int ComputeItemID()
        {
            if (ItemID == 0x99A || ItemID == 0x9B3 || ItemID == 0x9BF || ItemID == 0x9CB)
            {
                return ItemID;
            }

            return 0x99A;
        }

        [Constructible]
        public Goblet()
        {
            Weight = 1.0;
        }

        [Constructible]
        public Goblet(BeverageType type)
            : base(type)
        {
            Weight = 1.0;
        }

        public Goblet(Serial serial)
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
