using System;

namespace Server.Items
{
    [Flipable(0x4A9A, 0x4A9B)]
    public class SantaStatue : MonsterStatuette
    {
        public override int LabelNumber => 1097968;  // santa statue

        [Constructible]
        public SantaStatue()
            : base(MonsterStatuetteType.Santa)
        {
            Weight = 10.0;
        }

        public SantaStatue(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties => ObjectPropertyList.Enabled;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
