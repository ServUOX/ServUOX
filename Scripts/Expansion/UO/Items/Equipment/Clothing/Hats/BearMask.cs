using Server.Engines.Craft;

namespace Server.Items
{
    public class BearMask : BaseHat, IRepairable
    {
        public CraftSystem RepairSystem => DefTailoring.CraftSystem;
        public override int BasePhysicalResistance => 5;
        public override int BaseFireResistance => 3;
        public override int BaseColdResistance => 8;
        public override int BasePoisonResistance => 4;
        public override int BaseEnergyResistance => 4;

        public override int InitMinHits => 20;
        public override int InitMaxHits => 30;

        [Constructible]
        public BearMask()
            : this(0)
        {
        }

        [Constructible]
        public BearMask(int hue)
            : base(0x1545, hue)
        {
            Weight = 5.0;
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

        public BearMask(Serial serial)
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
