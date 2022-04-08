using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class TheArisenQuest : BaseQuest
    {
        public TheArisenQuest()
            : base()
        {
            if (0.30 > Utility.RandomDouble())
            {
                AddObjective(new SlayObjective(typeof(GargoyleShade), "Gargoyle Shade", 10));
            }
            else if (0.50 > Utility.RandomDouble())
            {
                AddObjective(new SlayObjective(typeof(EffetePutridGargoyle), "Effete Putrid Gargoyle", 10));
            }
            else
            {
                AddObjective(new SlayObjective(typeof(EffeteUndeadGargoyle), "Effete Undead Gargoyle", 10));
            }

            AddReward(new BaseReward(typeof(NecklaceofDiligence), 1113137));
        }

        public override bool DoneOnce => true;

        /* The Arisen */
        public override object Title => 1112538;

        public override object Description => 1112539;

        public override object Refuse => 1112540;

        public override object Uncomplete => 1112517;

        public override object Complete => 1112543;

        public override void OnCompleted()
        {
            Owner.SendLocalizedMessage(1112542, null, 0x23);
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
    }
}
