using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class KnowledgeoftheSoulforge : BaseQuest
    {
        public KnowledgeoftheSoulforge()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(EnchantedEssence), "Enchanted Essence", 50, 0x2DB2));

            AddReward(new BaseReward(typeof(ScrollBox), 1, "Knowledge"));
            //missing rewards 
            //A Mythical Scroll of Imbuing (115 Skill)
            ///A random Runic Mallet and Chisels(rare)
        }
        /*Knowledge of the Soulforge*/
        public override object Title => 1112536;
        /*Stand near a soulforge and use the Imbuing skill to unravel magical items. Retrieve Enchanted Essence and give it to Aurvidlem.
         * There are three magical elements that the soulforge can unravel from a magic item: Magical Residue, Enchanted Essence,
         * and Relic Fragments. Each Imbuing recipe includes a quantity of one of these ingredients.<br><center>------</center><br>
         * Well met! To continue your training, you must learn to unravel more powerful magic items. You must have a magic item.
         * Stand near a soulforge and unravel the magic item into magical ingredients until you obtain Enchanted Essence.<BR><BR>
         * Return to me with the Enchanted Essence, and I will reward you with a scroll of power.*/
        public override object Description => 1112526;
        /*Take care and be safe. Many great dangers surround us.*/
        public override object Refuse => 1112546;
        /*We meet again. Remember to stand near a soulforge and unravel magic items into ingredients.
         * You have the aptitude to be a great Artificer.
         * Do what I instruct, and bring me the result.*/
        public override object Uncomplete => 1112547;
        /*Well done! You are becoming a skilled Artificer. Come back if you wish to continue to learn.*/
        public override object Complete => 1112548;
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
