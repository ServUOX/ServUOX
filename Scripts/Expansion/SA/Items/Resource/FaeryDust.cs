using System;

namespace Server.Items
{
    public class FaeryDust : Item, ICommodity
    {

        [Constructible]
        public FaeryDust() : this(1)
        {
        }

        [Constructible]
        public FaeryDust(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public FaeryDust(int amount) : base(0x5745)
        {
            Stackable = true;
            Amount = amount;
        }

        public FaeryDust(Serial serial) : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override int LabelNumber => 1113358; // faery dust

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
