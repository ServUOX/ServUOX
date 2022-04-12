namespace Server.Mobiles
{
    [CorpseName("a giant ice worm corpse")]
    public class GiantIceWorm : BaseCreature
    {
        [Constructible]
        public GiantIceWorm()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 89;
            Name = "a giant ice worm";
            BaseSoundID = 0xDC;

            SetStr(216, 245);
            SetDex(76, 100);
            SetInt(66, 85);

            SetHits(130, 147);

            SetDamage(7, 17);

            SetDamageType(ResistType.Phys, 10);
            SetDamageType(ResistType.Cold, 90);

            SetResist(ResistType.Phys, 30, 35);
            SetResist(ResistType.Fire, 0);
            SetResist(ResistType.Cold, 80, 90);
            SetResist(ResistType.Pois, 15, 25);
            SetResist(ResistType.Engy, 10, 20);

            SetSkill(SkillName.Poisoning, 75.1, 95.0);
            SetSkill(SkillName.MagicResist, 45.1, 60.0);
            SetSkill(SkillName.Tactics, 75.1, 80.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 40;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 71.1;
        }

        public GiantIceWorm(Serial serial)
            : base(serial)
        {
        }

        public override bool SubdueBeforeTame => true;
        public override Poison PoisonImmunity => Poison.Greater;
        public override Poison HitPoison => Poison.Greater;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override bool StatLossAfterTame => true;

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
