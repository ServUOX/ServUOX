namespace Server.Mobiles
{
    [CorpseName("a goat corpse")]
    public class Goat : BaseCreature
    {
        [Constructible]
        public Goat()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a goat";
            Body = 0xD1;
            BaseSoundID = 0x99;

            SetStr(19);
            SetDex(15);
            SetInt(5);

            SetHits(12);
            SetMana(0);

            SetDamage(3, 4);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 5, 15);

            SetSkill(SkillName.MagicResist, 5.0);
            SetSkill(SkillName.Tactics, 5.0);
            SetSkill(SkillName.Wrestling, 5.0);

            Fame = 150;
            Karma = 0;

            VirtualArmor = 10;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 11.1;
        }

        public Goat(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 2;
        public override int Hides => 8;
        public override FoodType FavoriteFood => FoodType.Leather;

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

    [CorpseName("a mountain goat corpse")]
    public class MountainGoat : BaseCreature
    {
        [Constructible]
        public MountainGoat()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a mountain goat";
            Body = 88;
            BaseSoundID = 0x99;

            SetStr(22, 64);
            SetDex(56, 75);
            SetInt(16, 30);

            SetHits(20, 33);
            SetMana(0);

            SetDamage(3, 7);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 10, 20);
            SetResist(ResistType.Fire, 5, 10);
            SetResist(ResistType.Cold, 10, 20);
            SetResist(ResistType.Poison, 10, 15);
            SetResist(ResistType.Energy, 10, 15);

            SetSkill(SkillName.MagicResist, 25.1, 30.0);
            SetSkill(SkillName.Tactics, 29.3, 44.0);
            SetSkill(SkillName.Wrestling, 29.3, 44.0);

            Fame = 300;
            Karma = 0;

            VirtualArmor = 10;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -0.9;
        }

        public MountainGoat(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 2;
        public override int Hides => 12;
        public override FoodType FavoriteFood => FoodType.GrainsAndHay | FoodType.FruitsAndVegies;

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
