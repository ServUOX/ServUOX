using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Spite corpse")]
    public class Spite : Changeling
    {
        [Constructible]
        public Spite()
        {
            Hue = DefaultHue;

            SetStr(53, 214);
            SetDex(243, 367);
            SetInt(369, 586);

            SetHits(1013, 1052);
            SetStam(243, 367);
            SetMana(369, 586);

            SetDamage(14, 20);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 85, 90);
            SetResist(ResistType.Fire, 41, 46);
            SetResist(ResistType.Cold, 40, 44);
            SetResist(ResistType.Poison, 42, 46);
            SetResist(ResistType.Energy, 45, 47);

            SetSkill(SkillName.Wrestling, 12.8, 16.7);
            SetSkill(SkillName.Tactics, 102.6, 131.0);
            SetSkill(SkillName.MagicResist, 141.2, 161.6);
            SetSkill(SkillName.Magery, 108.4, 119.2);
            SetSkill(SkillName.EvalInt, 108.4, 120.0);
            SetSkill(SkillName.Meditation, 109.2, 120.0);
            SetSkill(SkillName.Spellweaving, 120.0);

            Fame = 21000;
            Karma = -21000;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetSpecialAbility(SpecialAbility.ManaDrain);
        }

        public Spite(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;
        public override string DefaultName => "Spite";
        public override int DefaultHue => 0x21;
        public override bool GivesMLMinorArtifact => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
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
