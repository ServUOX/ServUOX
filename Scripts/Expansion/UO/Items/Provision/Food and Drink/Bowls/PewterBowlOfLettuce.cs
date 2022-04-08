using Server.Targeting;

namespace Server.Items
{
    public class PewterBowlOfLettuce : Food
    {
        [Constructible]
        public PewterBowlOfLettuce()
            : base(0x1600)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 2;
        }

        public PewterBowlOfLettuce(Serial serial)
            : base(serial)
        {
        }

        public override bool Eat(Mobile from)
        {
            if (!base.Eat(from))
            {
                return false;
            }

            from.AddToBackpack(new EmptyPewterBowl());
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
