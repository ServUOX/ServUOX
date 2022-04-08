namespace Server.Items
{
    public class PulledPorkSandwich : Food
    {
        public override int LabelNumber => 1123352;  // Pulled Pork Sandwich

        [Constructible]
        public PulledPorkSandwich()
            : base(1, 0x99A0)
        {
            FillFactor = 3;
            Stackable = false;
        }

        public PulledPorkSandwich(Serial serial)
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
