namespace Server.Items
{
    public class OssianGrimoire : NecromancerSpellbook
    {
        public override int LabelNumber => 1078148; // Ossian Grimoire
        public override bool IsArtifact => true;

        [Constructible]
        public OssianGrimoire()
        {
            LootType = LootType.Blessed;
            SkillBonuses.SetValues(0, SkillName.Necromancy, 10.0);
            Attributes.RegenMana = 1;
            Attributes.CastSpeed = 1;
            Attributes.IncreasedKarmaLoss = 5;
        }

        public OssianGrimoire(Serial serial)
            : base(serial)
        {
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
