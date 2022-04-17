using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Lady Lissith corpse")]
    public class LadyLissith : GiantBlackWidow
    {
        [Constructible]
        public LadyLissith()
        {
            Name = "Lady Lissith";
            Hue = 0x452;

            SetStr(81, 130);
            SetDex(116, 152);
            SetInt(44, 100);

            SetHits(245, 375);
            SetStam(116, 157);
            SetMana(44, 100);

            SetDamage(ResistType.Phys, 100, 0, 15, 22);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 31, 39);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 71, 80);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.Wrestling, 108.6, 123.0);
            SetSkill(SkillName.Tactics, 102.7, 119.0);
            SetSkill(SkillName.MagicResist, 78.8, 95.6);
            SetSkill(SkillName.Anatomy, 68.6, 106.8);
            SetSkill(SkillName.Poisoning, 96.6, 112.9);

            Fame = 18900;
            Karma = -18900;

            SetWeaponAbility(WeaponAbility.BleedAttack);
        }

        public LadyLissith(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;

        public override void OnDeath(Container c)
        {
            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                c.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            if (Utility.RandomDouble() < 0.025)
                c.DropItem(new GreymistChest());

            if (Utility.RandomDouble() < 0.45)
                c.DropItem(new LissithsSilk());

            if (Utility.RandomDouble() < 0.1)
                c.DropItem(new ParrotItem());

            base.OnDeath(c);
        }

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
