using Server.Engines.Craft;

namespace Server.Items
{
    public abstract class BaseBracelet : BaseJewel, IRepairable
    {
        public CraftSystem RepairSystem => DefTinkering.CraftSystem;
        public BaseBracelet(int itemID)
            : base(itemID, Layer.Bracelet)
        {
            Weight = 1.0;
        }

        public BaseBracelet(Serial serial)
            : base(serial)
        {
        }

        public override int BaseGemTypeNumber => 1044221;// star sapphire bracelet
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
