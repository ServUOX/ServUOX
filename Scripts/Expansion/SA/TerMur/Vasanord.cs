using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a plant corpse")]
    public class Vasanord : BaseVoidCreature
    {
        public override VoidEvolution Evolution => VoidEvolution.Survival;
        public override int Stage => 3;

        [Constructible]
        public Vasanord() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.6, 1.2)
        {
            Name = "vasanord";
            Body = 780;

            SetStr(805, 869);
            SetDex(51, 64);
            SetInt(38, 48);

            SetHits(5000, 5200);
            SetMana(40, 70);
            SetStam(50, 80);

            SetDamage(10, 23);

            SetDamageType(ResistType.Phys, 20);
            SetDamageType(ResistType.Fire, 20);
            SetDamageType(ResistType.Cold, 20);
            SetDamageType(ResistType.Pois, 20);
            SetDamageType(ResistType.Engy, 20);

            SetResist(ResistType.Phys, 30, 50);
            SetResist(ResistType.Fire, 20, 50);
            SetResist(ResistType.Cold, 20, 40);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 20, 50);

            SetSkill(SkillName.MagicResist, 72.8, 77.7);
            SetSkill(SkillName.Tactics, 50.7, 110.0);
            SetSkill(SkillName.EvalInt, 99.5, 120.0);
            SetSkill(SkillName.Magery, 95.5, 106.9);
            SetSkill(SkillName.Wrestling, 53.6, 98.6);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 28;

            PackItem(new DaemonBone(30));
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.6)
                c.DropItem(new TaintedSeeds(2));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
        }

        public override bool BardImmunity => !Core.AOS;
        public override Poison PoisonImmunity => Poison.Lethal;

        public Vasanord(Serial serial) : base(serial)
        {
        }

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
