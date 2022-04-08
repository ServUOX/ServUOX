using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class MasteringtheSoulforge : BaseQuest
    {
        public MasteringtheSoulforge()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(RelicFragment), "Relic Fragments", 50, 0x2DB3));

            AddReward(new BaseReward(typeof(ScrollBox2), "Knowledge"));
        }
        /*Mastering the Soulforge*/
        public override object Title => 1112537;
        /*Stand near a soulforge and use the Imbuing skill to unravel magical items. Retrieve Relic Fragments and give them to Ansikart.
         * There are three magical elements that the soulforge can unravel from a magic item: Magical Residue, Enchanted Essence,
         * and Relic Fragments. Each Imbuing recipe includes a quantity of one of these ingredients.<br><center>------</center><br>Greetings!
         * To complete your training, you must learn to unravel the most powerful magic items. You must have a magic item.
         * Stand near a soulforge and unravel the magic item into magical ingredients until you obtain Relic Fragments.
         * <BR><BR>Return to me with the Relic Fragments, and I will reward you with a scroll of power.*/
        public override object Description => 1112529;
        /*May your life be filled with knowledge and wisdom. Until we meet again.*/
        public override object Refuse => 1112549;
        public override object Uncomplete => 1112550;
        public override object Complete => 1112551;
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
