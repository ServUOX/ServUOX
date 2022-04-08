namespace Server.Items
{
    public class WondrousFish : BaseMagicFish
    {
        [Constructible]
        public WondrousFish()
            : base(86)
        {
        }

        public WondrousFish(Serial serial)
            : base(serial)
        {
        }

        public override int Bonus => 5;
        public override StatType Type => StatType.Dex;
        public override int LabelNumber => 1041074;// wondrous fish
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
