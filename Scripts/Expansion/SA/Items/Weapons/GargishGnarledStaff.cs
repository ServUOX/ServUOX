using System;

namespace Server.Items
{
    //Based Off Gnarled Staff
    [Flipable(0x48B8, 0x48B9)]
    public class GargishGnarledStaff : BaseStaff
    {
        [Constructible]
        public GargishGnarledStaff()
            : base(0x48B8)
        {
            Weight = 3.0;
        }

        public GargishGnarledStaff(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ConcussionBlow;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ForceOfNature;
        public override int AosStrengthReq => 20;
        public override int AosMinDamage => 15;
        public override int AosMaxDamage => 18;
        public override int AosSpeed => 33;
        public override float MlSpeed => 3.25f;
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