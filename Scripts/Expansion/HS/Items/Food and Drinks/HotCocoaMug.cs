namespace Server.Items
{
    public class HotCocoaMug : CeramicMug
    {
        [Constructible]
        public HotCocoaMug()
            : base(BeverageType.HotCocoa)
        {
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Quantity > 0 && Content == BeverageType.HotCocoa)
            {
                list.Add(1049515, "#1155738"); // a mug of Hot Cocoa
            }
            else
            {
                base.AddNameProperty(list);
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Quantity > 0 && Content == BeverageType.HotCocoa)
            {
                from.PublicOverheadMessage(Network.MessageType.Regular, 0x3B2, 1155739); // *You sip from the mug*
                Pour_OnTarget(from, from);
            }
            else
            {
                base.OnDoubleClick(from);
            }
        }

        public HotCocoaMug(Serial serial)
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
