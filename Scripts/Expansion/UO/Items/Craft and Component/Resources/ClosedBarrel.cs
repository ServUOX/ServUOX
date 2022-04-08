namespace Server.Items
{
    class ClosedBarrel : TrapableContainer
    {
        [Constructible]
        public ClosedBarrel()
            : base(0x0FAE)
        {
        }

        public ClosedBarrel(Serial serial)
            : base(serial)
        {
        }

        public override int DefaultGumpID => 0x3e;
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
