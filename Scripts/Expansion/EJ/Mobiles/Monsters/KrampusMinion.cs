namespace Server.Mobiles
{
    [CorpseName("a minion corpse")]
    public class KrampusMinion : BaseCreature
    {
        [Constructible]
        public KrampusMinion()
           : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Minion";
            Body = 1485;
            BaseSoundID = 422;

            SetStr(476, 505);
            SetDex(76, 95);
            SetInt(301, 325);

            SetHits(286, 303);

            SetDamage(7, 14);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Cold, 50);

            SetResist(ResistanceType.Physical, 60, 70);
            SetResist(ResistanceType.Fire, 40, 50);
            SetResist(ResistanceType.Cold, 50, 60);
            SetResist(ResistanceType.Poison, 40, 50);
            SetResist(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.Tactics, 110, 120);
            SetSkill(SkillName.Wrestling, 100, 110);
            SetSkill(SkillName.DetectHidden, 60.0, 70.0);
            SetSkill(SkillName.MagicResist, 120);
            SetSkill(SkillName.Parry, 60, 70);

            Fame = 3000;
            Karma = -3000;
        }

        public KrampusMinion(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.Potions);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
