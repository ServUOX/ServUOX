using System;
using Server.Engines.Quests;

namespace Server.Items
{
    public class DenthesJournal : BaseQuestItem
    {
        [Constructible]
        public DenthesJournal()
            : base(0xFF2)
        {
        }

        public DenthesJournal(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(LastWordsQuest)
                };
        public override int LabelNumber => 1073240;// Lord Denthe's Journal
        public override int Lifespan => 3600;// ?
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
