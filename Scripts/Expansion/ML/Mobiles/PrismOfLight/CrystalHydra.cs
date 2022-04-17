using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a crystal hydra corpse")]
    public class CrystalHydra : Hydra
    {
        [Constructible]
        public CrystalHydra()
            : base()
        {
            Name = "a crystal hydra";
            Body = 0x109;
            Hue = 0x47E;
            BaseSoundID = 0x16A;

            SetStr(800, 830);
            SetDex(100, 120);
            SetInt(100, 120);

            SetHits(1450, 1500);

            SetDamage(ResistType.Phys, 5, 0, 21, 26);
            SetDamage(ResistType.Fire, 5);
            SetDamage(ResistType.Cold, 80);
            SetDamage(ResistType.Pois, 5);
            SetDamage(ResistType.Engy, 5);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 80, 100);
            SetResist(ResistType.Pois, 35, 45);
            SetResist(ResistType.Engy, 80, 100);

            SetSkill(SkillName.Wrestling, 100.0, 120.0);
            SetSkill(SkillName.Tactics, 100.0, 110.0);
            SetSkill(SkillName.MagicResist, 80.0, 100.0);
            SetSkill(SkillName.Anatomy, 70.0, 80.0);

            Fame = 17000;
            Karma = -17000;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public CrystalHydra(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.HighScrolls);
            AddLoot(LootPack.Parrot);
        }

        public override void OnDeath(Container c)
        {
            if (Utility.RandomDouble() < 0.25)
                c.DropItem(new ShatteredCrystals());

            c.DropItem(new CrystallineFragments());

            base.OnDeath(c);
        }

        public override int Hides => 40;
        public override int Meat => 19;
        public override int TreasureMapLevel => 5;

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
