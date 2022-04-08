using Server.Engines.Craft;

namespace Server.Items
{
    public class MagicWand : BaseBashing, IRepairable
    {
        public CraftSystem RepairSystem => DefCarpentry.CraftSystem;

        [Constructible]
        public MagicWand()
            : base(0xDF2)
        {
            Weight = 1.0;
        }

        public MagicWand(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.Dismount;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
        public override int AosStrengthReq => 5;
        public override int AosMinDamage => 9;
        public override int AosMaxDamage => 11;
        public override int AosSpeed => 40;
        public override float MlSpeed => 2.75f;
        public override int OldStrengthReq => 0;
        public override int OldMinDamage => 2;
        public override int OldMaxDamage => 6;
        public override int OldSpeed => 35;
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
