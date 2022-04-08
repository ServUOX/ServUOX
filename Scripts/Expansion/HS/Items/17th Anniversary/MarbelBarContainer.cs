using System;

namespace Server.Items
{
    [Furniture]
    public class MarbelBarContainer : LockableContainer, IEngravable
    {
        [Constructible]
        public MarbelBarContainer()
            : base(0x99C9)
        {
            Weight = 2.0;
        }

        public MarbelBarContainer(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 15.0)
                Weight = 2.0;
        }
    }
}