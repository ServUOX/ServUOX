namespace Server.Items
{
    public abstract class BaseDecorationContainerArtifact : BaseContainer
    {
        public BaseDecorationContainerArtifact(int itemID)
            : base(itemID)
        {
            Weight = 10.0;
        }

        public BaseDecorationContainerArtifact(Serial serial)
            : base(serial)
        {
        }

        public abstract int ArtifactRarity { get; }
        public override bool ForceShowProperties => true;
        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);

            list.Add(1061078, ArtifactRarity.ToString()); // artifact rarity ~1_val~
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }
    }
}
