using System;

namespace Server.Items
{
    public class SoulGlaive : BaseThrown
    {
        [Constructible]
        public SoulGlaive()
            : base(0x090A)
        {
            Weight = 8.0;
            Layer = Layer.OneHanded;
        }

        public SoulGlaive(Serial serial)
            : base(serial)
        {
        }

        public override int MinThrowRange => 8;

        public override WeaponAbility PrimaryAbility => WeaponAbility.ArmorIgnore;
        public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
        public override int AosStrengthReq => 60;
        public override int AosMinDamage => 16;
        public override int AosMaxDamage => 20;
        public override int AosSpeed => 25;
        public override float MlSpeed => 4.00f;
        public override int OldStrengthReq => 20;
        public override int OldMinDamage => 9;
        public override int OldMaxDamage => 41;
        public override int OldSpeed => 20;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 65;

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
