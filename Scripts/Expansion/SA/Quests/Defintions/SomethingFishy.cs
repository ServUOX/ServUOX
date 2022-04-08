using System;
using Server.Items;

namespace Server.Engines.Quests
{
    public class SomethingFishy : BaseQuest
    {
        /* Something Fishy */
        public override object Title => 1095059;

        public override object Description => 1095043;

        public override object Refuse => 1095044;

        public override object Uncomplete => 1095045;

        public override object Complete => 1095048;

        public SomethingFishy() : base()
        {
            AddObjective(new ObtainObjective(typeof(RedHerring), "Red Herring", 1, 0x9cc));

            AddReward(new BaseReward(typeof(BarreraaksRing), 1095049));
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
