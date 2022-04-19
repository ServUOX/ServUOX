namespace Server.Items
{
    public class LibraryFriendReadingChair : BigElvenChair
    {
        public override bool IsArtifact => true;
        [Constructible]
        public LibraryFriendReadingChair()
            : base()
        {
        }

        public LibraryFriendReadingChair(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1073340;// Friends of the Library Reading Chair
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
