using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefBlacksmithy), typeof(DualShortAxes))]
    [Flipable(0x13FB, 0x13FA)]
    public class LargeBattleAxe : BaseAxe
    {
        [Constructible]
        public LargeBattleAxe()
            : base(0x13FB)
        {
            Weight = 6.0;
        }

        public LargeBattleAxe(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.WhirlwindAttack;
        public override WeaponAbility SecondaryAbility => WeaponAbility.BleedAttack;
        public override int AosStrengthReq => 80;
        public override int AosMinDamage => 17;
        public override int AosMaxDamage => 20;
        public override int AosSpeed => 29;
        public override float MlSpeed => 3.75f;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 70;
        public override int OldStrengthReq => 40;
        public override int OldMinDamage => 6;
        public override int OldMaxDamage => 38;
        public override int OldSpeed => 30;
        public override int OldInitMinHits => 31;
        public override int OldInitMaxHits => 110;
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
