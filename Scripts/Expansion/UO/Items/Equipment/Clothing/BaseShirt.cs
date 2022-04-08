namespace Server.Items
{
    public abstract class BaseShirt : BaseClothing
    {
        public BaseShirt(int itemID)
            : this(itemID, 0)
        {
        }

        public BaseShirt(int itemID, int hue)
            : base(itemID, Layer.Shirt, hue)
        {
        }

        public BaseShirt(Serial serial)
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
