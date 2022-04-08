namespace Server.Items
{
    [Flipable(0x48D4, 0x48D8)]
    public class WhiteGrandfatherClock : ClockTime
    {
        public override int LabelNumber => 1149903;  // White Grandfather Clock

        [Constructible]
        public WhiteGrandfatherClock()
            : base(0x48D4)
        {
        }

        public WhiteGrandfatherClock(Serial serial)
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
