
namespace Server.Items
{

    public class Plum : Food
    {
        public override int LabelNumber => 1157208;  // plum

        [Constructible]
        public Plum()
            : this(1)
        {
        }

        [Constructible]
        public Plum(int amount)
            : base(amount, 0x9E86)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Plum(Serial serial)
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
