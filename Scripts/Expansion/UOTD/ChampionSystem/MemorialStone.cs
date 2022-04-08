using Server.ContextMenus;
using System.Collections.Generic;

namespace Server.Items
{
    public class MemorialStone : Item
    {
        public override int LabelNumber => 1071563;  // Memorial Stone

        [Constructible]
        public MemorialStone()
            : base(0x117F)
        {
            Movable = false;
        }

        public MemorialStone(Serial serial)
            : base(serial)
        {
        }

        public override bool HandlesOnMovement => true;

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (Parent == null && Utility.InRange(Location, m.Location, 1) && !Utility.InRange(Location, oldLocation, 1))
            {
                Ankhs.Resurrect(m, this);
            }
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            Ankhs.GetContextMenuEntries(from, this, list);
        }

        public override void OnDoubleClickDead(Mobile m)
        {
            Ankhs.Resurrect(m, this);
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
