using Server.Engines.Craft;
using Server.Engines.Points;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using System.Collections.Generic;

namespace Server.Engines.Fellowship
{
    public class FellowshipRewardGump : BaseRewardGump
    {
        public override int PointsName => 1159184;  // Your Fellowship Silver:
        public override int RewardLabel => 1159185;  // Would you like to buy something?

        public FellowshipRewardGump(Mobile owner, PlayerMobile user)
            : base(owner, user, Rewards, 1159183)
        {
        }

        public override int GetYOffset(int id)
        {
            if (Index > 3)
            {
                return 20;
            }

            return base.GetYOffset(id);
        }

        public override double GetPoints(Mobile m)
        {
            return PointsSystem.FellowshipData.GetPoints(m);
        }

        public override void RemovePoints(double points)
        {
            PointsSystem.FellowshipData.DeductPoints(User, points);
        }

        public override void OnItemCreated(Item item)
        {
            item.InvalidateProperties();
        }

        public override void OnConfirmed(CollectionItem citem, int index)
        {
            Item item = null;

            if (citem.Type == typeof(RecipeScroll))
            {
                switch (index)
                {
                    default:
                    case 9: item = new RecipeScroll((int)TailorRecipe.CowlOfTheMaceAndShield); break;
                    case 10: item = new RecipeScroll((int)TailorRecipe.MagesHoodOfScholarlyInsight); break;
                    case 11: item = new RecipeScroll((int)TailorRecipe.ElegantCollarOfFortune); break;
                    case 12: item = new RecipeScroll((int)TailorRecipe.CrimsonDaggerBelt); break;
                    case 13: item = new RecipeScroll((int)TailorRecipe.CrimsonSwordBelt); break;
                    case 14: item = new RecipeScroll((int)TailorRecipe.CrimsonMaceBelt); break;
                }
            }

            if (item != null)
            {
                if (User.Backpack == null || !User.Backpack.TryDropItem(User, item, false))
                {
                    User.SendLocalizedMessage(1074361); // The reward could not be given.  Make sure you have room in your pack.
                    item.Delete();
                }
                else
                {
                    User.SendLocalizedMessage(1073621); // Your reward has been placed in your backpack.
                    User.PlaySound(0x5A7);
                }
            }
            else
            {
                base.OnConfirmed(citem, index);
            }

            PointsSystem.FellowshipData.DeductPoints(User, citem.Points);
        }

        public static List<CollectionItem> Rewards { get; set; }

        public static void Initialize()
        {
            Rewards = new List<CollectionItem>
            {
                new CollectionItem(typeof(TinctureOfSilver), 0x183B, 1155619, 1900, 10000),
                new CollectionItem(typeof(PumpkinDeed), 0x14F2, 1159232, 1922, 50000),
                new CollectionItem(typeof(PumpkinRowBoatDeed), 0x14F2, 1159233, 0, 250000),
                new CollectionItem(typeof(JackOLanternHelm), 0xA3EA, 1125986, 0, 150000),
                new CollectionItem(typeof(AdmiralJacksPumpkinSpiceAle), 0xA40B, 1159230, 1922, 50000),
                new CollectionItem(typeof(ExplodingJackOLantern), 0xA407, 1159220, 1175, 40000),
                new CollectionItem(typeof(CaptainTitleDeed), 0x14EF, 1156995, 0, 80000),
                new CollectionItem(typeof(CommanderTitleDeed), 0x14EF, 1156995, 0, 40000),
                new CollectionItem(typeof(EnsignTitleDeed), 0x14EF, 1156995, 0, 20000),
                new CollectionItem(typeof(RecipeScroll), 0x2831, 1074560, 0, 150000),
                new CollectionItem(typeof(RecipeScroll), 0x2831, 1074560, 0, 150000),
                new CollectionItem(typeof(RecipeScroll), 0x2831, 1074560, 0, 200000),
                new CollectionItem(typeof(RecipeScroll), 0x2831, 1074560, 0, 75000),
                new CollectionItem(typeof(RecipeScroll), 0x2831, 1074560, 0, 75000),
                new CollectionItem(typeof(RecipeScroll), 0x2831, 1074560, 0, 75000)
            };
        }
    }
}
