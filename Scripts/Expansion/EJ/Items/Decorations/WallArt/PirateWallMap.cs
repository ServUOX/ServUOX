using Server.Gumps;

namespace Server.Items
{
    [Flipable(0xA2C8, 0xA2C9)]
    public class PirateWallMap : Item
    {
        public override int LabelNumber => 1158938;  // Pirate Wall Map

        [Constructible]
        public PirateWallMap()
            : base(0xA2C8)
        {
        }

        public override void OnDoubleClick(Mobile m)
        {
            if (m.InRange(GetWorldLocation(), 2))
            {
                var gump = new Gump(50, 50);
                gump.AddImage(0, 0, 0x9CE9);

                m.SendGump(gump);
            }
        }

        public PirateWallMap(Serial serial) : base(serial)
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
            reader.ReadInt();
        }
    }
}
