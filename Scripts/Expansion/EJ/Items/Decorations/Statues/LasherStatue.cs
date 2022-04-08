using Server.Gumps;
using System;

namespace Server.Mobiles
{
    public class LasherStatue : Item, ICreatureStatuette
    {
        public override int LabelNumber => 1157214;  // Lasher

        public Type CreatureType => typeof(Lasher);

        [Constructible]
        public LasherStatue()
            : base(0x9E35)
        {
            LootType = LootType.Blessed;
        }
        public LasherStatue(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.SendGump(new ConfirmMountStatuetteGump(this));
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
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
