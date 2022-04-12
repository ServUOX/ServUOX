namespace Server.Mobiles
{
    [CorpseName("a walrus corpse")]
    public class Walrus : BaseCreature
    {
        [Constructible]
        public Walrus()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a walrus";
            Body = 0xDD;
            BaseSoundID = 0xE0;

            SetStr(21, 29);
            SetDex(46, 55);
            SetInt(16, 20);

            SetHits(14, 17);
            SetMana(0);

            SetDamage(4, 10);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 20, 25);
            SetResist(ResistType.Fire, 5, 10);
            SetResist(ResistType.Cold, 20, 25);
            SetResist(ResistType.Pois, 5, 10);
            SetResist(ResistType.Engy, 5, 10);

            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 19.2, 29.0);
            SetSkill(SkillName.Wrestling, 19.2, 29.0);

            Fame = 150;
            Karma = 0;

            VirtualArmor = 18;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 35.1;
        }

        public Walrus(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int Hides => 12;
        public override FoodType FavoriteFood => FoodType.Fish;

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
