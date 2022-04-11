namespace Server.Mobiles
{

    [CorpseName("an eowmu corpse")]
    public class Eowmu : BaseMount
    {
        [Constructible]
        public Eowmu()
            : this("Eowmu")
        {
        }

        [Constructible]
        public Eowmu(string name)
            : base(name, 1440, 16079, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0xA8;

            SetStr(400);
            SetDex(125);
            SetInt(50, 55);

            SetHits(240);
            SetMana(0);

            SetDamage(1, 4);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 40, 50);
            SetResist(ResistanceType.Fire, 30, 40);
            SetResist(ResistanceType.Cold, 30, 40);
            SetResist(ResistanceType.Poison, 30, 40);
            SetResist(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 25.0, 30.0);
            SetSkill(SkillName.Tactics, 30.0, 40.0);
            SetSkill(SkillName.Wrestling, 30.0, 35.0);

            Fame = 300;
            Karma = 300;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 30.0;
        }

        public Eowmu(Serial serial)
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
