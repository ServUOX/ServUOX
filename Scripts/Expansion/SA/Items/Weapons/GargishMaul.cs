using System;

namespace Server.Items
{
    [Flipable(0x48C2, 0x48C3)]
    public class GargishMaul : BaseBashing
    {
        [Constructible]
        public GargishMaul()
            : base(0x48C2)
        {
            Weight = 10.0;
        }

        public GargishMaul(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.DoubleStrike;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ConcussionBlow;
        public override int AosStrengthReq => 45;
        public override int AosMinDamage => 14;
        public override int AosMaxDamage => 18;
        public override int AosSpeed => 32;
        public override float MlSpeed => 3.50f;
        public override int OldStrengthReq => 20;
        public override int OldMinDamage => 10;
        public override int OldMaxDamage => 30;
        public override int OldSpeed => 30;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 70;
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
