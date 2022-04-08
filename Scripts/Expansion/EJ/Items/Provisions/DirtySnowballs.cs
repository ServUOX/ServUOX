namespace Server.Items
{
    public class DirtySnowballs : SnowPile
    {
        public override int LabelNumber => 1158833;  // dirty snowballs

        [Constructible]
        public DirtySnowballs()
        {
            ItemID = 0xE74;
            Hue = 1301;
        }

        public DirtySnowballs(Serial serial)
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
