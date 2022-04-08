namespace Server.Items
{
    public class LanternOfSouls : Lantern
    {
        [Constructible]
        public LanternOfSouls()
        {
            Hue = 0x482;
        }

        public LanternOfSouls(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1061618;// Lantern of Souls
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
