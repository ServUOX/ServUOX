
namespace Server.Items
{
    [Flipable(0xC72, 0xC73)]
    public class Squash : Food
    {
        [Constructible]
        public Squash()
            : this(1)
        {
        }

        [Constructible]
        public Squash(int amount)
            : base(amount, 0xc72)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Squash(Serial serial)
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
