using System;
using System.Collections;
using Server.Engines.Plants;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a hiryu corpse")]
    public class Hiryu : BaseMount
    {
        [Constructible]
        public Hiryu()
            : base("a hiryu", 243, 0x3E94, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Hue = GetHue();

            SetStr(1201, 1410);
            SetDex(171, 270);
            SetInt(301, 325);

            SetHits(901, 1100);
            SetMana(60);

            SetDamage(ResistType.Phys, 100, 0, 20, 30);

            SetResist(ResistType.Phys, 55, 70);
            SetResist(ResistType.Fire, 70, 90);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Pois, 40, 50);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.Anatomy, 75.1, 80.0);
            SetSkill(SkillName.MagicResist, 85.1, 100.0);
            SetSkill(SkillName.Tactics, 100.1, 110.0);
            SetSkill(SkillName.Wrestling, 100.1, 120.0);

            Fame = 18000;
            Karma = -18000;

            Tamable = true;
            ControlSlots = 4;
            MinTameSkill = 98.7;

            if (Utility.RandomDouble() < .33)
                PackItem(Seed.RandomBonsaiSeed());

            if (Core.ML && Utility.RandomDouble() < .33)
                PackItem(Seed.RandomPeculiarSeed(4));

            SetWeaponAbility(WeaponAbility.Dismount);
            SetSpecialAbility(SpecialAbility.GraspingClaw);
        }

        public Hiryu(Serial serial)
            : base(serial)
        {
        }

        public override bool StatLossAfterTame => true;
        public override int TreasureMapLevel => 5;
        public override int Meat => 16;
        public override int Hides => 60;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override bool CanAngerOnTame => true;
        public override WeaponAbility GetWeaponAbility() => WeaponAbility.Dismount;

        public override int GetAngerSound() => 0x4FE;

        public override int GetIdleSound() => 0x4FD;

        public override int GetAttackSound() => 0x4FC;

        public override int GetHurtSound() => 0x4FF;

        public override int GetDeathSound() => 0x4FB;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.Gems, 4);
        }

        public override void OnAfterTame(Mobile tamer)
        {
            if (Owners.Count == 0 && PetTrainingHelper.Enabled)
            {
                RawStr = (int)Math.Max(1, RawStr * 0.5);
                RawDex = (int)Math.Max(1, RawDex * 0.5);

                HitsMaxSeed = RawStr;
                Hits = RawStr;

                StamMaxSeed = RawDex;
                Stam = RawDex;
            }
            else
            {
                base.OnAfterTame(tamer);
            }
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
            1000	1075	No Hue Color	93.02%	0x0
            * 
            10	1075	Ice Green    	0.93%	0x847F
            10	1075	Light Blue    	0.93%	0x848D
            10	1075	Strong Cyan		0.93%	0x8495
            10	1075	Agapite			0.93%	0x8899
            10	1075	Gold			0.93%	0x8032
            * 
            8	1075	Blue and Yellow	0.74%	0x8487
            * 
            5	1075	Ice Blue       	0.47%	0x8482
            * 
            3	1075	Cyan			0.28%	0x8123
            3	1075	Light Green		0.28%	0x8295
            * 
            2	1075	Strong Yellow	0.19%	0x8037
            2	1075	Green			0.19%	0x8030	//this one is an approximation
            * 
            1	1075	Strong Purple	0.09%	0x8490
            1	1075	Strong Green	0.09%	0x855C
            * */
            int rand = Utility.Random(1075);
            switch (rand)
            {
                case int n when n <= 0:
                    return 0x855C;
                case int n when n <= 1:
                    return 0x8490;
                case int n when n <= 3:
                    return 0x8030;
                case int n when n <= 5:
                    return 0x8037;
                case int n when n <= 8:
                    return 0x8295;
                case int n when n <= 11:
                    return 0x8123;
                case int n when n <= 16:
                    return 0x8482;
                case int n when n <= 24:
                    return 0x8487;
                case int n when n <= 34:
                    return 0x8032;
                case int n when n <= 44:
                    return 0x8899;
                case int n when n <= 54:
                    return 0x8495;
                case int n when n <= 64:
                    return 0x848D;
                case int n when n <= 74:
                    return 0x847F;
                default:
                    return 0;
            }
            /* old way
            if (rand <= 0)
                return 0x855C;
            else if (rand <= 1)
                return 0x8490;
            else if (rand <= 3)
                return 0x8030;
            else if (rand <= 5)
                return 0x8037;
            else if (rand <= 8)
                return 0x8295;
            else if (rand <= 11)
                return 0x8123;
            else if (rand <= 16)
                return 0x8482;
            else if (rand <= 24)
                return 0x8487;
            else if (rand <= 34)
                return 0x8032;
            else if (rand <= 44)
                return 0x8899;
            else if (rand <= 54)
                return 0x8495;
            else if (rand <= 64)
                return 0x848D;
            else if (rand <= 74)
                return 0x847F;

            return 0;*/
        }
    }
}
