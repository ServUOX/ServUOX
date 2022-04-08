using System;
using Server.Items;
using Server.Mobiles;
using Server.Spells.SkillMasteries;
using System.Collections.Generic;

namespace Server.Engines.Quests
{
    public class TheBeaconOfHarmonyQuest : BaseQuest
    {
        public TheBeaconOfHarmonyQuest() : base()
        {
            AddObjective(new PeacemakingObjective());

            AddReward(new BaseReward(1115679)); // Recognition for mastery of spirit soothing.
            AddReward(new BaseReward(typeof(BookOfMasteries), 1028794));
        }

        public override object Title => 1115677;        //The Beacon of Harmony

        public override object Description => 1115676;  /* This quest is the single quest required for a player to unlock the peacemaking 
                                                                        * mastery abilities for bards. This quest can be completed multiple times to reinstate
                                                                        * the peacemaking mastery. To prove yourself worthy, you must first be a master of 
                                                                        * peacemaking and musicianship. You must be able to calm even the most vicious beast
                                                                        * into tranquility.*/

        public override object Refuse => 1115680;        //To deliver peace you must persevere. 

        public override object Uncomplete => 1115680;    //To deliver peace you must persevere. 

        public override object Complete => 1115681;      /* You have proven yourself a beacon of peace and a bringer of harmony. Only 
                                                                         * a warrior may choose the peaceful solution, all others are condemned to it. 
                                                                         * May your message of peace flow into the world and shelter you from harm.*/

        public override bool CanOffer()
        {
            if (Owner == null)
                return false;

            if (Owner.Skills[SkillName.Musicianship].Base < 90 || Owner.Skills[SkillName.Peacemaking].Base < 90)
            {
                Owner.SendLocalizedMessage(1115703); // Your skills in this focus area are less than the required master level. (90 minimum)
                return false;
            }

            foreach (BaseQuest q in Owner.Quests)
            {
                if (q is IndoctrinationOfABattleRouserQuest || q is WieldingTheSonicBladeQuest)
                {
                    Owner.SendLocalizedMessage(1115702); //You must quit your other mastery quests before engaging on a new one.
                    return false;
                }
            }

            return true;
        }

        public override void GiveRewards()
        {
            base.GiveRewards();

            MasteryInfo.LearnMastery(Owner, SkillName.Peacemaking, 3);

            SkillMasterySpell.SetActiveMastery(Owner, SkillName.Peacemaking);
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
