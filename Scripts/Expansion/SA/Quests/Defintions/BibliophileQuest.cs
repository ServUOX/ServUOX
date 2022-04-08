using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class BibliophileQuest : BaseQuest
    {
        public override object Title => 1094968;         // Bibliophile

        public override object Description => 1094970;   /*Travel into the Underworld and find the Flint's stolen
                                                                         * log book.  Return to Flint with the Log book for your 
                                                                         * reward.<br><center>-----</center><br>Ye will not be 
                                                                         * believin' what misfortune has befallen me now!  No sooner
                                                                         * have I gotten me Barley back, those goblins have gone 
                                                                         * and taken me log book!  How in the two dimensions am I 
                                                                         * supposted to keep up with all of tha supplies with no 
                                                                         * log book?  Of course, those lay about guards dinna see 
                                                                         * anything!<br><br>Listen, ye 've been a blessin' to ol' 
                                                                         * Flint in tha past so I wanta make ye another offer.  
                                                                         * If'n ye will bring ol' Flint's book back ta 'im, I will
                                                                         * give ye a keg o' me special brew!*/

        public override object Refuse => 1094971;        /*'Tis a fine thing to do to a friend in need!  But so be it.
                                                                         * 'Tis not the first time today ol' Flint has been let 
                                                                         * down. Won't be the last.*/

        public override object Uncomplete => 1094972;    /*'Ave ye laid hold to ol' Flint's log book yet?  Oh, I'm sorry, 
                                                                         * I jes' figured ye would have it back by now... Carry on then!*/

        public override object Complete => 1094975;      /*'Ave ye laid hold to ol' Flint's log book did ye?  Let me
                                                                         * 'ave a look here!  Bloomin' savages!  They dog eared one
                                                                         * o' the pages and bent the corner o' me cover! Blast! 
                                                                         * Well, that's not fer you to be worryin' about.  Here be
                                                                         * yer pay as promised a keg of me brew far yer own.  This 
                                                                         * keg is special made by me own design, ye can use it to 
                                                                         * refill that bottle I gave ye.  My brew is too strong for
                                                                         * normal bottles, so I hope ye listened to ol' Flint and 
                                                                         * kept that bottle!*/

        public override QuestChain ChainID => QuestChain.FlintTheQuartermaster;

        public BibliophileQuest()
        {
            AddObjective(new ObtainObjective(typeof(FlintsLogbook), "Flint's Logbook", 1, 7185));
            AddReward(new BaseReward(typeof(KegOfFlintsPungnentBrew), 1113608));
        }

        public override void OnObjectiveUpdate(Item item)
        {
            Owner.SendLocalizedMessage(1094974); // This appears to be Flint's logbook.  It is not clear why the goblins were using it in a ritual.  Perhaps they were summoning a nefarious intention?
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int v = reader.ReadInt();
        }
    }
}
