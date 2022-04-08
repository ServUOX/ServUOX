using System;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class FrightenedDryad : MondainQuester
    {
        [Constructible]
        public FrightenedDryad()
            : base("The Frightened Dryad")
        {
        }

        public FrightenedDryad(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(BoundToTheLandQuest)
                };

        public override void InitBody()
        {
            InitStats(100, 100, 25);
            Female = true;
            Body = 266;
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
