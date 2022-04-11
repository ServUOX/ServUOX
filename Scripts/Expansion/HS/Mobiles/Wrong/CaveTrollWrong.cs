
namespace Server.Mobiles
{
    [CorpseName("a cave troll corpse")]
    public class CaveTrollWrong : BaseCreature
    {
        [Constructible]
        public CaveTrollWrong()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "cave troll";
            Body = Utility.RandomList(53, 54);
            BaseSoundID = 461;
            Hue = 674;

            SetStr(118, 118);
            SetDex(58, 58);
            SetInt(65, 65);

            SetHits(2136, 2136);

            SetDamage(8, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 76, 76);
            SetResist(ResistanceType.Fire, 55, 55);
            SetResist(ResistanceType.Cold, 72, 72);
            SetResist(ResistanceType.Poison, 75, 75);
            SetResist(ResistanceType.Energy, 50, 50);

            SetSkill(SkillName.MagicResist, 87.4, 87.4);
            SetSkill(SkillName.Tactics, 125.8, 125.8);
            SetSkill(SkillName.Wrestling, 137.5, 137.5);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 40;
        }

        public CaveTrollWrong(Serial serial)
            : base(serial)
        {
        }

        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 2;
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
            int version = reader.ReadInt();
        }
    }
}
