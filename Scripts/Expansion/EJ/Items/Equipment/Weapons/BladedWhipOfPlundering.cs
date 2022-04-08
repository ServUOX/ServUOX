namespace Server.Items
{
    public class BladedWhipOfPlundering : BladedWhip
    {
        public override int LabelNumber => 1158924;  // Bladed Whip of Plundering

        [Constructible]
        public BladedWhipOfPlundering()
        {
            ExtendedWeaponAttributes.HitExplosion = 15;
            WeaponAttributes.HitLeechMana = 81;
            Attributes.SpellChanneling = 1;
            Attributes.Luck = 100;
            Attributes.WeaponDamage = 70;
        }

        public BladedWhipOfPlundering(Serial serial)
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
