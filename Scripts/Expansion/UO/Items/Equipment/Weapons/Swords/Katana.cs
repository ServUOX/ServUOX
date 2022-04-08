using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefBlacksmithy), typeof(GargishKatana))]
    [Flipable(0x13FF, 0x13FE)]
    public class Katana : BaseSword
    {
        [Constructible]
        public Katana()
            : base(0x13FF)
        {
            Weight = 6.0;
        }

        public Katana(Serial serial)
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
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
