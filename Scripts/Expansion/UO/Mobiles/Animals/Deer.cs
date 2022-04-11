namespace Server.Mobiles
{
    [CorpseName("a deer corpse")]
    public class Hind : BaseCreature
    {
        [Constructible]
        public Hind()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a hind";
            Body = 0xED;

            SetStr(21, 51);
            SetDex(47, 77);
            SetInt(17, 47);

            SetHits(15, 29);
            SetMana(0);

            SetDamage(4);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 5, 15);
            SetResist(ResistType.Cold, 5);

            SetSkill(SkillName.MagicResist, 15.0);
            SetSkill(SkillName.Tactics, 19.0);
            SetSkill(SkillName.Wrestling, 26.0);

            Fame = 300;
            Karma = 0;

            VirtualArmor = 8;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 23.1;
        }

        public Hind(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 5;
        public override int Hides => 8;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;

        public override int GetAttackSound() { return 0x82; }
        public override int GetHurtSound() { return 0x83; }
        public override int GetDeathSound() { return 0x84; }

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

    [CorpseName("a deer corpse")]
    [TypeAlias("Server.Mobiles.Greathart")]
    public class GreatHart : BaseCreature
    {
        [Constructible]
        public GreatHart()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a great hart";
            Body = 0xEA;

            SetStr(41, 71);
            SetDex(47, 77);
            SetInt(27, 57);

            SetHits(27, 41);
            SetMana(0);

            SetDamage(5, 9);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 20, 25);
            SetResist(ResistType.Cold, 5, 10);

            SetSkill(SkillName.MagicResist, 26.8, 44.5);
            SetSkill(SkillName.Tactics, 29.8, 47.5);
            SetSkill(SkillName.Wrestling, 29.8, 47.5);

            Fame = 300;
            Karma = 0;

            VirtualArmor = 24;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 59.1;
        }

        public GreatHart(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 6;
        public override int Hides => 15;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;

        public override int GetAttackSound() { return 0x82; }
        public override int GetHurtSound() { return 0x83; }
        public override int GetDeathSound() { return 0x84; }

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
