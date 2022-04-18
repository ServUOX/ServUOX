using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Silk corpse")]
    public class Silk : GiantBlackWidow
    {
        [Constructible]
        public Silk()
        {
            Name = "Silk";
            Hue = 0x47E;

            SetStr(80, 131);
            SetDex(126, 156);
            SetInt(63, 102);

            SetHits(279, 378);
            SetStam(126, 156);
            SetMana(63, 102);

            SetDamage(ResistType.Phys, 100, 0, 15, 22);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 30, 39);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 70, 76);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.Wrestling, 114.1, 123.7);
            SetSkill(SkillName.Tactics, 102.6, 118.3);
            SetSkill(SkillName.MagicResist, 78.6, 94.8);
            SetSkill(SkillName.Anatomy, 81.3, 105.7);
            SetSkill(SkillName.Poisoning, 106.0, 119.2);

            Fame = 18900;
            Karma = -18900;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetWeaponAbility(WeaponAbility.ParalyzingBlow);
        }

        public Silk(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;
        public override bool GivesMLMinorArtifact => true;
        public override void GenerateLoot() => AddLoot(LootPack.UltraRich, 2);

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