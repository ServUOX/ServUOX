using System;

namespace Server.Items
{
    //Based off Axe
    [Flipable(0x48B2, 0x48B3)]
    public class GargishAxe : BaseAxe
    {
        [Constructible]
        public GargishAxe()
            : base(0x48B2)
        {
            Weight = 4.0;
        }

        public GargishAxe(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.CrushingBlow;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Dismount;
        public override int AosStrengthReq => 35;
        public override int AosMinDamage => 14;
        public override int AosMaxDamage => 17;
        public override int AosSpeed => 37;
        public override float MlSpeed => 3.00f;
        public override int OldStrengthReq => 35;
        public override int OldMinDamage => 6;
        public override int OldMaxDamage => 33;
        public override int OldSpeed => 37;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 110;
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