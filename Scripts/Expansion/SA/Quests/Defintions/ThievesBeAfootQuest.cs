using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class ThievesBeAfootQuest : BaseQuest
    {
        public override object Title => 1094958;         // Thieves Be Afoot!

        public override object Description => 1094960;   /*Travel into the Underworld and search for the stolen
                                                                         * barrels of barley.  Return them to Quartermaster Flint
                                                                         * for your reward.<br><center>-----</center><br>Hail 
                                                                         * Traveler.  Trying to find the fabled Stygian Abyss are 
                                                                         * ye?  I say good luck, an' be weary for I believe there 
                                                                         * to be a den o' thieves hidden in them halls!  Aye, just
                                                                         * last night I lost four barrels o' Barley.  I know they
                                                                         * be sayin' that none but critters live in them halls, but
                                                                         * I've looked every place I dare and seen no sign o' me
                                                                         * barrels.<br><br>Hope them lazy Society folk got a good 
                                                                         * nap last night, cause they wan have any o' me fine Barley
                                                                         * based products unless we get those barrels back!  
                                                                         * I canne believe none of them loafers who was guarding 
                                                                         * the door saw nothin!  Oy, it makes me so mad, I must not
                                                                         * think of it and control me temper... It's a frickin' 
                                                                         * barrel of Barley!  How could they miss seeing it???
                                                                         * <br><br>Sorry... I don' mean ta be takin' it out on ye.
                                                                         * Tell you what friend.  You find those barrels and I will
                                                                         * pay you for bringin' them back.  There be some nasty 
                                                                         * stuff in thar, so if'n ye bring back all four, I have 
                                                                         * somethin' special I will share with ye!*/

        public override object Refuse => 1094961;        /*Fine then, be on yar way but be warned!  Ol' Flint makes
                                                                         * the best refreshin' barley products in tha known world! 
                                                                         * That barley will not profit ye if'n ol' Flint ha' not
                                                                         * prepared it proper!*/

        public override object Uncomplete => 1094962;    /*What?  Back so soon and narry a barrel in sight?  Be y
                                                                         * e advised that ye are not the only traveler ol' Flint 
                                                                         * has a lookin fer his barrels!  If'n ye are gonna profit
                                                                         * of me potions, ye best be about it!*/

        public override object Complete => 1094965;      /*Ah, thar she is!  That's me barrel all right, I knew
                                                                         * someone had taken it in thar!  Goblins you say? Oy,
                                                                         * they be a nasty bit o' business, ain't they?  Well, a 
                                                                         * deal's a deal, here is yar potion as promised!*/

        public override QuestChain ChainID => QuestChain.FlintTheQuartermaster;
        public override Type NextQuest => typeof(BibliophileQuest);

        public ThievesBeAfootQuest()
        {
            AddObjective(new ObtainObjective(typeof(BarrelOfBarley), "Barrel of Barley", 4, 4014));
            AddReward(new BaseReward(typeof(BottleOfFlintsPungnentBrew), 1094967));
        }

        public override void OnObjectiveUpdate(Item item)
        {
            Owner.SendLocalizedMessage(1094964); // This barrel fits the description Flint gave you.  Boy is it heavy!
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
