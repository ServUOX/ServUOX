using System;
using Server.Items;
using Server.Mobiles;
using Server.Spells.SkillMasteries;
using System.Collections.Generic;

namespace Server.Engines.Quests
{
    public class WieldingTheSonicBladeQuest : BaseQuest
    {
        public WieldingTheSonicBladeQuest() : base()
        {
            AddObjective(new DiscordObjective());

            AddReward(new BaseReward(1115699)); // Recognition for mastery of song wielding.
            AddReward(new BaseReward(typeof(BookOfMasteries), 1028794));
        }

        public override object Title => 1115696;        //Wielding the Sonic Blade

        public override object Description => 1115697;  /*This quest is the single quest required for a player to unlock the discordance mastery 
                                                                        * abilities for bards. This quest can be completed multiple times to reinstate the discordance 
                                                                        * mastery. To prove yourself worthy, you must first be a master of discordance and musicianship. 
                                                                        * You must be willing to distort your notes to bring pain to even the most indifferent ears.*/

        public override object Refuse => 1115700;        //You must strive to spread discord.

        public override object Uncomplete => 1115700;    //You must strive to spread discord.

        public override object Complete => 1115701;      /* You have proven yourself worthy of wielding your music as a weapon. Rend the ears of your 
                                                                         * foes with your wails of discord. Let your song be feared as much as any sword.*/

        public override bool CanOffer()
        {
            if (Owner == null)
                return false;

            if (Owner.Skills[SkillName.Musicianship].Base < 90 || Owner.Skills[SkillName.Discordance].Base < 90)
            {
                Owner.SendLocalizedMessage(1115703); // Your skills in this focus area are less than the required master level. (90 minimum)
                return false;
            }

            foreach (BaseQuest q in Owner.Quests)
            {
                if (q is TheBeaconOfHarmonyQuest || q is IndoctrinationOfABattleRouserQuest)
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

            MasteryInfo.LearnMastery(Owner, SkillName.Discordance, 3);

            SkillMasterySpell.SetActiveMastery(Owner, SkillName.Discordance);
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
