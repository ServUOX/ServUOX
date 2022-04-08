namespace Server.Items
{
    public class SailingMasterRewardDeed : BaseRewardTitleDeed
    {
        public override TextDefinition Title => 1158950;  // Sailing Master

        [Constructible]
        public SailingMasterRewardDeed()
        {
        }

        public SailingMasterRewardDeed(Serial serial)
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
