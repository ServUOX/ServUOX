using System;

namespace Server.Items
{
    // Based off a Spear
    [Flipable(0x904, 0x406D)]
    public class DualPointedSpear : BaseSpear
    {
        [Constructible]
        public DualPointedSpear()
            : base(0x904)
        {
            //Weight = 7.0;
        }

        public DualPointedSpear(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.DoubleStrike;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
        public override int AosStrengthReq => 50;
        public override int AosMinDamage => 11;
        public override int AosMaxDamage => 14;
        public override int AosSpeed => 42;
        public override float MlSpeed => 2.25f;
        public override int OldStrengthReq => 30;
        public override int OldMinDamage => 2;
        public override int OldMaxDamage => 36;
        public override int OldSpeed => 46;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 80;

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