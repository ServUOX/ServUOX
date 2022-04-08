using System;

namespace Server.Items
{
    [Flipable(0xE87, 0xE88)]
    public class FNPitchfork : BaseSpear
    {
        public override bool IsArtifact => true;
        [Constructible]
        public FNPitchfork()
            : base(0xE87)
        {
            Weight = 11.0;
        }

        public FNPitchfork(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1113498;// Farmer Nash's Pitchfork
        public override WeaponAbility PrimaryAbility => WeaponAbility.BleedAttack;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Dismount;
        public override int AosStrengthReq => 50;
        public override int AosMinDamage => 13;
        public override int AosMaxDamage => 14;
        public override int AosSpeed => 43;
        public override float MlSpeed => 2.50f;
        public override int OldStrengthReq => 15;
        public override int OldMinDamage => 4;
        public override int OldMaxDamage => 16;
        public override int OldSpeed => 45;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 60;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 10.0)
                Weight = 11.0;
        }
    }
}