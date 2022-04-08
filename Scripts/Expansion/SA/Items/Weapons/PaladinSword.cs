using System;

namespace Server.Items
{
    [Flipable(0x26CE, 0x26CF)]
    public class PaladinSword : BaseSword
    {
        [Constructible]
        public PaladinSword()
            : base(0x26CE)
        {
            Weight = 6.0;
        }

        public PaladinSword(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.WhirlwindAttack;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
        public override int AosStrengthReq => 85;
        public override int AosMinDamage => 20;
        public override int AosMaxDamage => 24;
        public override float MlSpeed => 5.0f;
        public override int InitMinHits => 36;
        public override int InitMaxHits => 48;
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