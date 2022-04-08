
namespace Server.Items
{
    public class Peach : Food
    {
        [Constructible]
        public Peach()
            : this(1)
        {
        }

        [Constructible]
        public Peach(int amount)
            : base(amount, 0x9D2)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Peach(Serial serial)
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
