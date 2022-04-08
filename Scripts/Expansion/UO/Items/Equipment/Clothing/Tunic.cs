namespace Server.Items
{
    [Flipable(0x1fa1, 0x1fa2)]
    public class Tunic : BaseMiddleTorso
    {
        [Constructible]
        public Tunic()
            : this(0)
        {
        }

        [Constructible]
        public Tunic(int hue)
            : base(0x1FA1, hue)
        {
            Weight = 5.0;
        }

        public Tunic(Serial serial)
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
