using Server.Engines.Craft;

namespace Server.Items
{
    public class DeerMask : BaseHat, IRepairable
    {
        public CraftSystem RepairSystem => DefTailoring.CraftSystem;
        public override int BasePhysicalResistance => 2;
        public override int BaseFireResistance => 6;
        public override int BaseColdResistance => 8;
        public override int BasePoisonResistance => 1;
        public override int BaseEnergyResistance => 7;

        public override int InitMinHits => 20;
        public override int InitMaxHits => 30;

        [Constructible]
        public DeerMask()
            : this(0)
        {
        }

        [Constructible]
        public DeerMask(int hue)
            : base(0x1547, hue)
        {
            Weight = 4.0;
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

        public DeerMask(Serial serial)
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
