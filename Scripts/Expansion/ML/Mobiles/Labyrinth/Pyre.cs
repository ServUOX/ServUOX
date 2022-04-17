using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Pyre corpse")]
    public class Pyre : Phoenix
    {
        [Constructible]
        public Pyre()
        {
            Name = "Pyre";
            Hue = 0x489;

            FightMode = FightMode.Closest;

            SetStr(605, 611);
            SetDex(391, 519);
            SetInt(669, 818);

            SetHits(1783, 1939);

            SetDamage(ResistType.Phys, 50, 0, 30);
            SetDamage(ResistType.Fire, 50);

            SetResist(ResistType.Phys, 65);
            SetResist(ResistType.Fire, 72, 75);
            SetResist(ResistType.Pois, 36, 41);
            SetResist(ResistType.Engy, 50, 51);

            SetSkill(SkillName.Wrestling, 121.9, 130.6);
            SetSkill(SkillName.Tactics, 114.4, 117.4);
            SetSkill(SkillName.MagicResist, 147.7, 153.0);
            SetSkill(SkillName.Poisoning, 122.8, 124.0);
            SetSkill(SkillName.Magery, 121.8, 127.8);
            SetSkill(SkillName.EvalInt, 103.6, 117.0);
            SetSkill(SkillName.Meditation, 100.0, 110.0);

            Fame = 21000;
            Karma = -21000;

            Tamable = false;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetWeaponAbility(WeaponAbility.BleedAttack);
            SetWeaponAbility(WeaponAbility.ParalyzingBlow);
        }

        public override bool GivesMLMinorArtifact => true;

        public Pyre(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Paragon.ChestChance > Utility.RandomDouble())
                c.DropItem(new ParagonChest(Name, 5));

        }

        public override void GenerateLoot() => AddLoot(LootPack.UltraRich, 3);

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
