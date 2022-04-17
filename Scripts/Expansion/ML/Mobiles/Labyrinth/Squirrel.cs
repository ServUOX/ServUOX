namespace Server.Mobiles
{
    [CorpseName("a squirrel corpse")]
    public class Squirrel : BaseCreature
    {
        [Constructible]
        public Squirrel()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a squirrel";
            Body = 0x116;

            SetStr(44, 50);
            SetDex(35);
            SetInt(5);

            SetHits(42, 50);

            SetDamage(ResistType.Phys, 100, 0, 1, 2);

            SetResist(ResistType.Phys, 30, 34);
            SetResist(ResistType.Fire, 10, 14);
            SetResist(ResistType.Cold, 30, 35);
            SetResist(ResistType.Pois, 20, 25);
            SetResist(ResistType.Engy, 20, 25);

            SetSkill(SkillName.MagicResist, 4.0);
            SetSkill(SkillName.Tactics, 4.0);
            SetSkill(SkillName.Wrestling, 4.0);

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -21.3;
        }

        public Squirrel(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
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
