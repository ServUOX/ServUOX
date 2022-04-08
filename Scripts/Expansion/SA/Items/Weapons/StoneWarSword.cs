using System;

namespace Server.Items
{
    // Based off a VikingSword
    [Flipable(0x900, 0x4071)]
    public class StoneWarSword : BaseSword
    {
        [Constructible]
        public StoneWarSword()
            : base(0x900)
        {
            //Weight = 6.0;
        }

        public StoneWarSword(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ArmorIgnore;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ParalyzingBlow;
        public override int AosStrengthReq => 40;
        public override int AosMinDamage => 15;
        public override int AosMaxDamage => 19;
        public override int AosSpeed => 28;
        public override float MlSpeed => 3.75f;
        public override int OldStrengthReq => 40;
        public override int OldMinDamage => 6;
        public override int OldMaxDamage => 34;
        public override int OldSpeed => 30;
        public override int DefHitSound => 0x237;
        public override int DefMissSound => 0x23A;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 100;

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