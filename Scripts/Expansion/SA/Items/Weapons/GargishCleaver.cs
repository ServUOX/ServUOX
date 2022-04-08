using System;

namespace Server.Items
{
    //Based off Cleaver
    [Flipable(0x48AE, 0x48AF)]
    public class GargishCleaver : BaseKnife
    {
        [Constructible]
        public GargishCleaver()
            : base(0x48AE)
        {
            Weight = 2.0;
        }

        public GargishCleaver(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.BleedAttack;
        public override WeaponAbility SecondaryAbility => WeaponAbility.InfectiousStrike;
        public override int AosStrengthReq => 10;
        public override int AosMinDamage => 10;
        public override int AosMaxDamage => 14;
        public override int AosSpeed => 46;
        public override float MlSpeed => 2.50f;
        public override int OldStrengthReq => 10;
        public override int OldMinDamage => 2;
        public override int OldMaxDamage => 13;
        public override int OldSpeed => 40;
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