using System;

namespace Server.Items
{
    // Based off a Dagger
    [Flipable(0x902, 0x406A)]
    public class GargishDagger : BaseKnife
    {
        [Constructible]
        public GargishDagger()
            : base(0x902)
        {
            //Weight = 1.0;
        }

        public GargishDagger(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ShadowStrike;
        public override WeaponAbility SecondaryAbility => WeaponAbility.InfectiousStrike;
        public override int AosStrengthReq => 10;
        public override int AosMinDamage => 10;
        public override int AosMaxDamage => 12;
        public override int AosSpeed => 56;
        public override float MlSpeed => 2.00f;
        public override int OldStrengthReq => 1;
        public override int OldMinDamage => 3;
        public override int OldMaxDamage => 15;
        public override int OldSpeed => 55;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 40;
        public override SkillName DefSkill => SkillName.Fencing;
        public override WeaponType DefType => WeaponType.Piercing;
        public override WeaponAnimation DefAnimation => WeaponAnimation.Pierce1H;

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