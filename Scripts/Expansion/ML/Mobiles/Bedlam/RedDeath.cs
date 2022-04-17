using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Red Death corpse")]
    public class RedDeath : SkeletalSteed
    {
        [Constructible]
        public RedDeath()
            : base("Red Death")
        {
            Hue = 0x21;
            BaseSoundID = 0x1C3;

            AI = AIType.AI_Melee;
            FightMode = FightMode.Closest;

            SetStr(319, 324);
            SetDex(241, 244);
            SetInt(242, 255);

            SetHits(1540, 1605);

            SetDamage(ResistType.Phys, 25, 0, 25, 29);
            SetDamage(ResistType.Fire, 75);
            SetDamage(ResistType.Cold, 0);

            SetResist(ResistType.Phys, 60, 70);
            SetResist(ResistType.Fire, 90);
            SetResist(ResistType.Cold, 0);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 0);

            SetSkill(SkillName.Wrestling, 121.4, 143.7);
            SetSkill(SkillName.Tactics, 120.9, 142.2);
            SetSkill(SkillName.MagicResist, 120.1, 142.3);
            SetSkill(SkillName.Anatomy, 120.2, 144.0);

            Fame = 28000;
            Karma = -28000;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetWeaponAbility(WeaponAbility.WhirlwindAttack);
            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public RedDeath(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;
        public override bool GivesMLMinorArtifact => true;
        public override bool AlwaysMurderer => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 3);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.HighScrolls, 2);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            c.DropItem(new ResolvesBridle());
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
