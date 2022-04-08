using System;

namespace Server.Items
{
    public class SlithTongue : Item, ICommodity
    {
        [Constructible]
        public SlithTongue()
            : this(1)
        {
        }

        [Constructible]
        public SlithTongue(int amount)
            : base(0x5746)
        {
            Stackable = true;
            Amount = amount;
        }

        public SlithTongue(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override int LabelNumber => 1113359;// slith tongue
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
