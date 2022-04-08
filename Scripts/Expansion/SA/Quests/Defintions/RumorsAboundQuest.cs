using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class RumorsAboundQuest : BaseQuest
    {
        public RumorsAboundQuest()
            : base()
        {
            AddObjective(new DeliverObjective(typeof(EgwexemWrit), "Egwexem's Writ", 1, typeof(Naxatilor), "Naxatilor"));

            AddReward(new BaseReward(1112731));
        }

        public override TimeSpan RestartDelay => TimeSpan.FromHours(12);

        public override bool DoneOnce => true;

        /* Rumors Abound */
        public override object Title => 1112514;
        public override object Description => 1112515;
        public override object Refuse => 1112516;
        public override object Uncomplete => "You never spoke to Naxatillor yet! Go to him!";

        public override object Complete => 1112518;
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
