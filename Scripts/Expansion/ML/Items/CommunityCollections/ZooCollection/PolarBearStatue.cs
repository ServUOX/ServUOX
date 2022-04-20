namespace Server.Items
{
    public class PolarBearStatue : Item
    {
        public override bool IsArtifact => true;
        [Constructible]
        public PolarBearStatue()
            : base(0x20E1)
        {
            LootType = LootType.Blessed;
            Weight = 1.0;
        }

        public PolarBearStatue(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1073193;// A Polar Bear Contribution Statue from the Britannia Royal Zoo.
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