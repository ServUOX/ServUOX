using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class AWorthyPropositionQuest : BaseQuest
    {
        public AWorthyPropositionQuest()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(BambooFlute), "Bamboo Flutes", 10, 0x2805));
            AddObjective(new ObtainObjective(typeof(ElvenFletching), "Elven Fletching", 1, 0x5737));

            AddReward(new BaseReward(typeof(ValuableImbuingBag), 1113769));//Valuable Imbuing Bag
            AddReward(new BaseReward("Loyalty Rating"));
        }

        /* A Worthy Proposition */
        public override object Title => 1113782;

        //Hello, welcome to the shop. I don't own it, but the gargoyles here are as keen to 
        //learn from me as I am from them. They've been helping me with the work on my latest
        //invention, but I am short some parts. Perhaps you could help me?<br><br>I have heard 
        //that the bamboo flutes of the Tokuno Islands are exceptionally strong for their weight, 
        //and nothing can beat elven fletching for strength in holding them together. If you 
        //would bring me, say, ten bamboo flutes and some elven fletching, I have some valuable
        //imbuing ingredients I’ll give you in exchange. What do you say?
        public override object Description => 1113783;
        //Well, if you change your mind, I’ll be here.
        public override object Refuse => 1113784;
        //Hmm, what is that? Oh yes, I would like you to bring me ten bamboo flutes and some elven
        //fletching for my fly… er, my invention.
        public override object Uncomplete => 1113785;
        //These are of fine quality! I think they will work just fine to reinforce the floor of the 
        //basket. What’s that? Did I say basket? I meant, bakery! Yes, I am inventing a, um, floor 
        //for a bakery. There is a great need for that, you know! Ok, now please leave so I can get 
        //back to work. Thank you, bye, bye!
        public override object Complete => 1113786;

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
