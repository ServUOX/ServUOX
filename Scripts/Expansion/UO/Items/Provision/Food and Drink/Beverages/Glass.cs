namespace Server.Items
{
    [Flipable(0x1f81, 0x1f82, 0x1f83, 0x1f84)]
    public class Glass : Item
    {
        [Constructible]
        public Glass()
            : base(0x1f81)
        {
            Weight = 0.1;
        }

        public Glass(Serial serial)
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
