using Server.Network;

namespace Server.Items
{
    //[Flipable(0x1765, 0x1767)]
    public class FoldedCloth1 : Item, IScissorable, IDyable, ICommodity
    {
        [Constructible]
        public FoldedCloth1()
            : this(1)
        {
        }

        [Constructible]
        public FoldedCloth1(int amount)
            : base(0x1761)
        {
            Stackable = true;
            Amount = amount;
        }

        public FoldedCloth1(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight => 0.1;
        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;
        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
            {
                return false;
            }

            Hue = sender.DyedHue;

            return true;
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

        public override void OnSingleClick(Mobile from)
        {
            int number = (Amount == 1) ? 1049124 : 1049123;

            from.Send(new MessageLocalized(Serial, ItemID, MessageType.Regular, 0x3B2, 3, number, "", Amount.ToString()));
        }

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this))
            {
                return false;
            }

            base.ScissorHelper(from, new Bandage(), 1);

            return true;
        }
    }
}
