using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public class ThreeTieredCake : Item, IQuality
    {
        private ItemQuality _Quality;
        private int _Pieces;

        [CommandProperty(AccessLevel.GameMaster)]
        public ItemQuality Quality { get => _Quality; set { _Quality = value; InvalidateProperties(); } }

        public bool PlayerConstructed => true;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Pieces
        {
            get => _Pieces;
            set
            {
                _Pieces = value;

                if (_Pieces <= 0)
                {
                    Delete();
                }
            }
        }

        public override int LabelNumber => 1098235;  // A Three Tiered Cake 

        [Constructible]
        public ThreeTieredCake()
            : base(0x4BA3)
        {
            Weight = 1.0;
            Pieces = 10;
        }

        public virtual int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ItemQuality)quality;

            return quality;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                var cake = new Cake
                {
                    ItemID = 0x4BA4
                };

                from.PrivateOverheadMessage(Network.MessageType.Regular, 1154, 1157341, from.NetState); // *You cut a slice from the cake.*
                from.AddToBackpack(cake);

                Pieces--;
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            if (_Quality == ItemQuality.Exceptional)
            {
                list.Add(1060636); // Exceptional
            }
        }

        public ThreeTieredCake(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write((int)_Quality);
            writer.Write(_Pieces);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            _Quality = (ItemQuality)reader.ReadInt();
            _Pieces = reader.ReadInt();
        }
    }
}
