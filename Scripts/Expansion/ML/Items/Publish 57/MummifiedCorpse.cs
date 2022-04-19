namespace Server.Items
{
    public class MummifiedCorpse : Item
    {
        public override int LabelNumber => 1112400;  //a mummified corpse

        [Constructible]
        public MummifiedCorpse() : base(0x1C20)
        {
        }

        public MummifiedCorpse(Serial serial) : base(serial)
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
