using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public class SweetCocoaButter : Item, IQuality
    {
        private ItemQuality _Quality;

        [CommandProperty(AccessLevel.GameMaster)]
        public ItemQuality Quality { get { return _Quality; } set { _Quality = value; InvalidateProperties(); } }

        public override int LabelNumber => 1156401;  // Sweet Cocoa butter
        public override double DefaultWeight => 1.0;

        public bool PlayerConstructed => true;

        [Constructible]
        public SweetCocoaButter()
            : base(0x103D)
        {
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            if (_Quality == ItemQuality.Exceptional)
            {
                list.Add(1060636); // Exceptional
            }
        }

        public virtual int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ItemQuality)quality;

            return quality;
        }

        public SweetCocoaButter(Serial serial)
            : base(serial)
        { }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);

            writer.Write((int)_Quality);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            _Quality = (ItemQuality)reader.ReadInt();
        }
    }
}
