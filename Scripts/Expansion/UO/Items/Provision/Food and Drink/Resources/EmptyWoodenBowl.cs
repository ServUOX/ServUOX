using Server.Targeting;

namespace Server.Items
{
    public class EmptyWoodenBowl : Item
    {
        [Constructible]
        public EmptyWoodenBowl()
            : base(0x15F8)
        {
            Weight = 1.0;
        }

        public EmptyWoodenBowl(Serial serial)
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
