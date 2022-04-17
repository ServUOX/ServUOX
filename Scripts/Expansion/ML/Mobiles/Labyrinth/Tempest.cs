namespace Server.Mobiles
{
    [CorpseName("the remains of Tempest")]
    public class Tempest : AirElemental
    {
        [Constructible]
        public Tempest()
            : base()
        {
            Name = "Tempest";
            Body = 13;
            Hue = 1175;
            BaseSoundID = 263;

            SetStr(116, 135);
            SetDex(166, 185);
            SetInt(101, 125);

            SetHits(602);

            SetDamage(ResistType.Engy, 80, 0, 18, 20);
            SetDamage(ResistType.Cold, 20);

            SetResist(ResistType.Phys, 46);
            SetResist(ResistType.Fire, 39);
            SetResist(ResistType.Cold, 33);
            SetResist(ResistType.Pois, 36);
            SetResist(ResistType.Engy, 58);

            SetSkill(SkillName.EvalInt, 99.6);
            SetSkill(SkillName.Magery, 101.0);
            SetSkill(SkillName.MagicResist, 104.6);
            SetSkill(SkillName.Tactics, 111.8);
            SetSkill(SkillName.Wrestling, 116.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 40;
            ControlSlots = 2;
        }

        public override bool GivesMLMinorArtifact => true;

        public Tempest(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;
        public override double DispelFocus => 45.0;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 2;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.MedScrolls);
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
