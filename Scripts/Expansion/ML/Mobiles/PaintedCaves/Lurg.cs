using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Lurg corpse")]
    public class Lurg : Troglodyte
    {
        [Constructible]
        public Lurg()
        {
            Name = "Lurg";
            Hue = 0x455;

            SetStr(584, 625);
            SetDex(163, 176);
            SetInt(90, 106);

            SetHits(3034, 3189);
            SetStam(163, 176);
            SetMana(90, 106);

            SetDamage(16, 19);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 50, 53);
            SetResistance(ResistanceType.Fire, 45, 47);
            SetResistance(ResistanceType.Cold, 56, 60);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 41, 56);

            SetSkill(SkillName.Wrestling, 122.7, 130.5);
            SetSkill(SkillName.Tactics, 109.3, 118.5);
            SetSkill(SkillName.MagicResist, 72.9, 87.6);
            SetSkill(SkillName.Anatomy, 110.5, 124.0);
            SetSkill(SkillName.Healing, 84.1, 105.0);

            Fame = 10000;
            Karma = -10000;

            SetWeaponAbility(WeaponAbility.CrushingBlow);
        }

        public Lurg(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;
        public override bool GivesMLMinorArtifact => true;
        public override int TreasureMapLevel => 4;
        public override bool AllureImmunity => true;

        public override void OnDeath(Container CorpseLoot)
        {
            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                CorpseLoot.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            base.OnDeath(CorpseLoot);
        }

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
