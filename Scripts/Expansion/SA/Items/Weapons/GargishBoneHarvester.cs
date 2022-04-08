using System;

namespace Server.Items
{
    //Based off Bone Harvester
    [Flipable(0x48C6, 0x48C7)]
    public class GargishBoneHarvester : BaseSword
    {
        [Constructible]
        public GargishBoneHarvester()
            : base(0x48C6)
        {
            Weight = 3.0;
        }

        public GargishBoneHarvester(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
        public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
        public override int AosStrengthReq => 25;
        public override int AosMinDamage => 12;
        public override int AosMaxDamage => 16;
        public override int AosSpeed => 36;
        public override float MlSpeed => 3.00f;
        public override int OldStrengthReq => 25;
        public override int OldMinDamage => 13;
        public override int OldMaxDamage => 15;
        public override int OldSpeed => 36;
        public override int DefHitSound => 0x23B;
        public override int DefMissSound => 0x23A;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 70;
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