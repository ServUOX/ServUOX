namespace Server.Items
{
    public class Jug : BaseBeverage
    {
        public override int BaseLabelNumber => 1042965;// a jug of Ale
        public override int MaxQuantity => 10;
        public override bool Fillable => false;

        public override int ComputeItemID()
        {
            if (!IsEmpty)
            {
                return 0x9C8;
            }

            return 0;
        }

        [Constructible]
        public Jug(BeverageType type)
            : base(type)
        {
            Weight = 1.0;
        }

        public Jug(Serial serial)
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
