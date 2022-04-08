using System;

namespace Server.Items
{
    public class Cyclone : BaseThrown
    {
        [Constructible]
        public Cyclone()
            : base(0x901)
        {
            Weight = 6.0;
            Layer = Layer.OneHanded;
        }

        public Cyclone(Serial serial)
            : base(serial)
        {
        }

        public override int MinThrowRange => 6;

        public override WeaponAbility PrimaryAbility => WeaponAbility.MovingShot;
        public override WeaponAbility SecondaryAbility => WeaponAbility.InfusedThrow;
        public override int AosStrengthReq => 40;
        public override int AosMinDamage => 13;
        public override int AosMaxDamage => 17;
        public override int AosSpeed => 25;
        public override float MlSpeed => 3.25f;
        public override int OldStrengthReq => 20;
        public override int OldMinDamage => 9;
        public override int OldMaxDamage => 41;
        public override int OldSpeed => 20;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 60;

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