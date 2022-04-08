using Server.Engines.Craft;

namespace Server.Items
{
    public abstract class BaseNecklace : BaseJewel, IRepairable
    {
        public CraftSystem RepairSystem => DefTinkering.CraftSystem;
        public BaseNecklace(int itemID)
            : base(itemID, Layer.Neck)
        {
            Weight = 1.0;
        }

        public BaseNecklace(Serial serial)
            : base(serial)
        {
        }

        public override int BaseGemTypeNumber => 1044241;// star sapphire necklace
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
