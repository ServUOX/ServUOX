using Server.Targeting;

namespace Server.Items
{
    public class Eggshells : Item
    {
        [Constructible]
        public Eggshells()
            : base(0x9b4)
        {
            Weight = 0.5;
        }

        public Eggshells(Serial serial)
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
