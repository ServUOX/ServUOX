namespace Server.Items
{
    public class GoldBeadNecklace : BaseNecklace
    {
        [Constructible]
        public GoldBeadNecklace()
            : base(0x1089)
        {
        }

        public GoldBeadNecklace(Serial serial)
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
