using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class ABrokenVaseQuest : BaseQuest
    {
        public ABrokenVaseQuest()

        {
            AddObjective(new ObtainObjective(typeof(AncientPotteryFragments), "Ancient Pottery Fragments", 10, 0x223B, 0, 2108));

            AddReward(new BaseReward(typeof(MeagerMuseumBag), 1112993));
            AddReward(new BaseReward("Loyalty Rating"));
        }

        /*A Broken Vase */

        public override object Title => 1112795;

        public override object Description => 1112917;

        public override object Refuse => 1112918;

        public override object Uncomplete => 1112919;

        public override object Complete => 1112920;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}
