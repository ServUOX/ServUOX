namespace Server.Items
{
    public class BasketOfGreenTeaMug : CeramicMug
    {
        [Constructible]
        public BasketOfGreenTeaMug()
            : base(BeverageType.GreenTea)
        {
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Quantity > 0 && Content == BeverageType.GreenTea)
            {
                list.Add(1049515, "#1030315"); // a mug of Basket of Green Tea
            }
            else
            {
                base.AddNameProperty(list);
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Quantity > 0 && Content == BeverageType.GreenTea)
            {
                from.PublicOverheadMessage(Network.MessageType.Regular, 0x3B2, 1155739); // *You sip from the mug*
                Pour_OnTarget(from, from);
            }
            else
            {
                base.OnDoubleClick(from);
            }
        }

        public BasketOfGreenTeaMug(Serial serial)
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
