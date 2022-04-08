using Server.Gumps;

namespace Server.Items
{
    [Flipable(0xA2C6, 0xA2C7)]
    public class MysteriousStatue : Item
    {
        public override int LabelNumber => 1158935;  // Mysterious Statue

        [Constructible]
        public MysteriousStatue()
            : base(0xA2C6)
        {
        }

        public override void OnDoubleClick(Mobile m)
        {
            if (m.InRange(GetWorldLocation(), 2))
            {
                m.SendGump(new BasicInfoGump(1158937));
                /*This mysterious statue towers above you. Even as skilled a mason as you are, the craftsmanship is uncanny, and unlike anything you have encountered before.
                 * The stone appears to be smooth and special attention was taken to sculpt the statue as a perfect likeness. According to the pirate you purchased the statue
                 * from, it was recovered somewhere at sea. The amount of marine growth seems to reinforce this claim, yet you cannot discern how long it may have been
                 * submerged and are thus unsure of its age. Whatever its origins, one thing is clear - the figure is one you hope you do not encounter anytime soon...*/
            }
        }

        /*public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add(1158936); // Purchased from a Pirate Merchant
        }*/

        public MysteriousStatue(Serial serial) : base(serial)
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
