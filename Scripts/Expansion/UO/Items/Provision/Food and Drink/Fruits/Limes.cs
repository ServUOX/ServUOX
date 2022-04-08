
namespace Server.Items
{
    public class Limes : Food
    {
        [Constructible]
        public Limes()
            : this(1)
        {
        }

        [Constructible]
        public Limes(int amount)
            : base(amount, 0x172B)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Limes(Serial serial)
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
