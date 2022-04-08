using Server.Targeting;

namespace Server.Items
{
    public class WoodenBowlOfCarrots : Food
    {
        [Constructible]
        public WoodenBowlOfCarrots()
            : base(0x15F9)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 2;
        }

        public WoodenBowlOfCarrots(Serial serial)
            : base(serial)
        {
        }

        public override bool Eat(Mobile from)
        {
            if (!base.Eat(from))
            {
                return false;
            }

            from.AddToBackpack(new EmptyWoodenBowl());
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
