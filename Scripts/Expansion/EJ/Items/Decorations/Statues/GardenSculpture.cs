namespace Server.Items
{
    [Flipable(0xA565, 0xA566)]
    public class GardenSculpture : Item, IDyable
    {
        public override int LabelNumber => 1159464;  // Decorative Garden Sculpture

        [Constructible]
        public GardenSculpture()
            : base(0xA565)
        {
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
            {
                return false;
            }

            Hue = sender.DyedHue;

            return true;
        }

        public GardenSculpture(Serial serial)
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
