namespace Server.Items
{
    public abstract class BaseWaist : BaseClothing
    {
        public BaseWaist(int itemID)
            : this(itemID, 0)
        {
        }

        public BaseWaist(int itemID, int hue)
            : base(itemID, Layer.Waist, hue)
        {
        }

        public BaseWaist(Serial serial)
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
