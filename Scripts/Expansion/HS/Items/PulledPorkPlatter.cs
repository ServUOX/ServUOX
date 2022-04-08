namespace Server.Items
{
    public class PulledPorkPlatter : Food
    {
        public override int LabelNumber => 1123351;  // Pulled Pork Platter

        [Constructible]
        public PulledPorkPlatter()
            : base(1, 0x999F)
        {
            FillFactor = 5;
            Stackable = false;
            Hue = 1157;
        }

        public PulledPorkPlatter(Serial serial)
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
