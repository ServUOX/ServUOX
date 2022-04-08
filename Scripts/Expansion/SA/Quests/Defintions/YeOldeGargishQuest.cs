using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class YeOldeGargishQuest : BaseQuest
    {
        public YeOldeGargishQuest()
        {
            AddObjective(new ObtainObjective(typeof(UntranslatedAncientTome), "Untranslated Ancient Tome", 1, 0xFF2, 0, 2405));

            AddReward(new BaseReward(typeof(BulgingMuseumBag), 1112995));
            AddReward(new BaseReward("Loyalty Rating"));
        }

        /* Ye Olde Gargish */

        public override object Title => 1112797;

        public override object Description => 1112925;

        public override object Refuse => 1112926;

        public override object Uncomplete => 1112927;

        public override object Complete => 1112928;

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
