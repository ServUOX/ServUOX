namespace Server.Mobiles
{
    [CorpseName("a titans corpse")]
    public class Titan : BaseCreature
    {
        [Constructible]
        public Titan()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a titan";
            Body = 76;
            BaseSoundID = 609;

            SetStr(536, 585);
            SetDex(126, 145);
            SetInt(281, 305);

            SetHits(322, 351);

            SetDamage(13, 16);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 35, 45);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.EvalInt, 85.1, 100.0);
            SetSkill(SkillName.Magery, 85.1, 100.0);
            SetSkill(SkillName.MagicResist, 80.2, 110.0);
            SetSkill(SkillName.Tactics, 60.1, 80.0);
            SetSkill(SkillName.Wrestling, 40.1, 50.0);

            Fame = 11500;
            Karma = -11500;

            VirtualArmor = 40;

            if (Core.ML && Utility.RandomDouble() < .33)
                PackItem(Engines.Plants.Seed.RandomPeculiarSeed(1));

            if (0.1 > Utility.RandomDouble())
                PackItem(new Items.RoastPig());
        }

        public Titan(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 4;
        public override Poison PoisonImmunity => Poison.Regular;
        public override int TreasureMapLevel => 5;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Average);
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
