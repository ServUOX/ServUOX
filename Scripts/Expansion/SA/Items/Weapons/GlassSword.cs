using System;

namespace Server.Items
{
    // Based off a Katana
    [Flipable(0x90C, 0x4073)]
    public class GlassSword : BaseSword
    {
        [Constructible]
        public GlassSword()
            : base(0x90C)
        {
            Weight = 6.0;
        }

        public GlassSword(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.BleedAttack;
        public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
        public override int AosStrengthReq => 20;
        public override int AosMinDamage => 11;
        public override int AosMaxDamage => 15;
        public override int AosSpeed => 46;
        public override float MlSpeed => 2.75f;
        public override int OldStrengthReq => 10;
        public override int OldMinDamage => 5;
        public override int OldMaxDamage => 26;
        public override int OldSpeed => 58;
        public override int DefHitSound => 0x23B;
        public override int DefMissSound => 0x23A;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 90;

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