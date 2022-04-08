using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class WeavingFriendshipsQuest : BaseQuest
    {
        public override QuestChain ChainID => QuestChain.LaifemTheWeaver;
        public override Type NextQuest => typeof(NewSpinQuest);
        public override TimeSpan RestartDelay => TimeSpan.FromMinutes(30);
        public override bool DoneOnce => true;

        /* Weaving Friendships */
        public override object Title => 1113254;
        /* <I>Laifem stares down at the ruins of her first carpet weaving attempt</I><BR><BR>Hrm... I guess I thought
         * this would be a bit easier.<BR><BR><I>She reaches up and twists on her ear a little, obviously deep in 
         * thought</I><BR><BR>You know... I think I need some professional assistance! There's a tailoring shop in
         * Vesper called, hrm, "The Spinning..." "The Spinning..." something or other. Sorry, I just don't have a head
         * for all those clever Britannian shop names.<BR><BR><I>*laughs*</I><BR><BR> I'm sure someone there could help,
         * do you think you could deliver a letter of introduction for me? */
        public override object Description => 1113255;
        /* But I'm so close! If I can just talk to the right people we'll be in business for sure! */
        public override object Refuse => 1113257;
        /* There should be a man, er, a human one at that, who owns a tailoring shop in Vesper. Maybe he can help me? */
        public override object Uncomplete => 1113258;
        /* A letter? From a Gargoyle you say? */
        public override object Complete => 1113259;

        public WeavingFriendshipsQuest() : base()
        {
            AddObjective(new DeliverObjective(typeof(LetterOfIntroduction), "Letter of Introduction", 1, typeof(Dermott), "Dermott (Vesper)"));

            AddReward(new BaseReward(1113256)); // A step closer to having access to Laifem's inventory of decorative carpets.
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
