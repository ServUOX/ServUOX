namespace Server.Items
{
    public class SeaHorseFish : BaseFish
    {
        [Constructible]
        public SeaHorseFish()
            : base(0x3B10)
        {
        }

        public SeaHorseFish(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1074414;// A sea horse
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
