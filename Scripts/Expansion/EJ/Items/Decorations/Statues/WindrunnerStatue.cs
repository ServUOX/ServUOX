using Server.Gumps;
using System;

namespace Server.Mobiles
{
    public class WindrunnerStatue : Item, ICreatureStatuette
    {
        public override int LabelNumber => 1124685;  // Windrunner

        public Type CreatureType => typeof(Windrunner);

        [Constructible]
        public WindrunnerStatue()
            : base(0x9ED5)
        {
            LootType = LootType.Blessed;
        }
        public WindrunnerStatue(Serial serial)
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
