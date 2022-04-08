using System;

namespace Server.Items
{
    //Based Off Butcher Knife
    [Flipable(0x48B6, 0x48B7)]
    public class GargishButcherKnife : BaseKnife
    {
        [Constructible]
        public GargishButcherKnife()
            : base(0x48B6)
        {
            Weight = 1.0;
        }

        public GargishButcherKnife(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.InfectiousStrike;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
        public override int AosStrengthReq => 10;
        public override int AosMinDamage => 10;
        public override int AosMaxDamage => 13;
        public override int AosSpeed => 49;
        public override float MlSpeed => 2.25f;
        public override int OldStrengthReq => 5;
        public override int OldMinDamage => 2;
        public override int OldMaxDamage => 14;
        public override int OldSpeed => 40;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 40;
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
