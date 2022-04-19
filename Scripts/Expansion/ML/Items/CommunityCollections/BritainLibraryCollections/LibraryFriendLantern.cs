namespace Server.Items
{
    public class LibraryFriendLantern : Lantern
    {
        public override bool IsArtifact => true;
        [Constructible]
        public LibraryFriendLantern()
            : base()
        {
        }

        public LibraryFriendLantern(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1073339;// Friends of the Library Reading Lantern
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
