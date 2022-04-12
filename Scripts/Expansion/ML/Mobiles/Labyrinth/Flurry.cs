using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("The remains of Flurry")]
    public class Flurry : BaseCreature
    {
        [Constructible]
        public Flurry()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Flurry";
            Body = 13;
            Hue = 3;
            BaseSoundID = 655;

            SetStr(149, 195);
            SetDex(218, 264);
            SetInt(130, 199);

            SetHits(474, 477);

            SetDamage(10, 15);  // Erica's

            SetDamageType(ResistType.Engy, 20);
            SetDamageType(ResistType.Cold, 80);

            SetResist(ResistType.Phys, 56, 57);
            SetResist(ResistType.Fire, 38, 44);
            SetResist(ResistType.Cold, 40, 45);
            SetResist(ResistType.Pois, 31, 37);
            SetResist(ResistType.Engy, 39, 41);

            SetSkill(SkillName.EvalInt, 99.1, 100.2);
            SetSkill(SkillName.Magery, 105.1, 108.8);
            SetSkill(SkillName.MagicResist, 104.0, 112.8);
            SetSkill(SkillName.Tactics, 113.1, 119.8);
            SetSkill(SkillName.Wrestling, 105.6, 106.4);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 54;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }
        }

        public override bool GivesMLMinorArtifact => true;

        public Flurry(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;
        public override double DispelFocus => 45.0;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 2;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 10);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.LowScrolls);
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
