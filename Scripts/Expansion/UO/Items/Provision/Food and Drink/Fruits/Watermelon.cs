
namespace Server.Items
{
    public class Watermelon : Food
    {
        [Constructible]
        public Watermelon()
            : this(1)
        {
        }

        [Constructible]
        public Watermelon(int amount)
            : base(amount, 0xC5C)
        {
            Weight = 5.0;
            FillFactor = 5;
        }

        public Watermelon(Serial serial)
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
