using System;

namespace Server.Items
{
    public class Anniversary17thBag : Bag
    {
        public override int LabelNumber { get { return 1155416; } }/* 17th Anniversary Gift Bag*/
        [Constructible]
        public Anniversary17thBag()
            : this(1)
        {
        }

        [Constructible]
        public Anniversary17thBag(int amount)
        {
        }
        /*
        public override void AddNameProperties(ObjectPropertyList list)
        {
            AddNameProperty(list);
            list.Add(1155423);//In Commemoration of Seventeen Amazing Years! Thank You and Happy Anniversary!

            if (IsSecure)
                AddSecureProperty(list);
            else if (IsLockedDown)
                AddLockedDownProperty(list);

            if (DisplayLootType)
                AddLootTypeProperty(list);
        }
         */

        public Anniversary17thBag(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}