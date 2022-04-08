using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class ScrapingtheBottom : BaseQuest
    {
        /* SomethingFishy */
        public override object Title => 1095059;

        public override object Description => 1095061;

        public override object Refuse => 1095062;

        public override object Uncomplete => 1095063;

        public override object Complete => 1095065;

        public ScrapingtheBottom() : base()
        {
            AddObjective(new ObtainObjective(typeof(MudPuppy), "Mud Puppy", 1, 0x9cc));

            AddReward(new BaseReward(typeof(XenrrFishingPole), 1095066));
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
