using System;

namespace Server.Items
{
    //Based Off War Fork
    [Flipable(0x48BE, 0x48BF)]
    public class GargishWarFork : BaseSpear
    {
        [Constructible]
        public GargishWarFork()
            : base(0x48BE)
        {
            Weight = 9.0;
        }

        public GargishWarFork(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.BleedAttack;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
        public override int AosStrengthReq => 45;
        public override int AosMinDamage => 10;
        public override int AosMaxDamage => 14;
        public override int AosSpeed => 43;
        public override float MlSpeed => 2.50f;
        public override int OldStrengthReq => 35;
        public override int OldMinDamage => 4;
        public override int OldMaxDamage => 32;
        public override int OldSpeed => 45;
        public override int DefHitSound => 0x236;
        public override int DefMissSound => 0x238;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 110;
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