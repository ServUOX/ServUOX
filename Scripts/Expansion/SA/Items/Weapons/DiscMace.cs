using System;

namespace Server.Items
{
    // Based off a WarMace
    [Flipable(0x903, 0x406E)]
    public class DiscMace : BaseBashing
    {
        [Constructible]
        public DiscMace()
            : base(0x903)
        {
            //Weight = 17.0;
        }

        public DiscMace(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ArmorIgnore;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
        public override int AosStrengthReq => 45;
        public override int AosMinDamage => 11;
        public override int AosMaxDamage => 15;
        public override int AosSpeed => 26;
        public override float MlSpeed => 2.75f;
        public override int OldStrengthReq => 30;
        public override int OldMinDamage => 10;
        public override int OldMaxDamage => 30;
        public override int OldSpeed => 32;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 110;

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