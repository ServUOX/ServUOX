using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class UntanglingTheWebQuest : BaseQuest
    {
        public UntanglingTheWebQuest()
            : base()
        {
            AddObjective(new AcidCreaturesObjective(typeof(IAcidCreature), "acid creatures", 12));

            AddReward(new BaseReward(typeof(AcidPopper), 1095058));
        }

        /* Untangling the Web */
        public override object Title => 1095050;
        /* Kill Acid Slugs and Acid Elementals to fill Vernix's jars.  Return to Vernix with the filled jars for
        your reward.<br><center>-----</center><br>Vernix say, stranger has proven big power.  You now ready to 
        help Green Goblins big time.  Green Gobin and outsider not need to be enemy.  Need to be friend against 
        common enemy.  You help Green Goblins with important mission.  We tell you important information.  Help 
        your people not be eaten.<br><br>Go find acid slugs and acid elementals.  They very dangerous, but me 
        think you can handle it.  Fill these jars with acid from these. */
        public override object Description => 1095052;
        /* Hmm... Perhaps you are afraid.  Hmm... Very good to know.  Ok, you go and do.  You come back.
        Let me know if you stop being afraid of acid. */
        public override object Refuse => 1095053;
        /* Acid very important to stopping master plan of Gray Goblins.  You get acid, ok? */
        public override object Uncomplete => 1095054;
        /* This very good.  Now King Vernix tell you valuable secret.  Acid good for melting wolf spider webs.
        Webs very strong, but not stronger than acid.  Vernix gives to you pay for good work. */
        public override object Complete => 1095057;
        public override void OnCompleted()
        {
            Owner.SendLocalizedMessage(1095056, null, 0x23); // Vernix's Jars are now full.							
            Owner.PlaySound(CompleteSound);
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

        private class AcidCreaturesObjective : SlayObjective
        {
            public AcidCreaturesObjective(Type creature, string name, int amount)
                : base(creature, name, amount)
            {
            }

            public override void OnKill(Mobile killed)
            {
                base.OnKill(killed);

                if (!Completed)
                    Quest.Owner.SendLocalizedMessage(1095055); // You collect acid from the creature into the jar.
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
}
