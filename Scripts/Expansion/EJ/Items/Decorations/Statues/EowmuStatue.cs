using Server.Gumps;
using System;

namespace Server.Mobiles
{
    public class EowmuStatue : Item, ICreatureStatuette
    {
        public override int LabelNumber => 1158082;  // Eowmu

        public Type CreatureType => typeof(Eowmu);

        [Constructible]
        public EowmuStatue()
            : base(0xA0C0)
        {
            LootType = LootType.Blessed;
        }
        public EowmuStatue(Serial serial)
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
