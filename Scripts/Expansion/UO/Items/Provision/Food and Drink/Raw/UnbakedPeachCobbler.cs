namespace Server.Items
{
    public class UnbakedPeachCobbler : CookableFood
    {
        public override int LabelNumber => 1041335;// unbaked peach cobbler

        [Constructible]
        public UnbakedPeachCobbler()
            : base(0x1042, 25)
        {
            Weight = 1.0;
        }

        public UnbakedPeachCobbler(Serial serial)
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

        public override Food Cook()
        {
            return new PeachCobbler();
        }
    }
}
