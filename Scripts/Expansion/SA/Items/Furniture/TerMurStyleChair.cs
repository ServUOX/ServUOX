namespace Server.Items
{
    [Furniture]
    [Flipable(0x4023, 0x4024)]
    public class TerMurStyleChair : CraftableFurniture
    {
        public override int LabelNumber => 1095291;  // Ter-Mur style chair

        [Constructible]
        public TerMurStyleChair()
            : base(0x4023)
        {
            Weight = 20.0;
        }

        public TerMurStyleChair(Serial serial)
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
