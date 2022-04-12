using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an Irk corpse")]
    public class Irk : Changeling
    {
        [Constructible]
        public Irk()
        {
            Hue = DefaultHue;

            SetStr(23, 183);
            SetDex(259, 360);
            SetInt(374, 600);

            SetHits(1006, 1064);
            SetStam(259, 360);
            SetMana(374, 600);

            SetDamage(14, 20);

            SetResist(ResistType.Phys, 80, 90);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 41, 50);
            SetResist(ResistType.Engy, 40, 49);

            SetSkill(SkillName.Wrestling, 120.3, 123.0);
            SetSkill(SkillName.Tactics, 120.1, 131.8);
            SetSkill(SkillName.MagicResist, 132.3, 165.8);
            SetSkill(SkillName.Magery, 108.9, 119.7);
            SetSkill(SkillName.EvalInt, 108.4, 120.0);
            SetSkill(SkillName.Meditation, 108.9, 119.1);

            Fame = 21000;
            Karma = -21000;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetSpecialAbility(SpecialAbility.AngryFire);
        }

        public override bool CanBeParagon => false;

        public override void OnDeath(Container CorpseLoot)
        {
            base.OnDeath(CorpseLoot);

            if (Utility.RandomDouble() < 0.25)
                CorpseLoot.DropItem(new IrksBrain());

            if (Utility.RandomDouble() < 0.025)
                CorpseLoot.DropItem(new PaladinGloves());
        }

        public Irk(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "Irk";
        public override int DefaultHue => 0x489;

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
