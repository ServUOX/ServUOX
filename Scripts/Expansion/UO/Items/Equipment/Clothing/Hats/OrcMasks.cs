using Server.Engines.Craft;

namespace Server.Items
{
    public class OrcishKinMask : BaseHat, IRepairable
    {
        public CraftSystem RepairSystem => DefTailoring.CraftSystem;
        public override int BasePhysicalResistance => 1;
        public override int BaseFireResistance => 1;
        public override int BaseColdResistance => 7;
        public override int BasePoisonResistance => 7;
        public override int BaseEnergyResistance => 8;

        public override int InitMinHits => 20;
        public override int InitMaxHits => 30;

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

        public override string DefaultName => "a mask of orcish kin";

        [Constructible]
        public OrcishKinMask()
            : this(0x8A4)
        {
        }

        [Constructible]
        public OrcishKinMask(int hue)
            : base(0x141B, hue)
        {
            Weight = 2.0;
        }

        public override bool CanEquip(Mobile m)
        {
            if (!base.CanEquip(m))
            {
                return false;
            }

            if (m.BodyMod == 183 || m.BodyMod == 184)
            {
                m.SendLocalizedMessage(1061629); // You can't do that while wearing savage kin paint.
                return false;
            }

            return true;
        }

        public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile)
            {
                Misc.Titles.AwardKarma((Mobile)parent, -20, true);
            }
        }

        public OrcishKinMask(Serial serial)
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

    public class OrcMask : BaseHat
    {
        public override int BasePhysicalResistance => 1;
        public override int BaseFireResistance => 1;
        public override int BaseColdResistance => 7;
        public override int BasePoisonResistance => 7;
        public override int BaseEnergyResistance => 8;

        public override int InitMinHits => 20;
        public override int InitMaxHits => 30;

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

        [Constructible]
        public OrcMask()
            : this(0x8A4)
        {
        }

        [Constructible]
        public OrcMask(int hue)
            : base(0x141B, hue)
        {
            Weight = 2.0;
        }

        public OrcMask(Serial serial)
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
