namespace Server.Items
{
    public class RuinedTapestry : Item
    {
        public override int LabelNumber => 1096945; // ruined tapestry

        [Constructible]
        public RuinedTapestry()
            : base(Utility.RandomBool() ? 0x4699 : 0x469A)
        {
        }

        public RuinedTapestry(Serial serial)
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
