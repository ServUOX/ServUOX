
namespace Server.Mobiles
{
    [CorpseName("a balron corpse")]
    public class Balron : BaseCreature
    {
        [Constructible]
        public Balron()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("balron");
            Body = 40;
            BaseSoundID = 357;

            SetStr(986, 1185);
            SetDex(177, 255);
            SetInt(151, 250);

            SetHits(592, 711);

            SetDamage(22, 29);

            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Fire, 25);
            SetDamageType(ResistType.Engy, 25);

            SetResist(ResistType.Phys, 65, 80);
            SetResist(ResistType.Fire, 60, 80);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.Anatomy, 25.1, 50.0);
            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 95.5, 100.0);
            SetSkill(SkillName.Meditation, 25.1, 50.0);
            SetSkill(SkillName.MagicResist, 100.5, 150.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 24000;
            Karma = -24000;

            VirtualArmor = 90;
        }

        public Balron(Serial serial)
            : base(serial)
        {
        }

        public override bool CanFly => true;

        public override bool CanRummageCorpses => true;
        public override Poison PoisonImmunity => Poison.Deadly;
        public override int TreasureMapLevel => 5;
        public override int Meat => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
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
