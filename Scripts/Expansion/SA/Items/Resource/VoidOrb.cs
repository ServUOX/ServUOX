using System;

namespace Server.Items
{
    public class VoidOrb : Item, ICommodity
    {
        [Constructible]
        public VoidOrb()
            : this(1)
        {
        }

        [Constructible]
        public VoidOrb(int amount)
            : base(0x573E)
        {
            Stackable = true;
            Amount = amount;
        }

        public VoidOrb(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override int LabelNumber => 1113354;// void orb
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
