using Server.Targeting;

namespace Server.Items
{
    public class WoodenBowlOfStew : Food
    {
        [Constructible]
        public WoodenBowlOfStew()
            : base(0x1604)
        {
            Stackable = false;
            Weight = 2.0;
            FillFactor = 2;
        }

        public WoodenBowlOfStew(Serial serial)
            : base(serial)
        {
        }

        public override bool Eat(Mobile from)
        {
            if (!base.Eat(from))
            {
                return false;
            }

            from.AddToBackpack(new EmptyWoodenTub());
            return true;
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
