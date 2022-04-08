namespace Server.Items
{
    public class Cake : Food
    {
        [Constructible]
        public Cake()
            : base(0x9E9)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 10;
        }

        public Cake(Serial serial)
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
