using Server.Network;
using System;

namespace Server.Items
{
    public abstract class BaseMagicFish : Item
    {
        public BaseMagicFish(int hue)
            : base(0xDD6)
        {
            Hue = hue;
        }

        public BaseMagicFish(Serial serial)
            : base(serial)
        {
        }

        public virtual int Bonus => 0;
        public virtual StatType Type => StatType.Str;
        public override double DefaultWeight => 1.0;
        public virtual bool Apply(Mobile from)
        {
            bool applied = Spells.SpellHelper.AddStatOffset(from, Type, Bonus, TimeSpan.FromMinutes(1.0));

            if (!applied)
            {
                from.SendLocalizedMessage(502173); // You are already under a similar effect.
            }

            return applied;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else if (Apply(from))
            {
                from.FixedEffect(0x375A, 10, 15);
                from.PlaySound(0x1E7);
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 501774); // You swallow the fish whole!
                Delete();
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
