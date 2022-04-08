using System;

namespace Server.Items
{
    //Based Off Bardiche
    [Flipable(0x48B4, 0x48B5)]
    public class GargishBardiche : BasePoleArm
    {
        [Constructible]
        public GargishBardiche()
            : base(0x48B4)
        {
            Weight = 7.0;
        }

        public GargishBardiche(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Dismount;
        public override int AosStrengthReq => 45;
        public override int AosMinDamage => 17;
        public override int AosMaxDamage => 20;
        public override int AosSpeed => 28;
        public override float MlSpeed => 3.75f;
        public override int OldStrengthReq => 40;
        public override int OldMinDamage => 5;
        public override int OldMaxDamage => 43;
        public override int OldSpeed => 26;
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