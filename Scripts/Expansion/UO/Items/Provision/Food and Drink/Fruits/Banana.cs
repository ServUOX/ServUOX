
namespace Server.Items
{
    [Flipable(0x171F, 0x1720)]
    public class Banana : Food
    {
        [Constructible]
        public Banana()
            : this(1)
        {
        }

        [Constructible]
        public Banana(int amount)
            : base(amount, 0x171f)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Banana(Serial serial)
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
