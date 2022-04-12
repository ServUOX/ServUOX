using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an abyssal abomination corpse")]
    public class AbyssalAbomination : BaseCreature
    {
        [Constructible]
        public AbyssalAbomination()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an Abyssal Abomination";
            Body = 312;
            Hue = 769;
            BaseSoundID = 0x451;

            SetStr(401, 420);
            SetDex(81, 90);
            SetInt(401, 420);

            SetHits(600, 750);

            SetDamage(13, 17);

            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Pois, 50);

            SetResist(ResistType.Phys, 30, 35);
            SetResist(ResistType.Fire, 100);
            SetResist(ResistType.Cold, 50, 55);
            SetResist(ResistType.Pois, 60, 65);
            SetResist(ResistType.Engy, 77, 80);

            SetSkill(SkillName.EvalInt, 200.0);
            SetSkill(SkillName.Magery, 112.6, 117.5);
            SetSkill(SkillName.SpiritSpeak, 200.0);
            SetSkill(SkillName.Necromancy, 112.6, 117.5);
            SetSkill(SkillName.Meditation, 200.0);
            SetSkill(SkillName.MagicResist, 117.6, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 84.1, 88.0);

            Fame = 26000;
            Karma = -26000;

            VirtualArmor = 54;

            SetWeaponAbility(WeaponAbility.MortalStrike);
            SetWeaponAbility(WeaponAbility.WhirlwindAttack);
        }

        public AbyssalAbomination(Serial serial)
            : base(serial)
        {
        }

        public override bool IgnoreYoungProtection => Core.ML;
        public override bool BardImmunity => !Core.SE;
        public override bool Unprovokable => Core.SE;
        public override bool AreaPeaceImmunity => Core.SE;
        public override Poison PoisonImmunity => Poison.Lethal;

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
            int version = reader.ReadInt();
        }
    }
}
