namespace Server.Items
{
    [Flipable(0x44DD, 0x44E1)]
    public class LargeGrandfatherClock : ClockTime
    {
        public override int LabelNumber => 1149902;  // Large Grandfather Clock

        [Constructible]
        public LargeGrandfatherClock()
            : base(0x44DD)
        {
        }

        public LargeGrandfatherClock(Serial serial)
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
