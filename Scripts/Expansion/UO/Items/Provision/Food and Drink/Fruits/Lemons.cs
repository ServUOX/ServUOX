
namespace Server.Items
{
    public class Lemons : Food
    {
        [Constructible]
        public Lemons()
            : this(1)
        {
        }

        [Constructible]
        public Lemons(int amount)
            : base(amount, 0x1729)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Lemons(Serial serial)
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
