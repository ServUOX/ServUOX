namespace Server.Engines.XmlSpawner2
{
    public class CustomParagon : XmlParagon
    {
        // string that is displayed on the xmlspawner when this is attached
        public override string OnIdentify(Mobile from)
        {
            return string.Format("Custom {0}", base.OnIdentify(from));
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

        public CustomParagon(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public CustomParagon()
            : base()
        {
        }
    }
}
