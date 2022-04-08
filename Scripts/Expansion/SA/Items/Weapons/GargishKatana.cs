using System;

namespace Server.Items
{
    //Based Off Katana
    [Flipable(0x48BA, 0x48BB)]
    public class GargishKatana : BaseSword
    {
        [Constructible]
        public GargishKatana()
            : base(0x48BA)
        {
            Weight = 6.0;
        }

        public GargishKatana(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.DoubleStrike;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ArmorIgnore;
        public override int AosStrengthReq => 25;
        public override int AosMinDamage => 10;
        public override int AosMaxDamage => 14;
        public override int AosSpeed => 46;
        public override float MlSpeed => 2.50f;
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