namespace Server.Mobiles
{
    [CorpseName("a bone demon corpse")]
    public class BoneDemon : BaseCreature
    {
        [Constructible]
        public BoneDemon()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a bone demon";
            Body = 308;
            BaseSoundID = 0x48D;

            SetStr(1000);
            SetDex(151, 175);
            SetInt(171, 220);

            SetHits(3600);

            SetDamage(ResistType.Phys, 50, 0, 34, 36);
            SetDamage(ResistType.Cold, 50);

            SetResist(ResistType.Phys, 75);
            SetResist(ResistType.Fire, 60);
            SetResist(ResistType.Cold, 90);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 60);

            SetSkill(SkillName.Wrestling, 100.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 50.1, 75.0);
            SetSkill(SkillName.DetectHidden, 100.0);
            SetSkill(SkillName.Magery, 77.6, 87.5);
            SetSkill(SkillName.EvalInt, 77.6, 87.5);
            SetSkill(SkillName.Meditation, 100.0);

            Fame = 20000;
            Karma = -20000;

            VirtualArmor = 44;
        }

        public BoneDemon(Serial serial)
            : base(serial)
        {
        }

        public override bool BardImmunity => !Core.SE;
        public override bool Unprovokable => Core.SE;
        public override bool AreaPeaceImmunity => Core.SE;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 8);
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
