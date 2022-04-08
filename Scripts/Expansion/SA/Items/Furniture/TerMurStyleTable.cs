namespace Server.Items
{
    [Furniture]
    public class TerMurStyleTable : Item
    {
        public override int LabelNumber => 1095321;  // Ter-Mur style table

        [Constructible]
        public TerMurStyleTable()
            : base(0x4041)
        {
            Weight = 10.0;
        }

        public TerMurStyleTable(Serial serial)
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
