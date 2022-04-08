using Server.Engines.Craft;

namespace Server.Items
{
    public abstract class BaseRing : BaseJewel, IRepairable
    {
        public CraftSystem RepairSystem => DefTinkering.CraftSystem;

        public BaseRing(int itemID)
            : base(itemID, Layer.Ring)
        {
            Weight = 1.0;
        }

        public BaseRing(Serial serial)
            : base(serial)
        {
        }

        public override int BaseGemTypeNumber => 1044176;// star sapphire ring
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(2); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 1)
            {
                if (Weight == .1)
                {
                    Weight = -1;
                }
            }
        }
    }
}
