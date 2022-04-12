namespace Server.Mobiles
{
    [CorpseName("a darknight creeper corpse")]
    public class DarknightCreeper : BaseCreature
    {
        [Constructible]
        public DarknightCreeper()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("darknight creeper");
            Body = 313;
            BaseSoundID = 0xE0;

            SetStr(301, 330);
            SetDex(101, 110);
            SetInt(301, 330);

            SetHits(4000);

            SetDamage(22, 26);

            SetDamageType(ResistType.Phys, 85);
            SetDamageType(ResistType.Pois, 15);

            SetResist(ResistType.Phys, 60);
            SetResist(ResistType.Fire, 60);
            SetResist(ResistType.Cold, 100);
            SetResist(ResistType.Pois, 90);
            SetResist(ResistType.Engy, 75);

            SetSkill(SkillName.Wrestling, 90.1, 90.9);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 90.1, 90.9);
            SetSkill(SkillName.Poisoning, 120.0);
            SetSkill(SkillName.DetectHidden, 100.0);
            SetSkill(SkillName.Magery, 112.6, 120.0);
            SetSkill(SkillName.EvalInt, 118.1, 120.0);
            SetSkill(SkillName.Meditation, 150.0);

            Fame = 22000;
            Karma = -22000;

            VirtualArmor = 34;
        }

        public DarknightCreeper(Serial serial)
            : base(serial)
        {
        }

        public override bool CanFlee => false;
        public override bool IgnoreYoungProtection => Core.ML;
        public override bool BardImmunity => !Core.SE;
        public override bool Unprovokable => Core.SE;
        public override bool AreaPeaceImmunity => Core.SE;
        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
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
