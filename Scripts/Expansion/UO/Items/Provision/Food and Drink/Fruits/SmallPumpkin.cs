
namespace Server.Items
{
    public class SmallPumpkin : Food
    {
        [Constructible]
        public SmallPumpkin()
            : this(1)
        {
        }

        [Constructible]
        public SmallPumpkin(int amount)
            : base(amount, 0xC6C)
        {
            Weight = 1.0;
            FillFactor = 8;
        }

        public SmallPumpkin(Serial serial)
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
