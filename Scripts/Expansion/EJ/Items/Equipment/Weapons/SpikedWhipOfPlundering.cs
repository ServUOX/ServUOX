namespace Server.Items
{
    public class SpikedWhipOfPlundering : SpikedWhip
    {
        public override int LabelNumber => 1158925;  // Spiked Whip of Plundering

        [Constructible]
        public SpikedWhipOfPlundering()
        {
            ExtendedWeaponAttributes.HitExplosion = 15;
            WeaponAttributes.HitLeechMana = 81;
            Attributes.SpellChanneling = 1;
            Attributes.Luck = 100;
            Attributes.WeaponDamage = 70;
        }

        public SpikedWhipOfPlundering(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
