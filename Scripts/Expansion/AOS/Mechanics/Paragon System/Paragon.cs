using System;
using Server.Items;

namespace Server.Mobiles
{
    public class Paragon
    {
        public static double ChestChance = .10;// Chance that a paragon will carry a paragon chest
        public static double ChocolateIngredientChance = .20;// Chance that a paragon will drop a chocolatiering ingredient
        public static Map[] Maps = new Map[]                   // Maps that paragons will spawn on
        {
            Map.Ilshenar
        };
        //should only be 19 artifacts
        // luna lance 0
        // violet courage 1
        // cavorting club 2
        // night’s kiss 3
        // iolo’s lute 4
        // gwenno’s harp 5
        // arctic death dealer 6
        // enchanted titan leg bone 7
        // nox ranger’s heavy crossbow 8
        // blaze of death 9
        // burglar’s bandana 10
        // gold bricks 11
        // alchemist’s bauble 12
        // phillip’s wooden steed 13
        // polar bear mask 14
        // bow of the juka king 15
        // gloves of the pugilist 16
        // orcish visage 17
        // staff of power 18
        // shield of invulnerability 19
        public static Type[] Artifacts = new Type[]
        {
            typeof(LunaLance),
            typeof(VioletCourage),
            typeof(CavortingClub),
            typeof(NightsKiss),
            typeof(IolosLute),
            typeof(GwennosHarp),
            typeof(ArcticDeathDealer),
            typeof(EnchantedTitanLegBone),
            typeof(NoxRangersHeavyCrossbow),
            typeof(BlazeOfDeath),
            typeof(BurglarsBandana),
            typeof(GoldBricks),
            typeof(AlchemistsBauble),
            typeof(PhillipsWoodenSteed),
            typeof(PolarBearMask),
            typeof(BowOfTheJukaKing),
            typeof(GlovesOfThePugilist),
            typeof(OrcishVisage),
            typeof(StaffOfPower),
            typeof(ShieldOfInvulnerability),

            typeof(HeartOfTheLion),  // Added with SE Publish 28
            //typeof(WrathOfTheDryad),
            typeof(PixieSwatter)  // Aded With SE Publish 28
            
        };
        public static int Hue = 0x501;// Paragon hue

        // Buffs
        public static double HitsBuff = 5.0;
        public static double StrBuff = 1.05;
        public static double IntBuff = 1.20;
        public static double DexBuff = 1.20;
        public static double SkillsBuff = 1.20;
        public static double SpeedBuff = 1.25;// debuffed with SE from 150 to 125
        public static double FameBuff = 1.40;
        public static double KarmaBuff = 1.40;
        public static int DamageBuff = 5;
        public static void Convert(BaseCreature bc)
        {
            if (bc.IsParagon ||
                !bc.CanBeParagon)
                return;

            bc.Hue = Hue;

            if (bc.HitsMaxSeed >= 0)
                bc.HitsMaxSeed = (int)(bc.HitsMaxSeed * HitsBuff);

            bc.RawStr = (int)(bc.RawStr * StrBuff);
            bc.RawInt = (int)(bc.RawInt * IntBuff);
            bc.RawDex = (int)(bc.RawDex * DexBuff);

            bc.Hits = bc.HitsMax;
            bc.Mana = bc.ManaMax;
            bc.Stam = bc.StamMax;

            for (int i = 0; i < bc.Skills.Length; i++)
            {
                Skill skill = bc.Skills[i];

                if (skill.Base > 0.0)
                    skill.Base *= SkillsBuff;
            }

            bc.PassiveSpeed /= SpeedBuff;
            bc.ActiveSpeed /= SpeedBuff;
            bc.CurrentSpeed = bc.PassiveSpeed;

            bc.DamageMin += DamageBuff;
            bc.DamageMax += DamageBuff;

            if (bc.Fame > 0)
                bc.Fame = (int)(bc.Fame * FameBuff);

            if (bc.Fame > 32000)
                bc.Fame = 32000;

            // TODO: Mana regeneration rate = Sqrt( buffedFame ) / 4

            if (bc.Karma != 0)
            {
                bc.Karma = (int)(bc.Karma * KarmaBuff);

                if (Math.Abs(bc.Karma) > 32000)
                    bc.Karma = 32000 * Math.Sign(bc.Karma);
            }
        }

        public static void UnConvert(BaseCreature bc)
        {
            if (!bc.IsParagon)
                return;

            bc.Hue = 0;

            if (bc.HitsMaxSeed >= 0)
                bc.HitsMaxSeed = (int)(bc.HitsMaxSeed / HitsBuff);

            bc.RawStr = (int)(bc.RawStr / StrBuff);
            bc.RawInt = (int)(bc.RawInt / IntBuff);
            bc.RawDex = (int)(bc.RawDex / DexBuff);

            bc.Hits = bc.HitsMax;
            bc.Mana = bc.ManaMax;
            bc.Stam = bc.StamMax;

            for (int i = 0; i < bc.Skills.Length; i++)
            {
                Skill skill = bc.Skills[i];

                if (skill.Base > 0.0)
                    skill.Base /= SkillsBuff;
            }

            bc.PassiveSpeed *= SpeedBuff;
            bc.ActiveSpeed *= SpeedBuff;
            bc.CurrentSpeed = bc.PassiveSpeed;

            bc.DamageMin -= DamageBuff;
            bc.DamageMax -= DamageBuff;

            if (bc.Fame > 0)
                bc.Fame = (int)(bc.Fame / FameBuff);
            if (bc.Karma != 0)
                bc.Karma = (int)(bc.Karma / KarmaBuff);
        }

        public static bool CheckConvert(BaseCreature bc)
        {
            return CheckConvert(bc, bc.Location, bc.Map);
        }

        public static bool CheckConvert(BaseCreature bc, Point3D location, Map m)
        {
            if (!Core.AOS)
                return false;

            if (Array.IndexOf(Maps, m) == -1)
                return false;

            if (bc is BaseChampion || bc is Harrower || bc is BaseVendor || bc is BaseEscortable || bc is Clone || bc.IsParagon)
                return false;

            int fame = bc.Fame;

            if (fame > 32000)
                fame = 32000;

            double chance = 1 / Math.Round(20.0 - (fame / 3200));

            return (chance > Utility.RandomDouble());
        }

        public static bool CheckArtifactChance(Mobile m, BaseCreature bc)
        {
            if (!Core.AOS)
                return false;

            double fame = bc.Fame;

            if (fame > 32000)
                fame = 32000;

            int luck = m is PlayerMobile ? ((PlayerMobile)m).RealLuck : m.Luck;

            double chance = 1 / (Math.Max(10, 100 * (0.83 - Math.Round(Math.Log(Math.Round(fame / 6000, 3) + 0.001, 10), 3))) * (100 - Math.Sqrt(luck)) / 100.0);

            return chance > Utility.RandomDouble();
        }

        public static void GiveArtifactTo(Mobile m)
        {
            Item item = (Item)Activator.CreateInstance(Artifacts[Utility.Random(Artifacts.Length)]);

            if (m.AddToBackpack(item))
                m.SendMessage("As a reward for slaying the mighty paragon, an artifact has been placed in your backpack.");
            else
                m.SendMessage("As your backpack is full, your reward for destroying the legendary paragon has been placed at your feet.");
        }
    }
}
