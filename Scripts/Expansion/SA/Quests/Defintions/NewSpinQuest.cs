using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class NewSpinQuest : BaseQuest
    {
        public override QuestChain ChainID => QuestChain.LaifemTheWeaver;
        public override TimeSpan RestartDelay => TimeSpan.FromMinutes(30);
        public override bool DoneOnce => true;

        /* A New Spin on Things */
        public override object Title => 1113260;
        /* Oh my! Now isn't this something? A Gargoyle seeking to master the ways of our humble little industry.
         * Why, this is nothing short of inspirational!<BR><BR>I think I have just the thing for him. There's a
         * book over... oh! It's a her? My apologies, I just don't have a knack for those Gargish names you know!
         * <BR><BR>Regardless, please take this back to the young lady, if you would be so kind.<BR><BR>Best regards! */
        public override object Description => 1113261;
        /* Oh dear, truly? I'm sure she'd be very pleased to have this, and I don't have the means to journey there
         * myself.<BR><BR>How very, very unfortunate... */
        public override object Refuse => 1113262;
        /* Dermott wishes you to deliver the book "Mastering the Art of Weaving" to Laifem so she learn the ways of
         * Britannian weaving. */
        public override object Uncomplete => 1113263;
        /* This is perfect! Thank you so, so much!<BR><BR><I>Laifem eagerly begins reading the book while pacing about
         * the room</I><BR><BR>Yes, yes I see. <I>*nods*</I> And the loops are done in a... with mohair knots... 
         * <I>*her fingers begin weaving idly in the air as she thinks*</I> and then to finish off the tassels I just...
         * <BR><BR>This is everything I need to begin weaving my very own decorative carpets. You see, I won't just make
         * the larger carpets, I'm going to make them in smaller pieces that can be put together to make any size or 
         * combination! Isn't that wonderful? */
        public override object Complete => 1113264;

        public NewSpinQuest()
            : base()
        {
            AddObjective(new DeliverObjective(typeof(MasteringWeaving), "Mastering the Art of Weaving", 1, typeof(Laifem), "Laifem (Royal City)"));

            AddReward(new BaseReward(1113250)); // Access to Laifem's inventory of decorative carpets.
        }

        public override void GiveRewards()
        {
            base.GiveRewards();

            Owner.SendLocalizedMessage(1113265, "", 0x2A); // You have succeeded in aiding Laifem's attempts to master Britannian weaving, and can now access her inventory of decorative carpets!
            Owner.CanBuyCarpets = true;
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
