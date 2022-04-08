using System;
using Server.Network;

namespace Server.Items
{
    [Flipable(0x1765, 0x1767)]
    public class AbyssalCloth : Item, ICommodity, IScissorable
    {
        [Constructible]
        public AbyssalCloth()
            : this(1)
        {
        }

        [Constructible]
        public AbyssalCloth(int amount)
            : base(0x1767)
        {
            Stackable = true;
            Amount = amount;
            Hue = 2075;
        }

        public AbyssalCloth(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1113350;// abyssal cloth

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void OnSingleClick(Mobile from)
        {
            int number = (Amount == 1) ? 1049124 : 1049123;

            from.Send(new MessageLocalized(Serial, ItemID, MessageType.Regular, 0x3B2, 3, number, "", Amount.ToString()));
        }

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this))
                return false;

            base.ScissorHelper(from, new Bandage(), 1);

            return true;
        }
    }
}