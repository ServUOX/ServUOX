using System;

namespace Server.Items
{
    public class Boomerang : BaseThrown
    {
        [Constructible]
        public Boomerang()
            : base(0x8FF)
        {
            Weight = 4.0;
            Layer = Layer.OneHanded;
        }

        public Boomerang(Serial serial)
            : base(serial)
        {
        }

        public override int MinThrowRange => 4;

        public override WeaponAbility PrimaryAbility => WeaponAbility.MysticArc;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ConcussionBlow;
        /*
        Boomerang 0x8FF: MysticArc, ConcussionBlow
        Cyclone 2305/0x901: MovingShot, InfusedThrow
        Soul Glaive 2314/0x090A: ArmorIgnore, MortalStrike
        */
        public override int AosStrengthReq => 25;
        public override int AosMinDamage => 11;
        public override int AosMaxDamage => 15;
        public override int AosSpeed => 25;
        public override float MlSpeed => 2.75f;
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