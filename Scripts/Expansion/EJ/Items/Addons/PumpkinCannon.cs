using Server.Multis;

namespace Server.Items
{
    public class PumpkinCannon : BaseShipCannon
    {
        public override int LabelNumber => 1023691;  // cannon

        public override int Range => 10;
        public override ShipCannonDeed GetDeed => new PumpkinDeed();
        public override CannonPower Power => CannonPower.Pumpkin;

        public PumpkinCannon(BaseGalleon g)
            : base(g)
        {
        }

        public PumpkinCannon(Serial serial)
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
