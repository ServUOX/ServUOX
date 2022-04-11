using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a tangle corpse")]
    public class Tangle : BogThing
    {
        [Constructible]
        public Tangle()
            : base()
        {
            Name = "Tangle";
            Hue = 0x21;

            SetStr(843, 943);
            SetDex(58, 74);
            SetInt(46, 58);

            SetHits(2468, 2733);

            SetDamage(15, 28);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Poison, 40);

            SetResist(ResistanceType.Physical, 50, 57);
            SetResist(ResistanceType.Fire, 40, 43);
            SetResist(ResistanceType.Cold, 30, 35);
            SetResist(ResistanceType.Poison, 61, 69);
            SetResist(ResistanceType.Energy, 41, 45);

            SetSkill(SkillName.Wrestling, 80.8, 94.6);
            SetSkill(SkillName.Tactics, 90.6, 100.4);
            SetSkill(SkillName.MagicResist, 108.4, 114.0);

            Fame = 16000;
            Karma = -16000;

            VirtualArmor = 54;
        }

        public Tangle(Serial serial)
            : base(serial)
        {
        }

        public override bool GivesMLMinorArtifact => true;

        public override bool BardImmunity => !Core.AOS;
        public override Poison PoisonImmunity => Poison.Lethal;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosUltraRich, 3);
        }

        public override void OnDeath(Container c)
        {
            for (int i = 0; i < Utility.RandomMinMax(1, 3); i++)
            {
                c.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }   

            if (Utility.RandomDouble() < 0.3)
                c.DropItem(new TaintedSeeds());

            base.OnDeath(c);
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
