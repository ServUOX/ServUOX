using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class ALittleSomething : BaseQuest
    {
        public ALittleSomething()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(BrilliantAmber), "Brilliant Amber", 1, 0x3199));

            AddReward(new BaseReward(typeof(MeagerImbuingBag), 1, "Meager Imbuing Bag"));
        }
        /*A Little Something*/
        public override object Title => 1113772;
        /*You want some basic imbuing ingredients, the kind that can be obtained from the beasts of Ter Mur? Well,
         * I can help you. I usually don’t trade in such low end items, and I’m not sure why you cannot hunt them down yourself,
         * but today you are in luck. If you will bring me something of which I do have a need, perhaps a piece of brilliant amber,
         * I shall reward you with what I can find laying around. What exactly, I can’t tell you, as I do not have time to inventory
         * such things.*/
        public override object Description => 1113773;
        /*Well, okay, if that is what you wish.*/
        public override object Refuse => 1113774;
        /*We have agreed to trade one piece of brilliant amber for some of the lesser
         * imbuing ingredients. Bring that to me, and I shall uphold my part of our bargain.*/
        public override object Uncomplete => 1113775;
        /*Well, it is not the most brilliant piece of amber, but I suppose it shall do. Here is a bag with the ingredients I have found. In fact,
         * I found more of these lesser imbuing ingredients than I believed I possessed, so return to me if you wish to make another trade.*/
        public override object Complete => 1113776;
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
