using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an anlorvaglem corpse")]
    public class Anlorvaglem : BaseVoidCreature
    {
        public override VoidEvolution Evolution => VoidEvolution.Grouping;
        public override int Stage => 3;

        [Constructible]
        public Anlorvaglem()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.6, 1.2)
        {
            Name = "anlorvaglem";
            Hue = 2071;
            Body = 152;

            SetStr(1000, 1200);
            SetDex(1000, 1200);
            SetInt(100, 1200);

            SetHits(3205);

            SetDamage(11, 13);

            SetDamageType(ResistType.Phys, 20);
            SetDamageType(ResistType.Fire, 20);
            SetDamageType(ResistType.Cold, 20);
            SetDamageType(ResistType.Pois, 20);
            SetDamageType(ResistType.Engy, 20);

            SetResist(ResistType.Phys, 20, 50);
            SetResist(ResistType.Fire, 20, 60);
            SetResist(ResistType.Cold, 20, 58);
            SetResist(ResistType.Pois, 80, 100);
            SetResist(ResistType.Engy, 30, 50);

            SetSkill(SkillName.Wrestling, 75.8, 100.0);
            SetSkill(SkillName.Tactics, 50.0, 100.0);
            SetSkill(SkillName.MagicResist, 50.9, 90.0);

            Fame = 8000;
            Karma = -8000;

            VirtualArmor = 48;

            PackItem(new DaemonBone(30));
        }

        public Anlorvaglem(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Lethal;
        public override bool Unprovokable => true;
        public override bool BardImmunity => true;
        public override bool ReacquireOnMovement => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich);
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
