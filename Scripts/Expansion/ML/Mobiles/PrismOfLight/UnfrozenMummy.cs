using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an unfrozen mummy corpse")]
    public class UnfrozenMummy : BaseCreature
    {
        [Constructible]
        public UnfrozenMummy()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.4, 0.8)
        {
            Name = "an unfrozen mummy";
            Body = 0x9B;
            Hue = 0x480;
            BaseSoundID = 0x1D7;

            SetStr(450, 500);
            SetDex(200, 250);
            SetInt(800, 850);

            SetHits(1500);

            SetDamage(16, 20);

            SetDamageType(ResistanceType.Physical, 0);
            SetDamageType(ResistanceType.Energy, 50);
            SetDamageType(ResistanceType.Cold, 50);

            SetResist(ResistanceType.Physical, 35, 40);
            SetResist(ResistanceType.Fire, 20, 30);
            SetResist(ResistanceType.Cold, 60, 80);
            SetResist(ResistanceType.Poison, 20, 30);
            SetResist(ResistanceType.Energy, 70, 80);

            SetSkill(SkillName.Wrestling, 90.0, 100.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 250.0);
            SetSkill(SkillName.Magery, 50.0, 60.0);
            SetSkill(SkillName.EvalInt, 50.0, 60.0);
            SetSkill(SkillName.Meditation, 80.0);

            Fame = 25000;
            Karma = -25000;

            VirtualArmor = 38;
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Core.ML)
            {
                if (Utility.RandomDouble() < 0.6)
                    CorpseLoot.DropItem(new BrokenCrystals());

                if (Utility.RandomDouble() < 0.1)
                    CorpseLoot.DropItem(new ParrotItem());

                for (int i = 0; i < Utility.RandomMinMax(0, 2); i++)
                {
                    CorpseLoot.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
                }
            }

            base.OnDeath(CorpseLoot);
        }

        public UnfrozenMummy(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.Parrot);
            AddLoot(LootPack.HighScrolls, 2);
            AddLoot(LootPack.MedScrolls);
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
