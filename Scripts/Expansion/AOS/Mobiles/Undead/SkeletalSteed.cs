namespace Server.Mobiles
{
    [CorpseName("an undead horse corpse")]
    public class SkeletalSteed : BaseMount
    {
        [Constructible]
        public SkeletalSteed()
            : this("a skeletal steed")
        {
        }

        [Constructible]
        public SkeletalSteed(string name)
            : base(name, 793, 0x3EBB, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            SetStr(91, 100);
            SetDex(46, 55);
            SetInt(46, 60);

            SetHits(41, 50);

            SetDamage(ResistType.Phys, 50, 0, 5, 12);
            SetDamage(ResistType.Cold, 50);

            SetResist(ResistType.Phys, 50, 60);
            SetResist(ResistType.Cold, 90, 95);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 10, 15);

            SetSkill(SkillName.MagicResist, 95.1, 100.0);
            SetSkill(SkillName.Tactics, 50.0);
            SetSkill(SkillName.Wrestling, 70.1, 80.0);

            Fame = 0;
            Karma = 0;
        }

        public SkeletalSteed(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Lethal;
        public override bool BleedImmunity => true;

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
