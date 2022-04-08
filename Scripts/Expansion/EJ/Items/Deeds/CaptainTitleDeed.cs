namespace Server.Items
{
    public class CaptainTitleDeed : BaseRewardTitleDeed
    {
        public override TextDefinition Title => new TextDefinition(1159216);  // Captain

        [Constructible]
        public CaptainTitleDeed()
        {
        }

        public CaptainTitleDeed(Serial serial)
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
