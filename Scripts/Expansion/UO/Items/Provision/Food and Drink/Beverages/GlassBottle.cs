namespace Server.Items
{
    public class GlassBottle : Item
    {
        [Constructible]
        public GlassBottle()
            : base(0xe2b)
        {
            Weight = 0.3;
        }

        public GlassBottle(Serial serial)
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
