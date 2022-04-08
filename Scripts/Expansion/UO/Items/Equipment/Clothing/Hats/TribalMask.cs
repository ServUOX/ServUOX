using Server.Engines.Craft;

namespace Server.Items
{
    public class TribalMask : BaseHat, IRepairable
    {
        public CraftSystem RepairSystem => DefTailoring.CraftSystem;
        public override int BasePhysicalResistance => 3;
        public override int BaseFireResistance => 0;
        public override int BaseColdResistance => 6;
        public override int BasePoisonResistance => 10;
        public override int BaseEnergyResistance => 5;

        public override int InitMinHits => 20;
        public override int InitMaxHits => 30;

        [Constructible]
        public TribalMask()
            : this(0)
        {
        }

        [Constructible]
        public TribalMask(int hue)
            : base(0x154B, hue)
        {
            Weight = 2.0;
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

        public TribalMask(Serial serial)
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
