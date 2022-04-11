namespace Server.Mobiles
{
    [CorpseName("a skeletal dragon corpse")]
    public class SkeletalDragon : BaseCreature
    {
        [Constructible]
        public SkeletalDragon()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a skeletal dragon";
            Body = 104;
            BaseSoundID = 0x488;

            SetStr(898, 1030);
            SetDex(68, 200);
            SetInt(488, 620);

            SetHits(558, 599);

            SetDamage(29, 35);

            SetDamageType(ResistType.Physical, 75);
            SetDamageType(ResistType.Fire, 25);

            SetResist(ResistType.Physical, 75, 80);
            SetResist(ResistType.Fire, 40, 60);
            SetResist(ResistType.Cold, 40, 60);
            SetResist(ResistType.Poison, 70, 80);
            SetResist(ResistType.Energy, 40, 60);

            SetSkill(SkillName.EvalInt, 80.1, 100.0);
            SetSkill(SkillName.Magery, 80.1, 100.0);
            SetSkill(SkillName.MagicResist, 100.3, 130.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 97.6, 100.0);
            SetSkill(SkillName.Necromancy, 80.1, 100.0);
            SetSkill(SkillName.SpiritSpeak, 80.1, 100.0);

            Fame = 22500;
            Karma = -22500;

            VirtualArmor = 80;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public SkeletalDragon(Serial serial)
            : base(serial)
        {
        }

        public override bool AutoDispel => !Controlled;
        public override bool BleedImmunity => true;
        public override bool ReacquireOnMovement => !Controlled;
        public override double BonusPetDamageScalar => (Core.SE) ? 3.0 : 1.0;
        public override int Hides => 20;
        public override int Meat => 19;
        public override HideType HideType => HideType.Barbed;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override TribeType Tribe => TribeType.Undead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 4);
            AddLoot(LootPack.Gems, 5);
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
