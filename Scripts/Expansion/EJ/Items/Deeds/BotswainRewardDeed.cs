namespace Server.Items
{
    public class BotswainRewardDeed : BaseRewardTitleDeed
    {
        public override TextDefinition Title => 1158949;  // Botswain

        [Constructible]
        public BotswainRewardDeed()
        {
        }

        public BotswainRewardDeed(Serial serial)
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
