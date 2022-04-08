using Server.Engines.Harvest;

namespace Server.Items
{
    [Flipable(0xE86, 0xE85)]
    public class Pickaxe : BaseAxe, IUsesRemaining, IHarvestTool
    {
        [Constructible]
        public Pickaxe()
            : base(0xE86)
        {
            Weight = 11.0;
            UsesRemaining = 50;
            ShowUsesRemaining = true;
        }

        public Pickaxe(Serial serial)
            : base(serial)
        {
        }

        public override HarvestSystem HarvestSystem => Mining.System;
        public override WeaponAbility PrimaryAbility => WeaponAbility.DoubleStrike;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
        public override int AosStrengthReq => 50;
        public override int AosMinDamage => 12;
        public override int AosMaxDamage => 16;
        public override int AosSpeed => 35;
        public override float MlSpeed => 3.00f;
        public override int OldStrengthReq => 25;
        public override int OldMinDamage => 1;
        public override int OldMaxDamage => 15;
        public override int OldSpeed => 35;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 60;

        public override bool CanBeWornByGargoyles => true;

        public override WeaponAnimation DefAnimation => WeaponAnimation.Slash1H;
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
