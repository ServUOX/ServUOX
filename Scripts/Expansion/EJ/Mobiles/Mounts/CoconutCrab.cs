namespace Server.Mobiles
{

    [CorpseName("a Coconut Crab corpse")]
    public class CoconutCrab : BaseMount
    {
        [Constructible]
        public CoconutCrab()
            : this("Coconut Crab")
        {
        }

        [Constructible]
        public CoconutCrab(string name)
            : base(name, 1510, 16081, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0x4F2;

            SetStr(400);
            SetDex(125);
            SetInt(50, 55);

            SetHits(240);
            SetMana(0);

            SetDamage(1, 4);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 40, 50);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 25.0, 30.0);
            SetSkill(SkillName.Tactics, 30.0, 40.0);
            SetSkill(SkillName.Wrestling, 30.0, 35.0);

            Fame = 300;
            Karma = 300;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 30.0;
        }

        public CoconutCrab(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 3;
        public override int Hides => 10;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies;

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
