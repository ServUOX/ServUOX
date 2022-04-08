using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefBlacksmithy), typeof(DualShortAxes))]
    [Flipable(0xf4b, 0xf4c)]
    public class DoubleAxe : BaseAxe
    {
        [Constructible]
        public DoubleAxe()
            : base(0xF4B)
        {
            Weight = 8.0;
        }

        public DoubleAxe(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.DoubleStrike;
        public override WeaponAbility SecondaryAbility => WeaponAbility.WhirlwindAttack;
        public override int AosStrengthReq => 45;
        public override int AosMinDamage => 15;
        public override int AosMaxDamage => 18;
        public override int AosSpeed => 33;
        public override float MlSpeed => 3.25f;
        public override int OldStrengthReq => 45;
        public override int OldMinDamage => 5;
        public override int OldMaxDamage => 35;
        public override int OldSpeed => 37;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 110;
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
