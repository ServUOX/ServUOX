using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a rune beetle corpse")]
    public class RuneBeetle : BaseCreature
    {
        private static readonly Hashtable m_Table = new Hashtable();
        [Constructible]
        public RuneBeetle()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a rune beetle";
            Body = 244;

            SetStr(401, 460);
            SetDex(121, 170);
            SetInt(376, 450);

            SetHits(301, 360);

            SetDamage(15, 22);

            SetDamageType(ResistType.Phys, 20);
            SetDamageType(ResistType.Pois, 10);
            SetDamageType(ResistType.Engy, 70);

            SetResist(ResistType.Phys, 40, 65);
            SetResist(ResistType.Fire, 35, 50);
            SetResist(ResistType.Cold, 35, 50);
            SetResist(ResistType.Pois, 75, 95);
            SetResist(ResistType.Engy, 40, 60);

            SetSkill(SkillName.EvalInt, 100.1, 125.0);
            SetSkill(SkillName.Magery, 100.1, 110.0);
            SetSkill(SkillName.Poisoning, 120.1, 140.0);
            SetSkill(SkillName.MagicResist, 95.1, 110.0);
            SetSkill(SkillName.Tactics, 78.1, 93.0);
            SetSkill(SkillName.Wrestling, 70.1, 77.5);

            Fame = 15000;
            Karma = -15000;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 93.9;

            SetSpecialAbility(SpecialAbility.RuneCorruption);
            SetWeaponAbility(WeaponAbility.BleedAttack);
        }

        public RuneBeetle(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Greater;
        public override Poison HitPoison => Poison.Greater;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
        public override bool CanAngerOnTame => true;

        public override int GetAngerSound() { return 0x4E8; }
        public override int GetIdleSound() { return 0x4E7; }
        public override int GetAttackSound() { return 0x4E6; }
        public override int GetHurtSound() { return 0x4E9; }
        public override int GetDeathSound() { return 0x4E5; }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(Loot.PackBodyPartOrBones());

            if (Core.ML && Utility.RandomDouble() < .25)
                CorpseLoot.DropItem(Engines.Plants.Seed.RandomBonsaiSeed());

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.MedScrolls, 1);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(3);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (version < 1)
            {
                for (int i = 0; i < Skills.Length; ++i)
                {
                    Skills[i].Cap = Math.Max(100.0, Skills[i].Cap * 0.9);

                    if (Skills[i].Base > Skills[i].Cap)
                    {
                        Skills[i].Base = Skills[i].Cap;
                    }
                }
            }

            if (version < 3)
            {
                if (AbilityProfile == null || AbilityProfile.MagicalAbility == MagicalAbility.None)
                {
                    SetMagicalAbility(MagicalAbility.Poisoning);
                }

                if (version == 1)
                {
                    SetSpecialAbility(SpecialAbility.RuneCorruption);
                    SetWeaponAbility(WeaponAbility.BleedAttack);
                }
            }
        }
    }
}
