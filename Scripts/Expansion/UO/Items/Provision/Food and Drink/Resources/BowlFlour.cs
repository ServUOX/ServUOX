using Server.Targeting;

namespace Server.Items
{
    public class BowlFlour : Item
    {
        [Constructible]
        public BowlFlour()
            : base(0xa1e)
        {
            Weight = 1.0;
        }

        public BowlFlour(Serial serial)
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
