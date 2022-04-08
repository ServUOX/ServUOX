using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefCarpentry), typeof(GargishGnarledStaff))]
    [Flipable(0x13F8, 0x13F9)]
    public class GnarledStaff : BaseStaff
    {
        [Constructible]
        public GnarledStaff()
            : base(0x13F8)
        {
            Weight = 3.0;
        }

        public GnarledStaff(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ConcussionBlow;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ForceOfNature;
        public override int AosStrengthReq => 20;
        public override int AosMinDamage => 15;
        public override int AosMaxDamage => 18;
        public override int AosSpeed => 33;
        public override float MlSpeed => 3.25f;
        public override int OldStrengthReq => 20;
        public override int OldMinDamage => 10;
        public override int OldMaxDamage => 30;
        public override int OldSpeed => 33;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 50;
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
