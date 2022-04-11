namespace Server.Mobiles
{
    [CorpseName("a cat corpse")]
    [TypeAlias("Server.Mobiles.Housecat")]
    public class Cat : BaseCreature
    {
        [Constructible]
        public Cat()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a cat";
            Body = 0xC9;
            Hue = Utility.RandomAnimalHue();
            BaseSoundID = 0x69;

            SetStr(9);
            SetDex(35);
            SetInt(5);

            SetHits(6);
            SetMana(0);

            SetDamage(1);
            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 5, 10);

            SetSkill(SkillName.MagicResist, 5.0);
            SetSkill(SkillName.Tactics, 4.0);
            SetSkill(SkillName.Wrestling, 5.0);
            SetSkill(SkillName.Hiding, 20.0);

            Fame = 0;
            Karma = 150;

            VirtualArmor = 8;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -0.9;
        }

        public Cat(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override FoodType FavoriteFood => FoodType.Meat | FoodType.Fish;
        public override PackInstinct PackInstinct => PackInstinct.Feline;

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

    [CorpseName("a cougar corpse")]
    public class Cougar : BaseCreature
    {
        [Constructible]
        public Cougar()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a cougar";
            Body = 63;
            BaseSoundID = 0x73;

            SetStr(56, 80);
            SetDex(66, 85);
            SetInt(26, 50);

            SetHits(34, 48);
            SetMana(0);

            SetDamage(4, 10);
            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 20, 25);
            SetResist(ResistanceType.Fire, 5, 10);
            SetResist(ResistanceType.Cold, 10, 15);
            SetResist(ResistanceType.Poison, 5, 10);

            SetSkill(SkillName.MagicResist, 15.1, 30.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 45.1, 60.0);

            Fame = 450;
            Karma = 0;

            VirtualArmor = 16;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 41.1;
        }

        public Cougar(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int Hides => 10;
        public override FoodType FavoriteFood => FoodType.Fish | FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Feline;

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

    [CorpseName("a panther corpse")]
    public class Panther : BaseCreature
    {
        [Constructible]
        public Panther()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a panther";
            Body = 0xD6;
            Hue = 0x901;
            BaseSoundID = 0x462;

            SetStr(61, 85);
            SetDex(86, 105);
            SetInt(26, 50);

            SetHits(37, 51);
            SetMana(0);

            SetDamage(4, 12);
            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 20, 25);
            SetResist(ResistanceType.Fire, 5, 10);
            SetResist(ResistanceType.Cold, 10, 15);
            SetResist(ResistanceType.Poison, 5, 10);

            SetSkill(SkillName.MagicResist, 15.1, 30.0);
            SetSkill(SkillName.Tactics, 50.1, 65.0);
            SetSkill(SkillName.Wrestling, 50.1, 65.0);

            Fame = 450;
            Karma = 0;

            VirtualArmor = 16;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 53.1;
        }

        public Panther(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int Hides => 10;
        public override FoodType FavoriteFood => FoodType.Meat | FoodType.Fish;
        public override PackInstinct PackInstinct => PackInstinct.Feline;

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

    [CorpseName("a leopard corpse")]
    [TypeAlias("Server.Mobiles.Snowleopard")]
    public class SnowLeopard : BaseCreature
    {
        [Constructible]
        public SnowLeopard()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a snow leopard";
            Body = Utility.RandomList(64, 65);
            BaseSoundID = 0x73;

            SetStr(56, 80);
            SetDex(66, 85);
            SetInt(26, 50);

            SetHits(34, 48);
            SetMana(0);

            SetDamage(3, 9);
            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 20, 25);
            SetResist(ResistanceType.Fire, 5, 10);
            SetResist(ResistanceType.Cold, 30, 40);
            SetResist(ResistanceType.Poison, 10, 20);
            SetResist(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 25.1, 35.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 40.1, 50.0);

            Fame = 450;
            Karma = 0;

            VirtualArmor = 24;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 53.1;
        }

        public SnowLeopard(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int Hides => 8;
        public override FoodType FavoriteFood => FoodType.Meat | FoodType.Fish;
        public override PackInstinct PackInstinct => PackInstinct.Feline;

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
