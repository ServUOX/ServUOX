namespace Server.Items
{
    public class EnhancedBandage : Bandage, ICommodity
    {
        [Constructible]
        public EnhancedBandage()
            : this(1)
        {
        }

        [Constructible]
        public EnhancedBandage(int amount)
            : base(amount)
        {
            Hue = 0x8A5;
        }

        public EnhancedBandage(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public static int HealingBonus => 10;
        public override int LabelNumber => 1152441;// enhanced bandage
        public override bool Dye(Mobile from, DyeTub sender)
        {
            return false;
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);

            list.Add(1075216); // these bandages have been enhanced
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }
    }
}
