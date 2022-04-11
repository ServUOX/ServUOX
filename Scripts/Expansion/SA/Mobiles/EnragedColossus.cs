namespace Server.Mobiles
{
    [CorpseName("the remains of an enraged colossus")]
    public class EnragedColossus : BaseCreature
    {
        [Constructible]
        public EnragedColossus()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.5)
        {
            Name = "a rising colossus";
            Body = 829;

            SetStr(600);
            SetDex(70);
            SetInt(80);

            SetDamage(18, 21);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 56, 63);
            SetResist(ResistanceType.Fire, 29, 30);
            SetResist(ResistanceType.Cold, 53, 54);
            SetResist(ResistanceType.Poison, 54, 58);
            SetResist(ResistanceType.Energy, 26, 29);

            SetSkill(SkillName.MagicResist, 115.0, 140.0);
            SetSkill(SkillName.Tactics, 120.0, 130.0);
            SetSkill(SkillName.Wrestling, 120.0, 140);

            VirtualArmor = 58;
            ControlSlots = 5;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 4);
            AddLoot(LootPack.Gems, 8);
        }

        public EnragedColossus(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 125.0;
        public override double DispelFocus => 45.0;
        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;// Immune to poison?

        public override int GetAttackSound() { return 0x627; }
        public override int GetHurtSound() { return 0x629; }

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
