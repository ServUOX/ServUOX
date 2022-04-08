
namespace Server.Items
{
    [Flipable(0x1721, 0x1722)]
    public class Bananas : Food
    {
        [Constructible]
        public Bananas()
            : this(1)
        {
        }

        [Constructible]
        public Bananas(int amount)
            : base(amount, 0x1721)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Bananas(Serial serial)
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
