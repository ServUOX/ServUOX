using System;

namespace Server.Items
{
    // Based off a Warfork
    [Flipable(0x907, 0x4076)]
    public class Shortblade : BaseSword
    {
        [Constructible]
        public Shortblade()
            : base(0x907)
        {
            //Weight = 9.0;
        }

        public Shortblade(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ArmorIgnore;
        public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
        public override int AosStrengthReq => 45;
        public override int AosMinDamage => 10;
        public override int AosMaxDamage => 13;
        public override int AosSpeed => 43;
        public override float MlSpeed => 2.25f;
        public override int OldStrengthReq => 35;
        public override int OldMinDamage => 4;
        public override int OldMaxDamage => 32;
        public override int OldSpeed => 45;
        public override int DefHitSound => 0x236;
        public override int DefMissSound => 0x238;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 110;
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