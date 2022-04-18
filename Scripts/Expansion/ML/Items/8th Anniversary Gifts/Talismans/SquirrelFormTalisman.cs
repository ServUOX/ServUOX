namespace Server.Items
{
    public class SquirrelFormTalisman : BaseFormTalisman
    {
        public override TalismanForm Form => TalismanForm.Squirrel;

        [Constructible]
        public SquirrelFormTalisman()
            : base()
        {
        }

        public SquirrelFormTalisman(Serial serial)
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
