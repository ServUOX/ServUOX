namespace Server.Items
{
    [Flipable(0x1f7b, 0x1f7c)]
    public class Doublet : BaseMiddleTorso
    {
        [Constructible]
        public Doublet()
            : this(0)
        {
        }

        [Constructible]
        public Doublet(int hue)
            : base(0x1F7B, hue)
        {
            Weight = 2.0;
        }

        public Doublet(Serial serial)
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
