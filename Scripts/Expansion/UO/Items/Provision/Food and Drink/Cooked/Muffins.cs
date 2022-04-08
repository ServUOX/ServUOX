namespace Server.Items
{
    public class Muffins : Food
    {
        [Constructible]
        public Muffins()
            : base(0x9eb)
        {
            Stackable = true;
            Weight = 1.0;
            FillFactor = 4;
        }

        public Muffins(Serial serial)
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
