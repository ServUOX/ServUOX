namespace Server.Items
{
    public class SwordBelt : BaseWaist, IDyable
    {
        public override int LabelNumber => 1126021;  // sword belt

        [Constructible]
        public SwordBelt()
            : base(0xA40D)
        {
            Weight = 3.0;
            Layer = Layer.Waist;
        }

        public SwordBelt(Serial serial)
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
