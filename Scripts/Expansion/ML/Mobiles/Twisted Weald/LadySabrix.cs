using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Lady Sabrix corpse")]
    public class LadySabrix : GiantBlackWidow
    {
        [Constructible]
        public LadySabrix()
        {
            Name = "Lady Sabrix";
            Hue = 0x497;

            SetStr(82, 130);
            SetDex(117, 146);
            SetInt(50, 98);

            SetHits(233, 361);
            SetStam(117, 146);
            SetMana(50, 98);

            SetDamage(ResistType.Phys, 100, 0, 15, 22);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 30, 39);
            SetResist(ResistType.Pois, 70, 80);
            SetResist(ResistType.Engy, 35, 44);

            SetSkill(SkillName.Wrestling, 109.8, 122.8);
            SetSkill(SkillName.Tactics, 102.8, 120.0);
            SetSkill(SkillName.MagicResist, 79.4, 95.1);
            SetSkill(SkillName.Anatomy, 68.8, 105.1);
            SetSkill(SkillName.Poisoning, 97.8, 116.7);

            Fame = 18900;
            Karma = -18900;

            SetWeaponAbility(WeaponAbility.ArmorIgnore);
        }

        public LadySabrix(Serial serial)
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

            if (Utility.RandomDouble() < 0.2)
                c.DropItem(new SabrixsEye());

            if (Utility.RandomDouble() < 0.25)
            {
                switch (Utility.Random(2))
                {
                    case 0: AddToBackpack(new PaladinArms()); break;
                    case 1: AddToBackpack(new HunterLegs()); break;
                    default:
                        AddToBackpack(new HunterLegs()); break;
                }
            }

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
