using System;
using Server.Guilds;

namespace Server.Items
{
    // Based off an OrderShield
    [Flipable(0x422A, 0x422C)]
    public class GargishOrderShield : BaseShield
    {
        [Constructible]
        public GargishOrderShield()
            : base(0x422A)
        {
            if (!Core.AOS)
                LootType = LootType.Newbied;
            //Weight = 7.0;
        }

        public GargishOrderShield(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance => 1;
        public override int BaseFireResistance => 0;
        public override int BaseColdResistance => 0;
        public override int BasePoisonResistance => 0;
        public override int BaseEnergyResistance => 0;
        public override int InitMinHits => 100;
        public override int InitMaxHits => 125;
        public override int AosStrReq => 95;
        public override int ArmorBase => 30;
        public override bool CanBeWornByGargoyles => true;
        public override Race RequiredRace => Race.Gargoyle;
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); //version
        }

        public override bool OnEquip(Mobile from)
        {
            return Validate(from) && base.OnEquip(from);
        }

        public override void OnSingleClick(Mobile from)
        {
            if (Validate(Parent as Mobile))
                base.OnSingleClick(from);
        }

        public virtual bool Validate(Mobile m)
        {
            if (Core.AOS || m == null || !m.Player || m.AccessLevel != AccessLevel.Player)
                return true;

            Guild g = m.Guild as Guild;

            if (g == null || g.Type != GuildType.Order)
            {
                m.FixedEffect(0x3728, 10, 13);
                Delete();

                return false;
            }

            return true;
        }
    }
}