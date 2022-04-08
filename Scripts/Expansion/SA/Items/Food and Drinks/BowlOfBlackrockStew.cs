namespace Server.Items
{
    public class BowlOfBlackrockStew : Food
    {
        public override int LabelNumber => 1115752;  // blackrock stew

        [Constructible]
        public BowlOfBlackrockStew()
            : base(0x2DBA)
        {
            Stackable = false;
            Weight = 2.0;
            FillFactor = 1;

            Hue = 1954;
        }

        public override bool Eat(Mobile from)
        {
            from.SendLocalizedMessage(1115751); // You don't want to eat this, it smells horrible.  It looks like food for some kind of demon.
            return false;
        }

        public BowlOfBlackrockStew(Serial serial)
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
