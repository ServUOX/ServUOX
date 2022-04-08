using System;

namespace Server.Items
{
    // Based off a GnarledStaff
    [Flipable(0x906, 0x406F)]
    public class SerpentStoneStaff : BaseStaff
    {
        [Constructible]
        public SerpentStoneStaff()
            : base(0x906)
        {
            //Weight = 3.0;
        }

        public SerpentStoneStaff(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.CrushingBlow;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Dismount;
        public override int AosStrengthReq => 35;
        public override int AosMinDamage => 16;
        public override int AosMaxDamage => 19;
        public override int AosSpeed => 33;
        public override float MlSpeed => 3.50f;
        public override int OldStrengthReq => 20;
        public override int OldMinDamage => 10;
        public override int OldMaxDamage => 30;
        public override int OldSpeed => 33;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 50;

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