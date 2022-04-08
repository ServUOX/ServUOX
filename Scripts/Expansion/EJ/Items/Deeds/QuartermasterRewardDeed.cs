namespace Server.Items
{
    public class QuartermasterRewardDeed : BaseRewardTitleDeed
    {
        public override TextDefinition Title => 1158951;  // Quartermaster

        [Constructible]
        public QuartermasterRewardDeed()
        {
        }

        public QuartermasterRewardDeed(Serial serial)
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
