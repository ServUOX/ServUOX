using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an impaler corpse")]
    public class Impaler : BaseCreature
    {
        [Constructible]
        public Impaler()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("impaler");
            Body = 306;
            BaseSoundID = 0x2A7;

            SetStr(190);
            SetDex(45);
            SetInt(190);

            SetHits(5000);

            SetDamage(ResistType.Phys, 100, 0, 31, 35);

            SetResist(ResistType.Phys, 90);
            SetResist(ResistType.Fire, 60);
            SetResist(ResistType.Cold, 75);
            SetResist(ResistType.Pois, 60);
            SetResist(ResistType.Engy, 100);

            SetSkill(SkillName.Wrestling, 80.0, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 100.0);
            SetSkill(SkillName.Poisoning, 160.0);
            SetSkill(SkillName.DetectHidden, 100.0);
            SetSkill(SkillName.Necromancy, 110.0, 120.0);
            SetSkill(SkillName.SpiritSpeak, 100.0, 110.0);

            Fame = 24000;
            Karma = -24000;

            VirtualArmor = 49;

            SetWeaponAbility(WeaponAbility.BleedAttack);
            SetWeaponAbility(WeaponAbility.MortalStrike);
            SetWeaponAbility(WeaponAbility.ArmorIgnore);

            ForceActiveSpeed = 0.38;
            ForcePassiveSpeed = 0.66;
        }

        public Impaler(Serial serial)
            : base(serial)
        {
        }

        public override bool CanFlee => false;
        public override bool IgnoreYoungProtection => Core.ML;
        public override bool AutoDispel => true;
        public override bool BardImmunity => !Core.SE;
        public override bool Unprovokable => Core.SE;
        public override bool AreaPeaceImmunity => Core.SE;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override Poison HitPoison => (0.8 >= Utility.RandomDouble() ? Poison.Greater : Poison.Deadly);
        public override int TreasureMapLevel => 1;

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
