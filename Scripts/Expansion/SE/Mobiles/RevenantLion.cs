using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a revenant lion corpse")]
    public class RevenantLion : BaseCreature
    {
        [Constructible]
        public RevenantLion()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a Revenant Lion";
            Body = 251;

            SetStr(276, 325);
            SetDex(156, 175);
            SetInt(76, 105);

            SetHits(251, 280);

            SetDamage(ResistType.Phys, 30, 0, 18, 24);
            SetDamage(ResistType.Cold, 30);
            SetDamage(ResistType.Pois, 10);
            SetDamage(ResistType.Engy, 30);

            SetResist(ResistType.Phys, 40, 60);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 55, 65);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.EvalInt, 80.1, 90.0);
            SetSkill(SkillName.Magery, 80.1, 90.0);
            SetSkill(SkillName.Poisoning, 120.1, 130.0);
            SetSkill(SkillName.MagicResist, 70.1, 90.0);
            SetSkill(SkillName.Tactics, 60.1, 80.0);
            SetSkill(SkillName.Wrestling, 80.1, 88.0);

            Fame = 4000;
            Karma = -4000;

            PackItem(Loot.PackNecroReg(6, 8));

            SetWeaponAbility(WeaponAbility.BleedAttack);
        }

        public RevenantLion(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 3;
        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Greater;
        public override Poison HitPoison => Poison.Greater;

        public override int GetAngerSound() => 0x518;
        public override int GetIdleSound() => 0x517;
        public override int GetAttackSound() => 0x516;
        public override int GetHurtSound() => 0x519;
        public override int GetDeathSound() => 0x515;

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(Loot.PackBodyPartOrBones());
            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
            AddLoot(LootPack.MedScrolls, 2);
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
