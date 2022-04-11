using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("the remains of Grim")]
    public class Grim : Drake
    {
        [Constructible]
        public Grim()
            : base()
        {
            Name = "Grim";
            Hue = 1744;

            SetStr(527, 580);
            SetDex(284, 322);
            SetInt(249, 386);

            SetHits(1762, 2502);

            SetDamage(17, 25);

            SetDamageType(ResistType.Physical, 80);
            SetDamageType(ResistType.Fire, 20);

            SetResist(ResistType.Physical, 55, 60);
            SetResist(ResistType.Fire, 62, 68);
            SetResist(ResistType.Cold, 52, 57);
            SetResist(ResistType.Poison, 30, 40);
            SetResist(ResistType.Energy, 40, 44);

            SetSkill(SkillName.MagicResist, 105.8, 115.6);
            SetSkill(SkillName.Tactics, 102.8, 120.8);
            SetSkill(SkillName.Wrestling, 111.7, 119.2);
            SetSkill(SkillName.Anatomy, 105.0, 128.4);

            Fame = 17500;
            Karma = -5500;

            VirtualArmor = 54;

            Tamable = false;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetWeaponAbility(WeaponAbility.CrushingBlow);
            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public override bool GivesMLMinorArtifact => true;

        public Grim(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement => true;
        public override int Meat => 10;
        public override int Hides => 20;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.HighScrolls, 2);
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
