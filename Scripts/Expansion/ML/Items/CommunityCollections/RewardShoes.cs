using System;
using Server.Engines.Craft;

namespace Server.Items
{
    #region Reward Clothing
    public class ZooMemberThighBoots : ThighBoots
    {
        public override int LabelNumber => 1073221;// Britannia Royal Zoo Member

        [Constructible]
        public ZooMemberThighBoots()
            : this(0)
        {
        }

        [Constructible]
        public ZooMemberThighBoots(int hue)
            : base(hue)
        {
        }

        public ZooMemberThighBoots(Serial serial)
            : base(serial)
        {
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

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
    }

    #endregion

}
