namespace Server.Items
{
    public class Quiche : Food
    {
        public override int LabelNumber => 1041345;// baked quiche

        [Constructible]
        public Quiche()
            : base(0x1041)
        {
            Stackable = Core.ML;
            Weight = 1.0;
            FillFactor = 5;
        }

        public Quiche(Serial serial)
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
