namespace Server.Mobiles
{
    [CorpseName("a mantra effervescence corpse")]
    public class MantraEffervescence : BaseCreature
    {
        [Constructible]
        public MantraEffervescence()
            : base(AIType.AI_Spellweaving, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a mantra effervescence";
            Body = 0x111;
            BaseSoundID = 0x56E;

            SetStr(130, 150);
            SetDex(120, 130);
            SetInt(150, 230);

            SetHits(150, 250);

            SetDamage(ResistType.Phys, 30, 0, 21, 25);
            SetDamage(ResistType.Engy, 70);

            SetResist(ResistType.Phys, 60, 65);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 100);

            SetSkill(SkillName.Wrestling, 80.0, 85.0);
            SetSkill(SkillName.Tactics, 80.0, 85.0);
            SetSkill(SkillName.MagicResist, 105.0, 115.0);
            SetSkill(SkillName.Magery, 90.0, 110.0);
            SetSkill(SkillName.EvalInt, 80.0, 90.0);
            SetSkill(SkillName.Meditation, 90.0, 100.0);

            SetSkill(SkillName.Spellweaving, 90.0, 96.0);

            Fame = 6500;
            Karma = -6500;

            SetAreaEffect(AreaEffect.AuraOfEnergy);
        }

        public MantraEffervescence(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
        }

        public override int GetHurtSound() => 0x56E;

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
