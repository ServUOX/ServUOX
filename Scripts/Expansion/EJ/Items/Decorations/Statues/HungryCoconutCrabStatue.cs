using Server.Gumps;
using System;

namespace Server.Mobiles
{
    public class HungryCoconutCrabStatue : Item, ICreatureStatuette
    {
        public override int LabelNumber => 1159221;  // Hungry Coconut Crab Statuette

        public Type CreatureType => typeof(HungryCoconutCrab);

        [Constructible]
        public HungryCoconutCrabStatue()
            : base(0xA336)
        {
            Hue = 2713;
        }

        public HungryCoconutCrabStatue(Serial serial)
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
                SendLocalizedMessageTo(from, 1010095); // This must be on your person to use.
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1159222); // *Redeemable for a pet*<br>*Requires High Seas Booster Pack*
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
