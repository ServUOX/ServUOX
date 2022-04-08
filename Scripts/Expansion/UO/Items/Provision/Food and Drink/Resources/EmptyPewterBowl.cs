using Server.Targeting;

namespace Server.Items
{
    public class EmptyPewterBowl : Item
    {
        [Constructible]
        public EmptyPewterBowl()
            : base(0x15FD)
        {
            Weight = 1.0;
        }

        public EmptyPewterBowl(Serial serial)
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
