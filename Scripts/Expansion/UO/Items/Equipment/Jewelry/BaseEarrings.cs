using Server.Engines.Craft;

namespace Server.Items
{
    public abstract class BaseEarrings : BaseJewel, IRepairable
    {
        public CraftSystem RepairSystem => DefTinkering.CraftSystem;
        public override int BaseGemTypeNumber => 1044203; // star sapphire earrings

        public BaseEarrings(int itemID)
            : base(itemID, Layer.Earrings)
        {
            Weight = 1.0;
        }

        public BaseEarrings(Serial serial)
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
