namespace Server.Mobiles
{
    [CorpseName("a kaze kemono corpse")]
    public class KazeKemono : BaseCreature
    {

        [Constructible]
        public KazeKemono()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a kaze kemono";
            Body = 196;
            BaseSoundID = 655;

            SetStr(201, 275);
            SetDex(101, 155);
            SetInt(101, 105);

            SetHits(251, 330);

            SetDamage(ResistType.Phys, 70, 0, 15, 20);
            SetDamage(ResistType.Fire, 10);
            SetDamage(ResistType.Cold, 10);
            SetDamage(ResistType.Pois, 10);

            SetResist(ResistType.Phys, 50, 70);
            SetResist(ResistType.Fire, 30, 60);
            SetResist(ResistType.Cold, 30, 60);
            SetResist(ResistType.Pois, 50, 70);
            SetResist(ResistType.Engy, 60, 80);

            SetSkill(SkillName.MagicResist, 110.1, 125.0);
            SetSkill(SkillName.Tactics, 55.1, 65.0);
            SetSkill(SkillName.Wrestling, 85.1, 95.0);
            SetSkill(SkillName.Anatomy, 25.1, 35.0);
            SetSkill(SkillName.Magery, 95.1, 105.0);

            Fame = 8000;
            Karma = -8000;

            SetSpecialAbility(SpecialAbility.ConductiveBlast);
            SetSpecialAbility(SpecialAbility.FlurryForce);
        }

        public KazeKemono(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 3);
        }

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
