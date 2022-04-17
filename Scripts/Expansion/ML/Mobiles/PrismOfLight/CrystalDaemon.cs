using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a crystal daemon corpse")]
    public class CrystalDaemon : ArcaneDaemon
    {
        [Constructible]
        public CrystalDaemon()
            : base()
        {
            Name = "a crystal daemon";
            Body = 0x310;
            Hue = 0x3E8;
            BaseSoundID = 0x47D;

            SetStr(140, 200);
            SetDex(120, 150);
            SetInt(800, 850);

            SetHits(200, 220);

            SetDamage(ResistType.Phys, 0, 0, 16, 20);
            SetDamage(ResistType.Cold, 40);
            SetDamage(ResistType.Engy, 60);

            SetResist(ResistType.Phys, 20, 40);
            SetResist(ResistType.Fire, 0, 20);
            SetResist(ResistType.Cold, 60, 80);
            SetResist(ResistType.Pois, 20, 40);
            SetResist(ResistType.Engy, 65, 75);

            SetSkill(SkillName.Wrestling, 60.0, 80.0);
            SetSkill(SkillName.Tactics, 70.0, 80.0);
            SetSkill(SkillName.MagicResist, 100.0, 110.0);
            SetSkill(SkillName.Magery, 120.0, 130.0);
            SetSkill(SkillName.EvalInt, 100.0, 110.0);
            SetSkill(SkillName.Meditation, 100.0, 110.0);

            Fame = 15000;
            Karma = -15000;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }
        }

        public override void OnDeath(Container c)
        {
            if (Utility.RandomDouble() < 0.4)
                c.DropItem(new ScatteredCrystals());

            base.OnDeath(c);
        }

        public CrystalDaemon(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.HighScrolls);
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
