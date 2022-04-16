namespace Server.Mobiles
{
    [CorpseName("a wanderer of the void corpse")]
    public class WandererOfTheVoid : BaseCreature
    {
        [Constructible]
        public WandererOfTheVoid()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a wanderer of the void";
            Body = 316;
            BaseSoundID = 377;

            SetStr(111, 200);
            SetDex(101, 125);
            SetInt(301, 390);

            SetHits(351, 400);

            SetDamage(ResistType.Phys, 0, 0, 11, 13);
            SetDamage(ResistType.Cold, 15);
            SetDamage(ResistType.Engy, 85);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 15, 25);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 50, 75);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.EvalInt, 60.1, 70.0);
            SetSkill(SkillName.Magery, 60.1, 70.0);
            SetSkill(SkillName.Meditation, 60.1, 70.0);
            SetSkill(SkillName.MagicResist, 50.1, 75.0);
            SetSkill(SkillName.Tactics, 60.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 70.0);

            Fame = 20000;
            Karma = -20000;

            VirtualArmor = 44;
        }

        public WandererOfTheVoid(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override int TreasureMapLevel => Core.AOS ? 4 : 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
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
