using System;
using Server;

namespace Server.Items
{
    public class PowderCharge : Item, ICommodity
    {
        public override int LabelNumber => 1116160;  // powder charge

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        [Constructible]
        public PowderCharge()
            : this(1)
        {
        }

        [Constructible]
        public PowderCharge(int amount)
            : base(0xA2BE)
        {
            Stackable = true;
            Amount = amount;
        }

        public PowderCharge(Serial serial)
            : base(serial)
        {
        }

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

    public class LightPowderCharge : Item, ICommodity
    {
        public override int LabelNumber => 1116159;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        [Constructible]
        public LightPowderCharge() : this(1)
        {
        }

        [Constructible]
        public LightPowderCharge(int amount) : base(16932)
        {
            Hue = 2031;
            Stackable = true;
            Amount = amount;
        }


        public LightPowderCharge(Serial serial) : base(serial) { }

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

    public class HeavyPowderCharge : Item, ICommodity
    {
        public override int LabelNumber => 1116160;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        [Constructible]
        public HeavyPowderCharge() : this(1)
        {
        }

        [Constructible]
        public HeavyPowderCharge(int amount)
            : base(16932)
        {
            Hue = 2031;
            Stackable = true;
            Amount = amount;
        }

        public HeavyPowderCharge(Serial serial) : base(serial) { }

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
