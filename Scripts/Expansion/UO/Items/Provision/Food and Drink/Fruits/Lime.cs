
namespace Server.Items
{
    public class Lime : Food
    {
        [Constructible]
        public Lime()
            : this(1)
        {
        }

        [Constructible]
        public Lime(int amount)
            : base(amount, 0x172a)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Lime(Serial serial)
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
