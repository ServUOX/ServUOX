namespace Server.Items
{
    public class TableWithPurpleClothAddon : BaseAddon
    {
        [Constructible]
        public TableWithPurpleClothAddon()
            : base()
        {
            AddComponent(new LocalizedAddonComponent(0x118B, 1076275), 0, 0, 0);
        }

        public TableWithPurpleClothAddon(Serial serial)
            : base(serial)
        {
        }

        public override BaseAddonDeed Deed => new TableWithPurpleClothDeed();
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }
    }

    public class TableWithPurpleClothDeed : BaseAddonDeed
    {
        [Constructible]
        public TableWithPurpleClothDeed()
            : base()
        {
            LootType = LootType.Blessed;
        }

        public TableWithPurpleClothDeed(Serial serial)
            : base(serial)
        {
        }

        public override BaseAddon Addon => new TableWithPurpleClothAddon();
        public override int LabelNumber => 1076275;// Table With A Purple Tablecloth
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }
    }
}
