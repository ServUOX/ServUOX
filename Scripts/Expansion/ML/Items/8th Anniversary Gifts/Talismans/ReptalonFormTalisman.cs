namespace Server.Items
{
    public class ReptalonFormTalisman : BaseFormTalisman
    {
        public override TalismanForm Form => TalismanForm.Reptalon;

        [Constructible]
        public ReptalonFormTalisman()
            : base()
        {
        }

        public ReptalonFormTalisman(Serial serial)
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
