namespace Server.Mobiles
{
    [CorpseName("a bird corpse")]
    public class Bird : BaseCreature
    {
        [Constructible]
        public Bird()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            if (Utility.RandomBool())
            {
                Hue = 0x901;

                switch (Utility.Random(3))
                {
                    default: break;
                    case 0: Name = "a crow"; break;
                    case 2: Name = "a raven"; break;
                    case 1: Name = "a magpie"; break;
                }
            }
            else
            {
                Hue = Utility.RandomBirdHue();
                Name = NameList.RandomName("bird");
            }

            Body = 6;
            BaseSoundID = 0x1B;

            VirtualArmor = Utility.RandomMinMax(0, 6);

            SetStr(10);
            SetDex(25, 35);
            SetInt(10);

            SetDamage(0);
            SetDamageType(ResistanceType.Physical, 100);

            SetSkill(SkillName.Wrestling, 4.2, 6.4);
            SetSkill(SkillName.Tactics, 4.0, 6.0);
            SetSkill(SkillName.MagicResist, 4.0, 5.0);

            Fame = 150;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -6.9;
        }

        public Bird(Serial serial)
            : base(serial)
        {
        }

        public override MeatType MeatType => MeatType.Bird;
        public override int Meat => 1;
        public override int Feathers => 25;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;

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

    [CorpseName("a bird corpse")]
    public class TropicalBird : BaseCreature
    {
        [Constructible]
        public TropicalBird()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Hue = Utility.RandomBirdHue();
            Name = "a tropical bird";

            Body = 6;
            BaseSoundID = 0xBF;

            VirtualArmor = Utility.RandomMinMax(0, 6);

            SetStr(10);
            SetDex(25, 35);
            SetInt(10);

            SetDamage(0);
            SetDamageType(ResistanceType.Physical, 100);

            SetSkill(SkillName.Wrestling, 4.2, 6.4);
            SetSkill(SkillName.Tactics, 4.0, 6.0);
            SetSkill(SkillName.MagicResist, 4.0, 5.0);

            Fame = 150;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -6.9;
        }

        public TropicalBird(Serial serial)
            : base(serial)
        {
        }

        public override MeatType MeatType => MeatType.Bird;
        public override int Meat => 1;
        public override int Feathers => 25;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;

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

    [CorpseName("a chicken corpse")]
    public class Chicken : BaseCreature
    {
        [Constructible]
        public Chicken()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a chicken";
            Body = 0xD0;
            BaseSoundID = 0x6E;

            SetStr(5);
            SetDex(15);
            SetInt(5);

            SetHits(3);
            SetMana(0);

            SetDamage(1);
            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 1, 5);

            SetSkill(SkillName.MagicResist, 4.0);
            SetSkill(SkillName.Tactics, 5.0);
            SetSkill(SkillName.Wrestling, 5.0);

            Fame = 150;
            Karma = 0;

            VirtualArmor = 2;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -0.9;
        }

        public Chicken(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override MeatType MeatType => MeatType.Bird;
        public override FoodType FavoriteFood => FoodType.GrainsAndHay;
        public override bool CanFly => true;
        public override int Feathers => 25;

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

    [CorpseName("an eagle corpse")]
    public class Eagle : BaseCreature
    {
        [Constructible]
        public Eagle()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "an eagle";
            Body = 5;
            BaseSoundID = 0x2EE;

            SetStr(31, 47);
            SetDex(36, 60);
            SetInt(8, 20);

            SetHits(20, 27);
            SetMana(0);

            SetDamage(5, 10);
            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 20, 25);
            SetResist(ResistanceType.Fire, 10, 15);
            SetResist(ResistanceType.Cold, 20, 25);
            SetResist(ResistanceType.Poison, 5, 10);
            SetResist(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.MagicResist, 15.3, 30.0);
            SetSkill(SkillName.Tactics, 18.1, 37.0);
            SetSkill(SkillName.Wrestling, 20.1, 30.0);

            Fame = 300;
            Karma = 0;

            VirtualArmor = 22;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 17.1;
        }

        public Eagle(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override MeatType MeatType => MeatType.Bird;
        public override int Feathers => 36;
        public override FoodType FavoriteFood => FoodType.Meat | FoodType.Fish;
        public override bool CanFly => true;

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
