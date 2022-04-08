using Server.Targeting;

namespace Server.Items
{
    public class BowlOfRotwormStew : Food
    {
        public override int LabelNumber => 1031706;  // bowl of rotworm stew

        [Constructible]
        public BowlOfRotwormStew()
            : base(0x2DBA)
        {
            Stackable = false;
            Weight = 2.0;
            FillFactor = 1;
        }

        public BowlOfRotwormStew(Serial serial)
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
