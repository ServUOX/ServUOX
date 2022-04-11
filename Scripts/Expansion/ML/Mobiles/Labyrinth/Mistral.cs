using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a mistral corpse")]
    public class Mistral : BaseCreature
    {
        [Constructible]
        public Mistral()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Mistral";
            Body = 13;
            Hue = 924;
            BaseSoundID = 263;

            SetStr(134, 201);
            SetDex(226, 238);
            SetInt(126, 134);

            SetHits(386, 609);

            SetDamage(17, 20);
            SetDamageType(ResistType.Energy, 20);
            SetDamageType(ResistType.Cold, 40);
            SetDamageType(ResistType.Physical, 40);

            SetResist(ResistType.Physical, 55, 64);
            SetResist(ResistType.Fire, 36, 40);
            SetResist(ResistType.Cold, 33, 39);
            SetResist(ResistType.Poison, 30, 39);
            SetResist(ResistType.Energy, 49, 53);

            SetSkill(SkillName.EvalInt, 96.2, 97.8);
            SetSkill(SkillName.Magery, 100.8, 112.9);
            SetSkill(SkillName.MagicResist, 106.2, 111.2);
            SetSkill(SkillName.Tactics, 110.2, 117.1);
            SetSkill(SkillName.Wrestling, 100.3, 104.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 40;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }
        }

        public override bool GivesMLMinorArtifact => true;

        public Mistral(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;
        public override double DispelFocus => 45.0;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 2;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
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
