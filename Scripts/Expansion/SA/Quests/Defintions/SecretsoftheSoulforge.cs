using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class SecretsoftheSoulforge : BaseQuest
    {
        public SecretsoftheSoulforge()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(MagicalResidue), "Magical Residue", 50, 0x2DB1));

            AddReward(new BaseReward(typeof(ScrollBox3), 1, 1112530));//Knowledge //Reward should be A Wondrous or An Exalted Scroll of Imbuing (105 or 110 Skill) 
        }
        /*Secrets of the Soulforge */
        public override object Title => 1112535;
        /*Stand near a soulforge and use the Imbuing skill to unravel magical items.
         * Retrieve Magical Residue and give it to Beninort. There are three magical
         * elements that the soulforge can unravel from a magic item: Magical Residue,
         * Enchanted Essence, and Relic Fragments. Each Imbuing recipe includes a quantity
         * of one of these ingredients.<br><center>------</center><br>I am pleased to meet you.
         * I have been assigned to teach the use of the soulforge to those approved by the Queen.
         * I am pleased that you are approved. To begin your training, you must learn to unravel magic items.
         * You must have a magic item. Stand near a soulforge and unravel the magic item into magical
         * ingredients until you obtain Magical Residue.<BR><BR>Return to me with the Magical Residue,
         * and I will reward you with a scroll of power.*/
        public override object Description => 1112522;
        /*A pleasure to have met you. I hope for your safe return in these dark times.*/
        public override object Refuse => 1112523;
        /*Well met! Remember to stand near a soulforge and unravel magic items into ingredients.
         * I encourage you. I have confidence in your ability. Do what I instruct, and bring me the result.*/
        public override object Uncomplete => 1112524;
        /*You have learned well. You display discipline. Remember this lesson and continue to master your craft.*/
        public override object Complete => 1112527;
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
