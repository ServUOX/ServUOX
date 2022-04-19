using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public class CocoaLiquor : Item, IQuality
    {
        public override int LabelNumber => 1080007;  // Cocoa liquor
        public override double DefaultWeight => 1.0;

        public virtual bool PlayerConstructed => true;

        private ItemQuality _Quality;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual ItemQuality Quality { get { return _Quality; } set { _Quality = value; InvalidateProperties(); } }

        [Constructible]
        public CocoaLiquor()
            : base(0x103F)
        {
            Hue = 1130;
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            if (_Quality == ItemQuality.Exceptional)
            {
                list.Add(1060636); // Exceptional
            }
        }

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ItemQuality)quality;

            return quality;
        }

        public CocoaLiquor(Serial serial)
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
