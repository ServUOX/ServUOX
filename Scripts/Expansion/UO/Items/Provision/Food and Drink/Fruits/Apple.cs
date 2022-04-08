
namespace Server.Items
{
    public class Apple : Food
    {
        [Constructible]
        public Apple()
            : this(1)
        {
        }

        [Constructible]
        public Apple(int amount)
            : base(amount, 0x9D0)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Apple(Serial serial)
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
