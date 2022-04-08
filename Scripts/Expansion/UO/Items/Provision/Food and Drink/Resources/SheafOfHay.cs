namespace Server.Items
{
    public class SheafOfHay : Item
    {
        [Constructible]
        public SheafOfHay()
            : base(0xF36)
        {
            Weight = 10.0;
        }

        public SheafOfHay(Serial serial)
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
