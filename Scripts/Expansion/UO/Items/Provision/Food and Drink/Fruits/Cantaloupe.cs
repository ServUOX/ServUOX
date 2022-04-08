
namespace Server.Items
{
    [Flipable(0xC79, 0xC7A)]
    public class Cantaloupe : Food
    {
        [Constructible]
        public Cantaloupe()
            : this(1)
        {
        }

        [Constructible]
        public Cantaloupe(int amount)
            : base(amount, 0xc79)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Cantaloupe(Serial serial)
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
