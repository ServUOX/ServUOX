using System;
using Server;

namespace Server.Items
{
    public class Swab : Item
    {
        [Constructible]
        public Swab()
            : base(Utility.RandomMinMax(16968, 16969))
        {
        }

        public Swab(Serial serial) : base(serial) { }

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