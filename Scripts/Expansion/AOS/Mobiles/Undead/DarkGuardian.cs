namespace Server.Mobiles
{
    [CorpseName("a dark guardians' corpse")]
    public class DarkGuardian : BaseCreature
    {
        [Constructible]
        public DarkGuardian()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a dark guardian";
            Body = 78;
            BaseSoundID = 0x3E9;

            SetStr(125, 150);
            SetDex(100, 120);
            SetInt(200, 235);

            SetHits(150, 180);

            SetDamage(ResistType.Phys, 10, 0, 24, 26);
            SetDamage(ResistType.Cold, 40);
            SetDamage(ResistType.Engy, 50);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 20, 45);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 20, 45);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.EvalInt, 40.1, 50.0);
            SetSkill(SkillName.Magery, 50.1, 60.0);
            SetSkill(SkillName.Meditation, 85.0, 95.0);
            SetSkill(SkillName.MagicResist, 50.1, 70.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Necromancy, 100.0, 110.0);
            SetSkill(SkillName.SpiritSpeak, 95.0, 105.0);
            SetSkill(SkillName.DetectHidden, 55.0, 60.0);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 50;
            PackItem(Loot.PackNecroReg(15, 25));
        }

        public DarkGuardian(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override int TreasureMapLevel => Utility.RandomMinMax(1, 3);
        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override bool Unprovokable => true;

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
