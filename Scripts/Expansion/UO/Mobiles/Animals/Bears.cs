
namespace Server.Mobiles
{
    [CorpseName("a bear corpse")]
    [TypeAlias("Server.Mobiles.Bear")]
    public class BlackBear : BaseCreature
    {
        [Constructible]
        public BlackBear()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a black bear";
            Body = 211;
            BaseSoundID = 0xA3;

            SetStr(76, 100);
            SetDex(56, 75);
            SetInt(11, 14);

            SetHits(46, 60);
            SetMana(0);

            SetDamage(4, 10);
            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 20, 25);
            SetResist(ResistanceType.Cold, 10, 15);
            SetResist(ResistanceType.Poison, 5, 10);

            SetSkill(SkillName.MagicResist, 20.1, 40.0);
            SetSkill(SkillName.Tactics, 40.1, 60.0);
            SetSkill(SkillName.Wrestling, 40.1, 60.0);

            Fame = 450;
            Karma = 0;

            VirtualArmor = 24;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 35.1;
        }

        public BlackBear(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int Hides => 12;
        public override FoodType FavoriteFood => FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies;
        public override PackInstinct PackInstinct => PackInstinct.Bear;

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

    [CorpseName("a bear corpse")]
    public class BrownBear : BaseCreature
    {
        [Constructible]
        public BrownBear()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a brown bear";
            Body = 167;
            BaseSoundID = 0xA3;

            SetStr(76, 100);
            SetDex(26, 45);
            SetInt(23, 47);

            SetHits(46, 60);
            SetMana(0);

            SetDamage(6, 12);
            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 20, 30);
            SetResist(ResistanceType.Cold, 15, 20);
            SetResist(ResistanceType.Poison, 10, 15);

            SetSkill(SkillName.MagicResist, 25.1, 35.0);
            SetSkill(SkillName.Tactics, 40.1, 60.0);
            SetSkill(SkillName.Wrestling, 40.1, 60.0);

            Fame = 450;
            Karma = 0;

            VirtualArmor = 24;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 41.1;
        }

        public BrownBear(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int Hides => 12;
        public override FoodType FavoriteFood => FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Bear;

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

    [CorpseName("a grizzly bear corpse")]
    [TypeAlias("Server.Mobiles.Grizzlybear")]
    public class GrizzlyBear : BaseCreature
    {
        [Constructible]
        public GrizzlyBear()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a grizzly bear";
            Body = 212;
            BaseSoundID = 0xA3;

            SetStr(126, 155);
            SetDex(81, 105);
            SetInt(16, 40);

            SetHits(76, 93);
            SetMana(0);

            SetDamage(8, 13);
            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 25, 35);
            SetResist(ResistanceType.Cold, 15, 25);
            SetResist(ResistanceType.Poison, 5, 10);
            SetResist(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.MagicResist, 25.1, 40.0);
            SetSkill(SkillName.Tactics, 70.1, 100.0);
            SetSkill(SkillName.Wrestling, 45.1, 70.0);

            Fame = 1000;
            Karma = 0;

            VirtualArmor = 24;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 59.1;
        }

        public GrizzlyBear(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 2;
        public override int Hides => 16;
        public override FoodType FavoriteFood => FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Bear;

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

    [CorpseName("a polar bear corpse")]
    [TypeAlias("Server.Mobiles.Polarbear")]
    public class PolarBear : BaseCreature
    {
        [Constructible]
        public PolarBear()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a polar bear";
            Body = 213;
            BaseSoundID = 0xA3;

            SetStr(116, 140);
            SetDex(81, 105);
            SetInt(26, 50);

            SetHits(70, 84);
            SetMana(0);

            SetDamage(7, 12);
            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 25, 35);
            SetResist(ResistanceType.Cold, 60, 80);
            SetResist(ResistanceType.Poison, 15, 25);
            SetResist(ResistanceType.Energy, 10, 15);

            SetSkill(SkillName.MagicResist, 45.1, 60.0);
            SetSkill(SkillName.Tactics, 60.1, 90.0);
            SetSkill(SkillName.Wrestling, 45.1, 70.0);

            Fame = 1500;
            Karma = 0;

            VirtualArmor = 18;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 35.1;
        }

        public PolarBear(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 2;
        public override int Hides => 16;
        public override FoodType FavoriteFood => FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Bear;

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
