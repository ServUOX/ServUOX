namespace Server.Mobiles
{
    [CorpseName("an ostard corpse")]
    public class DesertOstard : BaseMount
    {
        [Constructible]
        public DesertOstard()
            : this("a desert ostard")
        {
        }

        [Constructible]
        public DesertOstard(string name)
            : base(name, 0xD2, 0x3EA3, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0x270;

            SetStr(94, 170);
            SetDex(56, 75);
            SetInt(6, 10);

            SetHits(71, 88);
            SetMana(0);

            SetDamage(ResistType.Phys, 100, 0, 5, 11);

            SetResist(ResistType.Phys, 15, 20);
            SetResist(ResistType.Fire, 5, 15);

            SetSkill(SkillName.MagicResist, 25.1, 30.0);
            SetSkill(SkillName.Tactics, 25.3, 40.0);
            SetSkill(SkillName.Wrestling, 29.3, 44.0);

            Fame = 450;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 29.1;
        }

        public DesertOstard(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 3;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
        public override PackInstinct PackInstinct => PackInstinct.Ostard;

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

    [CorpseName("an ostard corpse")]
    public class ForestOstard : BaseMount
    {
        [Constructible]
        public ForestOstard()
            : this("a forest ostard")
        {
        }

        [Constructible]
        public ForestOstard(string name)
            : base(name, 0xDB, 0x3EA5, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Hue = Utility.RandomSlimeHue() | 0x8000;

            BaseSoundID = 0x270;

            SetStr(94, 170);
            SetDex(56, 75);
            SetInt(6, 10);

            SetHits(71, 88);
            SetMana(0);

            SetDamage(ResistType.Phys, 100, 0, 8, 14);

            SetResist(ResistType.Phys, 15, 20);

            SetSkill(SkillName.MagicResist, 27.1, 32.0);
            SetSkill(SkillName.Tactics, 29.3, 44.0);
            SetSkill(SkillName.Wrestling, 29.3, 44.0);

            Fame = 450;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 29.1;
        }

        public ForestOstard(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 3;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
        public override PackInstinct PackInstinct => PackInstinct.Ostard;

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

    [CorpseName("an ostard corpse")]
    public class FrenziedOstard : BaseMount
    {
        [Constructible]
        public FrenziedOstard()
            : this("a frenzied ostard")
        {
        }

        [Constructible]
        public FrenziedOstard(string name)
            : base(name, 0xDA, 0x3EA4, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Hue = Utility.RandomHairHue() | 0x8000;

            BaseSoundID = 0x275;

            SetStr(94, 170);
            SetDex(96, 115);
            SetInt(6, 10);

            SetHits(71, 110);
            SetMana(0);

            SetDamage(ResistType.Phys, 100, 0, 11, 17);

            SetResist(ResistType.Phys, 25, 30);
            SetResist(ResistType.Fire, 10, 15);
            SetResist(ResistType.Pois, 20, 25);
            SetResist(ResistType.Engy, 20, 25);

            SetSkill(SkillName.MagicResist, 75.1, 80.0);
            SetSkill(SkillName.Tactics, 79.3, 94.0);
            SetSkill(SkillName.Wrestling, 79.3, 94.0);

            Fame = 1500;
            Karma = -1500;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 77.1;
        }

        public FrenziedOstard(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 3;
        public override FoodType FavoriteFood => FoodType.Meat | FoodType.Fish | FoodType.Eggs | FoodType.FruitsAndVegies;
        public override PackInstinct PackInstinct => PackInstinct.Ostard;

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
