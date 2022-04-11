using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a hydra corpse")]
    public class Hydra : BaseCreature
    {
        [Constructible]
        public Hydra()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a hydra";
            Body = 0x109;
            BaseSoundID = 0x16A;

            SetStr(801, 828);
            SetDex(105, 118);
            SetInt(102, 120);

            SetHits(1484, 1500);

            SetDamage(21, 26);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Fire, 10);
            SetDamageType(ResistanceType.Cold, 10);
            SetDamageType(ResistanceType.Poison, 10);
            SetDamageType(ResistanceType.Energy, 10);

            SetResist(ResistanceType.Physical, 65, 75);
            SetResist(ResistanceType.Fire, 70, 81);
            SetResist(ResistanceType.Cold, 25, 35);
            SetResist(ResistanceType.Poison, 35, 43);
            SetResist(ResistanceType.Energy, 36, 45);

            SetSkill(SkillName.Wrestling, 103.5, 117.4);
            SetSkill(SkillName.Tactics, 100.1, 109.8);
            SetSkill(SkillName.MagicResist, 85.5, 96.4);
            SetSkill(SkillName.Anatomy, 75.4, 79.8);

            Fame = 22000;
            Karma = -22000;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Hydra(Serial serial)
            : base(serial)
        {
        }

        public override int Hides => 40;
        public override int Meat => 19;
        public override int TreasureMapLevel => 5;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosUltraRich, 3);
        }

        public override void OnDeath(Container c)
        {
            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                c.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            c.DropItem(new HydraScale());

            if (Utility.RandomDouble() < 0.2)
                c.DropItem(new ParrotItem());

            if (Utility.RandomDouble() < 0.05)
                c.DropItem(new ThorvaldsMedallion());

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
