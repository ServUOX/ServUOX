using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class TheExchangeQuest : BaseQuest
    {
        public TheExchangeQuest()
            : base()

        {
            AddObjective(new ObtainObjective(typeof(WhiteChocolate), "White Chocolates", 5, 0xF11));
            AddObjective(new ObtainObjective(typeof(DarkSapphire), "Dark Sapphire", 1, 0x3192));

            AddReward(new BaseReward(typeof(AverageImbuingBag), 1113768));//Average Imbuing Bag
            AddReward(new BaseReward("Loyalty Rating"));
        }

        /*The Exchange*/
        public override object Title => 1113777;
        //Hello there! Hail and well met, and all of that. I must apologize in advance for being
        //so impatient, but you must help me! You see, my mother and my eldest sister are visiting
        //soon, and I haven’t seen them in quite awhile, so I want to present them both with a
        //surprise when they arrive.<br><br>My sister absolutely adores white chocolate, but 
        //gargoyles don’t seem to care for it much, so I haven’t been able to find any here.
        //It was recently my mother’s birthday, and I know that she would love some finely 
        //crafted gargish jewelry, but the jeweler hasn’t had her favorite jewel in stock for 
        //quite some time. If you could help me obtain five pieces of white chocolate and one
        //dark sapphire, I will reward you with a bag of hard to obtain imbuing ingredients.
        public override object Description => 1113778;
        //Oh, no, you must help me! Please say that you will!
        public override object Refuse => 1113779;
        //Remember, I need five pieces of white chocolate, and one dark sapphire. Please do hurry!
        public override object Uncomplete => 1113780;
        //Oh, thank you so very much! I cannot begin to thank you enough for helping me find 
        //these presents. Here is your reward. You’ll have to excuse me while I set this dark
        //sapphire in a setting that will best highlight the cut. Farewell!
        public override object Complete => 1113781;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
