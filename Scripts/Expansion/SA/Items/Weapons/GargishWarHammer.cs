using System;

namespace Server.Items
{
    //Based Off War Hammer
    [Flipable(0x48C0, 0x481)]
    public class GargishWarHammer : BaseBashing
    {
        [Constructible]
        public GargishWarHammer()
            : base(0x48C0)
        {
            Weight = 10.0;
            Layer = Layer.TwoHanded;
        }

        public GargishWarHammer(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.WhirlwindAttack;
        public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
        public override int AosStrengthReq => 95;
        public override int AosMinDamage => 17;
        public override int AosMaxDamage => 20;
        public override int AosSpeed => 28;
        public override float MlSpeed => 3.75f;
        public override int OldStrengthReq => 40;
        public override int OldMinDamage => 8;
        public override int OldMaxDamage => 36;
        public override int OldSpeed => 31;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 110;
        public override WeaponAnimation DefAnimation => WeaponAnimation.Bash2H;
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