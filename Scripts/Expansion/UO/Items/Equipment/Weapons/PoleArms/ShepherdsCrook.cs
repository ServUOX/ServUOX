using Server.SkillHandlers;

namespace Server.Items
{
    [Flipable(0xE81, 0xE82)]
    public class ShepherdsCrook : BaseStaff
    {
        [Constructible]
        public ShepherdsCrook()
            : base(0xE81)
        {
            Weight = 4.0;
        }

        public ShepherdsCrook(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.CrushingBlow;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
        public override int AosStrengthReq => 20;
        public override int AosMinDamage => 13;
        public override int AosMaxDamage => 16;
        public override int AosSpeed => 40;
        public override float MlSpeed => 2.75f;
        public override int OldStrengthReq => 10;
        public override int OldMinDamage => 3;
        public override int OldMaxDamage => 12;
        public override int OldSpeed => 30;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 50;

        public override bool CanBeWornByGargoyles => true;

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

        public override void OnDoubleClick(Mobile from)
        {
            from.SendLocalizedMessage(502464); // Target the animal you wish to herd.
            from.Target = new HerdingTarget(this);
        }
    }
}
