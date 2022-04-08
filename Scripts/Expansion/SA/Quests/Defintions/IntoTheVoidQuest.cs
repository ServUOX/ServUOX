using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class IntoTheVoidQuest : BaseQuest
    {
        public IntoTheVoidQuest()
            : base()
        {
            AddObjective(new SlayObjective(typeof(BaseVoidCreature), "Void Daemons", 10));

            AddReward(new BaseReward(typeof(AbyssReaver), 1112694)); // Abyss Reaver
        }

        public override bool DoneOnce => true;
        public override object Title => 1112687;
        public override object Description => 1112690;
        public override object Refuse => 1112691;
        public override object Uncomplete => 1112692;
        public override object Complete => 1112693;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
