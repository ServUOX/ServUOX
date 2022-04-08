using System;
using Server.Items;

namespace Server.Items
{
    public class WorkableGlass : Item, ICommodity
    {
        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override int LabelNumber => 1154170;  // workable glass

        [Constructible]
        public WorkableGlass() : this(1)
        {
        }

        [Constructible]
        public WorkableGlass(int amount) : base(19328)
        {
            Stackable = true;
            Amount = amount;
            Weight = 1.0;
        }

        public WorkableGlass(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
