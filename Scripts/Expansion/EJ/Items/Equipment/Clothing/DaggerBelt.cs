namespace Server.Items
{
    public class DaggerBelt : BaseWaist, IDyable
    {
        public override int LabelNumber => 1159210;  // dagger belt

        [Constructible]
        public DaggerBelt()
            : base(0xA40E)
        {
            Weight = 3.0;
            Layer = Layer.Waist;
        }

        public DaggerBelt(Serial serial)
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
