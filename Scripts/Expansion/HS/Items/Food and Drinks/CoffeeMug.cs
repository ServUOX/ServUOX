namespace Server.Items
{
    public class CoffeeMug : CeramicMug
    {
        [Constructible]
        public CoffeeMug()
            : base(BeverageType.Coffee)
        {
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Quantity > 0 && Content == BeverageType.Coffee)
            {
                list.Add(1049515, "#1155737"); // a mug of Coffee
            }
            else
            {
                base.AddNameProperty(list);
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Quantity > 0 && Content == BeverageType.Coffee)
            {
                from.PublicOverheadMessage(Network.MessageType.Regular, 0x3B2, 1155739); // *You sip from the mug*
                Pour_OnTarget(from, from);
            }
            else
            {
                base.OnDoubleClick(from);
            }
        }

        public CoffeeMug(Serial serial)
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
