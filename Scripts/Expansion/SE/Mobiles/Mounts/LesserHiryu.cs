using System;
using Server.Engines.Plants;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a hiryu corpse")]
    public class LesserHiryu : BaseMount
    {
        [Constructible]
        public LesserHiryu()
            : base("a lesser hiryu", 243, 0x3E94, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Hue = GetHue();

            SetStr(301, 410);
            SetDex(171, 270);
            SetInt(301, 325);

            SetHits(401, 600);
            SetMana(60);

            SetDamage(ResistType.Phys, 100, 0, 18, 23);

            SetResist(ResistType.Phys, 45, 70);
            SetResist(ResistType.Fire, 60, 80);
            SetResist(ResistType.Cold, 5, 15);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.Anatomy, 75.1, 80.0);
            SetSkill(SkillName.MagicResist, 85.1, 100.0);
            SetSkill(SkillName.Tactics, 100.1, 110.0);
            SetSkill(SkillName.Wrestling, 100.1, 120.0);

            Fame = 10000;
            Karma = -10000;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 98.7;

            if (Utility.RandomDouble() < .33)
                PackItem(Seed.RandomBonsaiSeed());

            SetWeaponAbility(WeaponAbility.Dismount);
            SetSpecialAbility(SpecialAbility.GraspingClaw);
        }

        public LesserHiryu(Serial serial)
            : base(serial)
        {
        }

        public override bool StatLossAfterTame => true;
        public override int TreasureMapLevel => 3;
        public override int Meat => 16;
        public override int Hides => 60;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override bool CanAngerOnTame => true;

        public override bool OverrideBondingReqs()
        {
            if (ControlMaster.Skills[SkillName.Bushido].Base >= 90.0)
                return true;
            return false;
        }

        public override int GetAngerSound() => 0x4FE;

        public override int GetIdleSound() => 0x4FD;

        public override int GetAttackSound() => 0x4FC;

        public override int GetHurtSound() => 0x4FF;

        public override int GetDeathSound() => 0x4FB;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.Gems, 4);
        }

        public override double GetControlChance(Mobile m, bool useBaseSkill)
        {
            if (PetTrainingHelper.Enabled)
            {
                var profile = PetTrainingHelper.GetAbilityProfile(this);

                if (profile != null && profile.HasCustomized())
                {
                    return base.GetControlChance(m, useBaseSkill);
                }
            }

            double tamingChance = base.GetControlChance(m, useBaseSkill);

            if (tamingChance >= 0.95)
            {
                return tamingChance;
            }

            double skill = (useBaseSkill ? m.Skills.Bushido.Base : m.Skills.Bushido.Value);

            if (skill < 90.0)
            {
                return tamingChance;
            }

            double bushidoChance = (skill - 30.0) / 100;

            if (m.Skills.Bushido.Base >= 120)
                bushidoChance += 0.05;

            return bushidoChance > tamingChance ? bushidoChance : tamingChance;
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

        private static int GetHue()
        {
            /*

            500	527	No Hue Color	94.88%	0
            10	527	Green			1.90%	0x8295
            10	527	Green			1.90%	0x8163	(Very Close to Above Green)	//this one is an approximation
            5	527	Dark Green		0.95%	0x87D4
            1	527	Valorite		0.19%	0x88AB
            1	527	Midnight Blue	0.19%	0x8258

            * */
            int rand = Utility.Random(527);
            switch (rand)
            {
                case int n when n <= 0:
                    return 0x8258;
                case int n when n <= 1:
                    return 0x88AB;
                case int n when n <= 3:
                    return 0x8030;
                case int n when n <= 6:
                    return 0x87D4;
                case int n when n <= 16:
                    return 0x8163;
                case int n when n <= 26:
                    return 0x8295;
                default:
                    return 0;
            }
            /*old way
            if (rand <= 0)
                return 0x8258;
            else if (rand <= 1)
                return 0x88AB;
            else if (rand <= 6)
                return 0x87D4;
            else if (rand <= 16)
                return 0x8163;
            else if (rand <= 26)
                return 0x8295;

            return 0;*/
        }
    }
}
