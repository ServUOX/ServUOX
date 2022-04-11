using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Lady Marai corpse")]
    public class LadyMarai : SkeletalKnight
    {
        [Constructible]
        public LadyMarai()
        {
            Name = "Lady Marai";
            Hue = 0x21;

            SetStr(221, 304);
            SetDex(98, 138);
            SetInt(54, 99);

            SetHits(694, 846);

            SetDamage(15, 25);

            SetDamageType(ResistType.Physical, 40);
            SetDamageType(ResistType.Cold, 60);

            SetResist(ResistType.Physical, 55, 65);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 70, 80);
            SetResist(ResistType.Poison, 40, 50);
            SetResist(ResistType.Energy, 50, 60);

            SetSkill(SkillName.Wrestling, 126.6, 137.2);
            SetSkill(SkillName.Tactics, 128.7, 134.5);
            SetSkill(SkillName.MagicResist, 102.1, 119.1);
            SetSkill(SkillName.Anatomy, 126.2, 136.5);

            Fame = 18000;
            Karma = -18000;

            VirtualArmor = 29;

            SetWeaponAbility(WeaponAbility.CrushingBlow);
        }

        public LadyMarai(Serial serial)
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

            if (Utility.RandomDouble() < 0.15)
                c.DropItem(new DisintegratingThesisNotes());

            if (Utility.RandomDouble() < 0.1)
                c.DropItem(new ParrotItem());

            base.OnDeath(c);
        }

        /*public override bool GivesMLMinorArtifact
        {
            get
            {
                return true;
            }
        }
        */

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 3);
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
