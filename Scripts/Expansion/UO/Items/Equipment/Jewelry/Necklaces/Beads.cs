namespace Server.Items
{
    public class Beads : BaseNecklace
    {
        [Constructible]
        public Beads()
            : base(0x108B)
        {
        }

        public Beads(Serial serial)
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
