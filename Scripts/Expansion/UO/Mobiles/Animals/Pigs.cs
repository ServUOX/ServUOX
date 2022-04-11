
namespace Server.Mobiles
{
    [CorpseName("a pig corpse")]
    public class Pig : BaseCreature
    {
        [Constructible]
        public Pig()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a pig";
            Body = 0xCB;
            BaseSoundID = 0xC4;

            SetStr(20);
            SetDex(20);
            SetInt(5);

            SetHits(12);
            SetMana(0);

            SetDamage(2, 4);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 10, 15);

            SetSkill(SkillName.MagicResist, 5.0);
            SetSkill(SkillName.Tactics, 5.0);
            SetSkill(SkillName.Wrestling, 5.0);

            Fame = 150;
            Karma = 0;

            VirtualArmor = 12;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 11.1;
        }

        public Pig(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
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

    [CorpseName("a pig corpse")]
    public class Boar : BaseCreature
    {
        [Constructible]
        public Boar()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a boar";
            Body = 0x122;
            BaseSoundID = 0xC4;

            SetStr(25);
            SetDex(15);
            SetInt(5);

            SetHits(15);
            SetMana(0);

            SetDamage(3, 6);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 10, 15);
            SetResist(ResistanceType.Fire, 5, 10);
            SetResist(ResistanceType.Poison, 5, 10);

            SetSkill(SkillName.MagicResist, 9.0);
            SetSkill(SkillName.Tactics, 9.0);
            SetSkill(SkillName.Wrestling, 9.0);

            Fame = 300;
            Karma = 0;

            VirtualArmor = 10;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 29.1;
        }

        public Boar(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 2;
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
}
