using System;

namespace Server.Items
{
    // Based off a Halberd
    [Flipable(0x908, 0x4075)]
    public class GargishTalwar : BaseSword
    {
        [Constructible]
        public GargishTalwar()
            : base(0x908)
        {
            //Weight = 16.0;
        }

        public GargishTalwar(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.WhirlwindAttack;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Dismount;
        public override int AosStrengthReq => 40;
        public override int AosMinDamage => 16;
        public override int AosMaxDamage => 19;
        public override int AosSpeed => 25;
        public override float MlSpeed => 3.50f;
        public override int OldStrengthReq => 45;
        public override int OldMinDamage => 5;
        public override int OldMaxDamage => 49;
        public override int OldSpeed => 25;
        public override int DefHitSound => 0x237;
        public override int DefMissSound => 0x238;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 80;

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