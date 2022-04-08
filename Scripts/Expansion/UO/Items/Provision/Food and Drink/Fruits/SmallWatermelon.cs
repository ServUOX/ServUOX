
namespace Server.Items
{
    public class SmallWatermelon : Food
    {
        [Constructible]
        public SmallWatermelon()
            : this(1)
        {
        }

        [Constructible]
        public SmallWatermelon(int amount)
            : base(amount, 0xC5D)
        {
            Weight = 5.0;
            FillFactor = 5;
        }

        public SmallWatermelon(Serial serial)
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
            _ = reader.ReadInt();
        }
    }
}
