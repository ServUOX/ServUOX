namespace Server.Items
{
    public class SilverBracelet : BaseBracelet
    {

        [Constructible]
        public SilverBracelet()
            : base(0x1F06)
        {
        }

        public SilverBracelet(Serial serial)
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
