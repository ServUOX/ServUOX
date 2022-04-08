using System;

namespace Server.Items
{
    // Based off a DoubleAxe
    [Flipable(0x8FD, 0x4068)]
    public class DualShortAxes : BaseAxe
    {
        [Constructible]
        public DualShortAxes()
            : base(0x8FD)
        {
            //Weight = 8.0;
        }

        public DualShortAxes(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.DoubleStrike;
        public override WeaponAbility SecondaryAbility => WeaponAbility.InfectiousStrike;
        public override int AosStrengthReq => 35;
        public override int AosMinDamage => 14;
        public override int AosMaxDamage => 17;
        public override int AosSpeed => 33;
        public override float MlSpeed => 3.00f;
        public override int OldStrengthReq => 45;
        public override int OldMinDamage => 5;
        public override int OldMaxDamage => 35;
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