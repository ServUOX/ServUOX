namespace Server.Items
{
    public class WaterWheelAddon : BaseAddon, IWaterSource
    {
        [Constructible]
        public WaterWheelAddon(DirectionType type)
        {
            switch (type)
            {
                case DirectionType.South:
                    AddComponent(new LocalizedAddonComponent(0xA0F8, 1125222), 0, 0, 0);
                    break;
                case DirectionType.East:
                    AddComponent(new LocalizedAddonComponent(0xA0EE, 1125222), 0, 0, 0);
                    break;
            }
        }

        public int Quantity
        {
            get => 500;
            set { }
        }

        public WaterWheelAddon(Serial serial)
            : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem { get; set; }

        public override BaseAddonDeed Deed
        {
            get
            {
                WaterWheelDeed deed = new WaterWheelDeed
                {
                    IsRewardItem = IsRewardItem
                };

                return deed;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            writer.Write(IsRewardItem);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            IsRewardItem = reader.ReadBool();
        }
    }
}
