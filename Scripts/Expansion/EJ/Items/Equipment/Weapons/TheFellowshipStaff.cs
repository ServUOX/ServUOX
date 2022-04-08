namespace Server.Items
{
    [Flipable(0xA343, 0xA344)]
    public class TheFellowshipStaff : BaseStaff
    {
        public override int LabelNumber => 1159034;  // The Fellowship Staff

        public static TheFellowshipStaff InstanceTram { get; set; }
        public static TheFellowshipStaff InstanceFel { get; set; }

        [Constructible]
        public TheFellowshipStaff()
            : base(0xA343)
        {
            Hue = 2721;
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.Block;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ForceOfNature;
        public override int AosStrengthReq => 20;
        public override int AosMinDamage => 15;
        public override int AosMaxDamage => 18;
        public override float MlSpeed => 3.25f;

        public override int InitMinHits => 30;
        public override int InitMaxHits => 60;

        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct)
        {
            fire = cold = pois = nrgy = chaos = direct = 0;
            phys = 100;
        }

        public TheFellowshipStaff(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();

            if (Map == Map.Trammel)
            {
                InstanceTram = this;
            }

            if (Map == Map.Felucca)
            {
                InstanceFel = this;
            }
        }
    }
}
