namespace Server.Items
{
    public class FerretFormTalisman : BaseFormTalisman
    {
        public override TalismanForm Form => TalismanForm.Ferret;

        [Constructible]
        public FerretFormTalisman()
            : base()
        {
        }

        public FerretFormTalisman(Serial serial)
            : base(serial)
        {
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
