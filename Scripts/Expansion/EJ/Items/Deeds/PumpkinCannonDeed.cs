namespace Server.Items
{
    public class PumpkinDeed : ShipCannonDeed
    {
        public override CannonPower CannonType => CannonPower.Pumpkin;
        public override int LabelNumber => 1159232;  // Pumpkin Cannon

        [Constructible]
        public PumpkinDeed()
        {
            Hue = 1192;
        }

        public PumpkinDeed(Serial serial)
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
