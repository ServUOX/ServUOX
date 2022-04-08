using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class ShearingKnowledgeQuest : BaseQuest
    {
        public override QuestChain ChainID => QuestChain.LaifemTheWeaver;
        public override Type NextQuest => typeof(WeavingFriendshipsQuest);
        public override TimeSpan RestartDelay => TimeSpan.FromMinutes(30);
        public override bool DoneOnce => true;

        /*Shearing Knowledge */
        public override object Title => 1113245;
        /*Welcome to my little shop!<BR><BR>Don't you just love these beautiful carpet samples?
         * Look at these embroidery patterns! And the intricate knotwork! It was sure worth every
         * gold piece I paid to have these shipped from Vesper.<BR><BR>What's that? No, no, I'm 
         * sorry, these aren't for sale! I'm working towards recreating each of these gorgeous
         * styles myself, you see, and just wanted to show my future customers what they might
         * one day expect! By the skies though, how do I even begin learning these new patterns?
         * <BR><BR>I know! If you help me get started, you could be one of my first customers! 
         * Yes, that's it - I need to get into the mind of a Britannian crafter, so I need 
         * Britannian wool! Real, natural wool, mind you, none of the cheap sort you see on the 
         * vendors.<BR><BR>Maybe you could find some by shearing some of those... what do you 
         * call them? Sherp? Sheeple? */
        public override object Description => 1113246;
        /*Oh no, really? I was hoping you could be one of my first patrons... */
        public override object Refuse => 1113251;
        /*Creatures in Ter Mur simply won't do! And the vendors? Horrible quality! You'll have 
         *to visit Britannia and shear a few sheep by hand to obtain some authentic Britannian wool. */
        public override object Uncomplete => 1113252;
        /*Wow! Isn't this amazing? It's so soft, so pure - surely this is the key to my efforts!<BR><BR>
         * <I>Laifem skillfully spins the wool into a beautiful ball of white yarn - before you know it, 
         * she's staring down at her first attempt to weave a Britannian carpet</I> */
        public override object Complete => 1113253;

        public ShearingKnowledgeQuest() : base()
        {
            AddObjective(new ObtainObjective(typeof(BritannianWool), "Britannian Wool", 10, 0xDF8));

            AddReward(new BaseReward(1113256)); /*A step closer to having access to Laifem's inventory of decorative carpets. */
        }

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
