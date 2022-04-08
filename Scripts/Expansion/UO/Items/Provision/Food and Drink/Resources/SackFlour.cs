using Server.Engines.Craft;
using Server.Targeting;
using System;

namespace Server.Items
{
    public class SackFlour : Item, IQuality
    {
        private ItemQuality _Quality;

        [CommandProperty(AccessLevel.GameMaster)]
        public ItemQuality Quality { get => _Quality; set { _Quality = value; InvalidateProperties(); } }

        public bool PlayerConstructed => true;

        [Constructible]
        public SackFlour()
            : this(1)
        {
        }

        [Constructible]
        public SackFlour(int amount)
            : base(0x1039)
        {
            Weight = 5.0;

            Stackable = true;
            Amount = amount;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
            {
                return;
            }

            var flour = new SackFlourOpen
            {
                Location = Location
            };

            if (Parent is Container)
            {
                ((Container)Parent).DropItem(flour);
            }
            else
            {
                flour.MoveToWorld(GetWorldLocation(), Map);
            }

            if (Amount > 1)
            {
                Amount--;
            }
            else
            {
                Delete();
            }
        }

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ItemQuality)quality;

            return quality;
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            if (_Quality == ItemQuality.Exceptional)
            {
                list.Add(1060636); // Exceptional
            }
        }

        public SackFlour(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(4);
            writer.Write((int)_Quality);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 4:
                    _Quality = (ItemQuality)reader.ReadInt();
                    break;
                case 3:
                    _Quality = (ItemQuality)reader.ReadInt();
                    reader.ReadInt();
                    Stackable = true;
                    break;
            }
        }
    }
}
