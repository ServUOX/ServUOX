using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a clan scratch savage wolf corpse")]
    public class ClanSSW : BaseCreature
    {
        [Constructible]
        public ClanSSW()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Clan Scratch Savage Wolf";
            Body = 98;
            Hue = 0x2C;
            BaseSoundID = 229;

            SetStr(170);
            SetDex(244);
            SetInt(57);

            SetHits(65);

            SetDamage(8, 10);

            SetDamageType(ResistType.Physical, 20);
            SetDamageType(ResistType.Cold, 80);

            SetResist(ResistType.Physical, 30, 35);
            SetResist(ResistType.Cold, 40, 45);
            SetResist(ResistType.Poison, 25, 30);
            SetResist(ResistType.Energy, 20, 25);

            SetSkill(SkillName.Swords, 99.0, 100.0);
            SetSkill(SkillName.MagicResist, 41.5, 42.5);
            SetSkill(SkillName.Tactics, 65.1, 70.0);
            SetSkill(SkillName.Wrestling, 42.3, 45.5);

            Fame = 3400;
            Karma = -3400;

            VirtualArmor = 50;

            SetWeaponAbility(WeaponAbility.ParalyzingBlow);
        }

        public ClanSSW(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Canine;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
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
