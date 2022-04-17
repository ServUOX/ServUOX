namespace Server.Mobiles
{
    [CorpseName("a dread warhorse corpse")]
    public class DreadWarhorse : BaseMount
    {
        [Constructible]
        public DreadWarhorse()
            : this("a dread warhorse")
        {
        }

        [Constructible]
        public DreadWarhorse(string name)
            : base(name, 0x74, 0x3EA7, AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0xA8;
            BodyValue = 116;
            Hue = 1175;

            SetStr(500, 555);
            SetDex(89, 125);
            SetInt(100, 160);

            SetHits(555, 650);

            SetDamage(ResistType.Phys, 40, 0, 20, 26);
            SetDamage(ResistType.Pois, 20);
            SetDamage(ResistType.Engy, 40);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 20, 40);
            SetResist(ResistType.Cold, 20, 40);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.EvalInt, 15.2, 19.3);
            SetSkill(SkillName.Magery, 39.5, 49.5);
            SetSkill(SkillName.MagicResist, 91.4, 93.4);
            SetSkill(SkillName.Tactics, 108.1, 110.0);
            SetSkill(SkillName.Wrestling, 97.3, 98.2);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 60;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 108.0;
        }

        public DreadWarhorse(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 5;
        public override int Hides => 10;
        public override HideType HideType => HideType.Barbed;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override bool CanAngerOnTame => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.Potions);
        }

        public override int GetAngerSound()
        {
            if (!Controlled)
                return 0x16A;

            return base.GetAngerSound();
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
