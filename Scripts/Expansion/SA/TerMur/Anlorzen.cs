using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an anlorzen corpse")]
    public class Anlorzen : BaseVoidCreature
    {
        public override VoidEvolution Evolution => VoidEvolution.Grouping;
        public override int Stage => 1;

        [Constructible]
        public Anlorzen()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "anlorzen";
            Body = 11;
            BaseSoundID = 1170;

            SetStr(600, 750);
            SetDex(666, 800);
            SetInt(850, 1000);

            SetHits(300, 400);
            SetDamage(15, 18);

            SetDamageType(ResistType.Physical, 20);
            SetDamageType(ResistType.Fire, 20);
            SetDamageType(ResistType.Cold, 20);
            SetDamageType(ResistType.Poison, 20);
            SetDamageType(ResistType.Energy, 20);

            SetResist(ResistType.Physical, 40, 50);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 30, 50);
            SetResist(ResistType.Poison, 100);
            SetResist(ResistType.Energy, 40, 60);

            SetSkill(SkillName.MagicResist, 30.1, 60.0);
            SetSkill(SkillName.Tactics, 30.1, 70.0);
            SetSkill(SkillName.Wrestling, 50.1, 70.0);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 56;

            PackItem(new DaemonBone(5));
        }

        public Anlorzen(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override bool AlwaysMurderer => true;
        public override bool BardImmunity => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
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

            if (BaseSoundID == 263)
                BaseSoundID = 1170;
        }
    }
}
