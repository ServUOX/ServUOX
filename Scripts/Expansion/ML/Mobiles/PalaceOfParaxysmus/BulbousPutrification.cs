namespace Server.Mobiles
{
    [CorpseName("a bulbous putrification corpse")]
    public class BulbousPutrification : BaseCreature
    {
        [Constructible]
        public BulbousPutrification()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a bulbous putrification";
            Body = 0x307;
            Hue = 0x55C;
            BaseSoundID = 0x165;

            SetStr(755, 800);
            SetDex(53, 60);
            SetInt(51, 59);

            SetHits(1211, 1231);

            SetDamage(22, 29);

            SetDamageType(ResistType.Phys, 60);
            SetDamageType(ResistType.Pois, 40);

            SetResist(ResistType.Phys, 55, 65);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 55, 70);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.Wrestling, 104.8, 114.7);
            SetSkill(SkillName.Tactics, 111.9, 119.1);
            SetSkill(SkillName.MagicResist, 55.5, 64.1);
            SetSkill(SkillName.Anatomy, 110.0);
            SetSkill(SkillName.Poisoning, 80.0);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 28;
        }

        public BulbousPutrification(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosFilthyRich, 5);
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
