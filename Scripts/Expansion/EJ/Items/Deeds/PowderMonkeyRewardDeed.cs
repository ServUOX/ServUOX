namespace Server.Items
{
    public class PowderMonkeyRewardDeed : BaseRewardTitleDeed
    {
        public override TextDefinition Title => 1158948;  // Powder Monkey

        [Constructible]
        public PowderMonkeyRewardDeed()
        {
        }

        public PowderMonkeyRewardDeed(Serial serial)
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
