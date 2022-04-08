namespace Server.Items
{
    [Flipable(0x44D5, 0x44D9)]
    public class SmallGrandfatherClock : ClockTime
    {
        public override int LabelNumber => 1149901;  // Small Grandfather Clock

        [Constructible]
        public SmallGrandfatherClock()
            : base(0x44D5)
        {
        }

        public SmallGrandfatherClock(Serial serial)
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
