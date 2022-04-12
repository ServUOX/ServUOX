namespace Server.Mobiles
{
    [CorpseName("a shadow wyrm corpse")]
    public class ShadowWyrm : BaseCreature
    {
        [Constructible]
        public ShadowWyrm()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a shadow wyrm";
            Body = 106;
            BaseSoundID = 362;

            SetStr(898, 1030);
            SetDex(68, 200);
            SetInt(488, 620);

            SetHits(558, 599);

            SetDamage(ResistType.Phys, 75, 0, 29, 35);
            SetDamage(ResistType.Cold, 25);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 45, 55);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.EvalInt, 80.1, 100.0);
            SetSkill(SkillName.Magery, 80.1, 100.0);
            SetSkill(SkillName.Meditation, 52.5, 75.0);
            SetSkill(SkillName.MagicResist, 100.3, 130.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 97.6, 100.0);
            SetSkill(SkillName.DetectHidden, 90.0, 100.0);
            SetSkill(SkillName.Necromancy, 80.0, 90.0);
            SetSkill(SkillName.SpiritSpeak, 100.0, 105.0);

            Fame = 22500;
            Karma = -22500;

            VirtualArmor = 70;

            Tamable = true;
            ControlSlots = 5;
            MinTameSkill = 105.0;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public ShadowWyrm(Serial serial)
            : base(serial)
        {
        }

        public override bool CanAngerOnTame => true;
        public override bool ReacquireOnMovement => !Controlled;
        public override bool AutoDispel => !Controlled;
        public override Poison PoisonImmunity => Poison.Deadly;
        public override Poison HitPoison => Poison.Deadly;
        public override int TreasureMapLevel => 5;
        public override int Meat => 19;
        public override int Hides => 20;
        public override int Scales => 10;
        public override ScaleType ScaleType => ScaleType.Black;
        public override HideType HideType => HideType.Barbed;
        public override bool CanFly => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.Gems, 5);
        }

        public override int GetIdleSound()
        {
            return 0x2D5;
        }

        public override int GetHurtSound()
        {
            return 0x2D1;
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
