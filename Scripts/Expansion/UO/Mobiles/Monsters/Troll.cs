namespace Server.Mobiles
{
    [CorpseName("a troll corpse")]
    public class Troll : BaseCreature
    {
        [Constructible]
        public Troll()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a troll";
            Body = Utility.RandomList(53, 54);
            BaseSoundID = 461;

            SetStr(176, 205);
            SetDex(46, 65);
            SetInt(46, 70);

            SetHits(106, 123);

            SetDamage(8, 14);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 35, 45);
            SetResist(ResistType.Fire, 25, 35);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Poison, 5, 15);
            SetResist(ResistType.Energy, 5, 15);

            SetSkill(SkillName.MagicResist, 45.1, 60.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 50.1, 70.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 40;
        }

        public Troll(Serial serial)
            : base(serial)
        {
        }

        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 1;
        public override int Meat => 2;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
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
