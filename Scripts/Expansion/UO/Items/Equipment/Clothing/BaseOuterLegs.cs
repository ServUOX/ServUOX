namespace Server.Items
{
    public abstract class BaseOuterLegs : BaseClothing
    {
        public BaseOuterLegs(int itemID)
            : this(itemID, 0)
        {
        }

        public BaseOuterLegs(int itemID, int hue)
            : base(itemID, Layer.OuterLegs, hue)
        {
        }

        public BaseOuterLegs(Serial serial)
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
