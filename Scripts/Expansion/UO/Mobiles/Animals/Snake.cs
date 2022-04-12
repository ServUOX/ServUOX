namespace Server.Mobiles
{
    [CorpseName("a snake corpse")]
    public class Snake : BaseCreature
    {
        [Constructible]
        public Snake()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a snake";
            Body = 52;
            Hue = Utility.RandomSnakeHue();
            BaseSoundID = 0xDB;

            SetStr(22, 34);
            SetDex(16, 25);
            SetInt(6, 10);

            SetHits(15, 19);
            SetMana(0);

            SetDamage(ResistType.Phys, 100, 0, 1, 4);

            SetResist(ResistType.Phys, 15, 20);
            SetResist(ResistType.Pois, 20, 30);

            SetSkill(SkillName.Poisoning, 50.1, 70.0);
            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 19.3, 34.0);
            SetSkill(SkillName.Wrestling, 19.3, 34.0);

            Fame = 300;
            Karma = -300;

            VirtualArmor = 16;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 59.1;
        }

        public Snake(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Lesser;
        public override Poison HitPoison => Poison.Lesser;
        public override bool DeathAdderCharmable => true;
        public override int Meat => 1;
        public override FoodType FavoriteFood => FoodType.Eggs;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
