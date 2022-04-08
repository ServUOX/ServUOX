namespace Server.Items
{
    class WaterBarrel : BaseWaterContainer, IChopable
    {
        private static readonly int EmptyID = 0xE77;
        private static readonly int FilledID = 0x154D;

        [Constructible]
        public WaterBarrel()
            : this(false)
        {
        }

        [Constructible]
        public WaterBarrel(bool filled)
            : base(filled ? FilledID : EmptyID, filled)
        {
        }

        public WaterBarrel(Serial serial)
            : base(serial)
        {
        }

        public void OnChop(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), 2) && !IsLockedDown && !IsSecure && (RootParent == null || RootParent == from))
            {
                Effects.PlaySound(GetWorldLocation(), Map, 0x3B3);
                from.SendLocalizedMessage(500461); // You destroy the item.

                Delete();
            }
        }

        public override int LabelNumber => 1025453;/* water barrel */
        public override int voidItem_ID => EmptyID;
        public override int fullItem_ID => FilledID;
        public override int MaxQuantity => 100;

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
