using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a reptalon corpse")]
    public class Reptalon : BaseMount
    {
        [Constructible]
        public Reptalon()
            : base("a reptalon", 0x114, 0x3E90, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.35)
        {
            BaseSoundID = 0x16A;

            SetStr(1001, 1025);
            SetDex(152, 164);
            SetInt(251, 289);

            SetHits(833, 931);

            SetDamage(ResistType.Phys, 0, 0, 21, 28);
            SetDamage(ResistType.Pois, 25);
            SetDamage(ResistType.Engy, 75);

            SetResist(ResistType.Phys, 53, 64);
            SetResist(ResistType.Fire, 35, 45);
            SetResist(ResistType.Cold, 36, 45);
            SetResist(ResistType.Pois, 52, 63);
            SetResist(ResistType.Engy, 71, 83);

            SetSkill(SkillName.Wrestling, 101.5, 118.2);
            SetSkill(SkillName.Tactics, 101.7, 108.2);
            SetSkill(SkillName.MagicResist, 76.4, 89.9);
            SetSkill(SkillName.Anatomy, 56.4, 59.7);

            Tamable = true;
            ControlSlots = 4;
            MinTameSkill = 101.1;

            SetWeaponAbility(WeaponAbility.ParalyzingBlow);
            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Reptalon(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 5;
        public override int Meat => 5;
        public override int Hides => 10;
        public override bool CanAngerOnTame => true;
        public override bool StatLossAfterTame => true;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override bool CanFly => true;

        public override void GenerateLoot() => AddLoot(LootPack.AosUltraRich, 3);

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
