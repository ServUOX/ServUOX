using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a centaur corpse")]
    public class Centaur : BaseCreature
    {
        [Constructible]
        public Centaur()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("centaur");
            Body = 101;
            BaseSoundID = 679;

            SetStr(202, 300);
            SetDex(104, 260);
            SetInt(91, 100);

            SetHits(130, 172);

            SetDamage(ResistType.Phys, 100, 0, 13, 24);

            SetResist(ResistType.Phys, 45, 55);
            SetResist(ResistType.Fire, 35, 45);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Pois, 45, 55);
            SetResist(ResistType.Engy, 35, 45);

            SetSkill(SkillName.Anatomy, 95.1, 115.0);
            SetSkill(SkillName.Archery, 95.1, 100.0);
            SetSkill(SkillName.MagicResist, 50.3, 80.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 95.1, 100.0);

            Fame = 6500;
            Karma = 0;

            VirtualArmor = 50;
            AddItem(new Bow());
            PackItem(new Arrow(Utility.RandomMinMax(80, 90))); // OSI it is different: in a sub backpack, this is probably just a limitation of their engine
        }

        public Centaur(Serial serial)
            : base(serial)
        {
        }

        public override TribeType Tribe => TribeType.Fey;

        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;
        public override int Meat => 1;
        public override int Hides => 8;
        public override HideType HideType => HideType.Spined;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems);
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
