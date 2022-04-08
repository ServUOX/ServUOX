using System;

namespace Server.Items
{
    //Based Off Pike
    [Flipable(0x48C8, 0x48C9)]
    public class GargishPike : BaseSpear
    {
        [Constructible]
        public GargishPike()
            : base(0x48C8)
        {
            Weight = 8.0;
        }

        public GargishPike(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
        public override WeaponAbility SecondaryAbility => WeaponAbility.InfectiousStrike;
        public override int AosStrengthReq => 50;
        public override int AosMinDamage => 14;
        public override int AosMaxDamage => 17;
        public override int AosSpeed => 37;
        public override float MlSpeed => 3.00f;
        public override int OldStrengthReq => 50;
        public override int OldMinDamage => 14;
        public override int OldMaxDamage => 16;
        public override int OldSpeed => 37;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 110;
        public override Race RequiredRace => Race.Gargoyle;
        public override bool CanBeWornByGargoyles => true;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}