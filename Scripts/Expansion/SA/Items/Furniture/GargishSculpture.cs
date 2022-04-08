using System;

namespace Server.Items
{
    [Flipable(0x403F, 0x4040)]
    public class GargishSculpture : Item
    {
        [Constructible]
        public GargishSculpture()
            : base(0x403F)
        {
            Weight = 1.0;
        }

        public GargishSculpture(Serial serial)
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