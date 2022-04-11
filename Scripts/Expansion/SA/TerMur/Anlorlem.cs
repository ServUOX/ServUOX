using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an anlorlem corpse")]
    public class Anlorlem : BaseVoidCreature
    {
        public override VoidEvolution Evolution => VoidEvolution.Grouping;
        public override int Stage => 2;

        [Constructible]
        public Anlorlem()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "anlorlem";
            Body = 72;
            Hue = 2071;
            BaseSoundID = 644;

            SetStr(900, 1000);
            SetDex(1000, 1200);
            SetInt(900, 950);

            SetHits(500, 650);

            SetDamage(18, 22);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResist(ResistanceType.Physical, 45, 55);
            SetResist(ResistanceType.Fire, 30, 40);
            SetResist(ResistanceType.Cold, 35, 45);
            SetResist(ResistanceType.Poison, 90, 100);
            SetResist(ResistanceType.Energy, 35, 45);

            SetSkill(SkillName.MagicResist, 40.1, 70.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 16000;
            Karma = -16000;

            VirtualArmor = 50;

            PackItem(new DaemonBone(15));
        }

        public Anlorlem(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 3;
        public override bool BardImmunity => !Core.AOS;
        public override bool Unprovokable => true;
        public override bool ReacquireOnMovement => true;
        public override Poison PoisonImmunity => Poison.Greater;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.MedScrolls, 2);
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
