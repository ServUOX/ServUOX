namespace Server.Mobiles
{
    [CorpseName("a coconut crab corpse")]
    public class HungryCoconutCrab : BaseCreature
    {
        [Constructible]
        public HungryCoconutCrab()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "hungry coconut crab";
            Body = 0x5E7;
            Hue = 2713;
            BaseSoundID = 0x4F2;

            SetStr(19);
            SetDex(15);
            SetInt(5);

            SetHits(12);

            SetDamage(3, 4);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 10, 20);

            SetSkill(SkillName.MagicResist, 5.0);
            SetSkill(SkillName.Tactics, 5.0);
            SetSkill(SkillName.Wrestling, 5.0);

            Fame = 0;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 0.0;
        }

        public HungryCoconutCrab(Serial serial)
            : base(serial)
        {
        }

        public override bool DeleteOnRelease => true;
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
