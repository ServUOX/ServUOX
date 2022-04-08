namespace Server.Items
{
    public class Hamburger : Food
    {
        public override int LabelNumber => 1125202;  // hamburger

        [Constructible]
        public Hamburger()
            : this(1)
        {
        }

        [Constructible]
        public Hamburger(int amount)
            : base(amount, 0xA0DA)
        {
            FillFactor = 2;
        }

        public Hamburger(Serial serial)
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
