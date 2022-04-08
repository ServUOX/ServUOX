namespace Server.Items
{
    public class SkullCap : BaseHat
    {
        public override int BasePhysicalResistance => 0;
        public override int BaseFireResistance => 3;
        public override int BaseColdResistance => 5;
        public override int BasePoisonResistance => 8;
        public override int BaseEnergyResistance => 8;

        public override int InitMinHits => (Core.ML ? 14 : 7);
        public override int InitMaxHits => (Core.ML ? 28 : 12);

        [Constructible]
        public SkullCap()
            : this(0)
        {
        }

        [Constructible]
        public SkullCap(int hue)
            : base(0x1544, hue)
        {
            Weight = 1.0;
        }

        public SkullCap(Serial serial)
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
