using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class PuttingThePiecesTogetherQuest : BaseQuest
    {
        public PuttingThePiecesTogetherQuest()
        {
            AddObjective(new ObtainObjective(typeof(TatteredAncientScroll), "Tattered Ancient Scrolls", 5, 0x1437));

            AddReward(new BaseReward(typeof(DustyMuseumBag), 1112994));
            AddReward(new BaseReward("Loyalty Rating"));
        }

        /* Putting The Pieces Together */

        public override object Title => 1112796;

        public override object Description => 1112921;

        public override object Refuse => 1112922;

        public override object Uncomplete => 1112923;

        public override object Complete => 1112924;

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
