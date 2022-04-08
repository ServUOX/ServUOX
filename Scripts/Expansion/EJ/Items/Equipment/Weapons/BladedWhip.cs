using Server.Engines.Craft;

namespace Server.Items
{
    public class BladedWhip : BaseSword, IRepairable
    {
        public CraftSystem RepairSystem => DefTinkering.CraftSystem;
        public override int LabelNumber => 1125643;  // Bladed Whip

        [Constructible]
        public BladedWhip()
            : base(0xA28B)
        {
            Weight = 5.0;
        }

        public BladedWhip(Serial serial)
            : base(serial)
        {
        }

        public override bool CanBeWornByGargoyles => true;
        public override WeaponAbility PrimaryAbility => WeaponAbility.BleedAttack;
        public override WeaponAbility SecondaryAbility => WeaponAbility.WhirlwindAttack;
        public override int AosStrengthReq => 20;
        public override int AosMinDamage => 13;
        public override int AosMaxDamage => 17;
        public override float MlSpeed => 3.25f;
        public override int DefHitSound => 0x23B;
        public override int DefMissSound => 0x23A;
        public override int InitMinHits => 30;
        public override int InitMaxHits => 60;

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
