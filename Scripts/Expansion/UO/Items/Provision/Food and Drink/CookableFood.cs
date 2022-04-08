using Server.Engines.Craft;
using Server.Targeting;
using System;

namespace Server.Items
{
    public abstract class CookableFood : Item, IQuality, ICommodity
    {
        private ItemQuality _Quality;

        [CommandProperty(AccessLevel.GameMaster)]
        public int CookingLevel { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public ItemQuality Quality { get => _Quality; set { _Quality = value; InvalidateProperties(); } }

        public bool PlayerConstructed => true;

        public CookableFood(int itemID, int cookingLevel)
            : base(itemID)
        {
            CookingLevel = cookingLevel;
        }

        public CookableFood(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

        public abstract Food Cook();

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(2);
            // Version 2
            writer.Write((int)_Quality);
            // Version 1
            writer.Write(CookingLevel);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    {
                        _Quality = (ItemQuality)reader.ReadInt();
                        goto case 1;
                    }
                case 1:
                    {
                        CookingLevel = reader.ReadInt();
                        break;
                    }
            }
        }

#if false
		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
		    {
               return;
            }

			from.Target = new CookableFoodTarget( this );
		}
#endif

        public static bool IsHeatSource(object targeted)
        {
            int itemID;

            if (targeted is Item)
            {
                itemID = ((Item)targeted).ItemID;
            }
            else if (targeted is StaticTarget)
            {
                itemID = ((StaticTarget)targeted).ItemID;
            }
            else
            {
                return false;
            }

            if (itemID >= 0xDE3 && itemID <= 0xDE9)
            {
                return true; // Campfire
            }
            else if (itemID >= 0x461 && itemID <= 0x48E)
            {
                return true; // Sandstone oven/fireplace
            }
            else if (itemID >= 0x92B && itemID <= 0x96C)
            {
                return true; // Stone oven/fireplace
            }
            else if (itemID == 0xFAC)
            {
                return true; // Firepit
            }
            else if (itemID >= 0x184A && itemID <= 0x184C)
            {
                return true; // Heating stand (left)
            }
            else if (itemID >= 0x184E && itemID <= 0x1850)
            {
                return true; // Heating stand (right)
            }
            else if (itemID >= 0x398C && itemID <= 0x399F)
            {
                return true; // Fire field
            }

            return false;
        }

        private class CookableFoodTarget : Target
        {
            private readonly CookableFood m_Item;

            public CookableFoodTarget(CookableFood item)
                : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted)
                {
                    return;
                }

                if (IsHeatSource(targeted))
                {
                    if (from.BeginAction(typeof(CookableFood)))
                    {
                        from.PlaySound(0x225);

                        m_Item.Consume();

                        CookableFoodTimer t = new CookableFoodTimer(from, targeted as IPoint3D, from.Map, m_Item);
                        t.Start();
                    }
                    else
                    {
                        from.SendLocalizedMessage(500119); // You must wait to perform another action
                    }
                }
            }

            private class CookableFoodTimer : Timer
            {
                private readonly Mobile m_From;
                private readonly IPoint3D m_Point;
                private readonly Map m_Map;
                private readonly CookableFood m_CookableFood;

                public CookableFoodTimer(Mobile from, IPoint3D p, Map map, CookableFood cookableFood)
                    : base(TimeSpan.FromSeconds(5.0))
                {
                    m_From = from;
                    m_Point = p;
                    m_Map = map;
                    m_CookableFood = cookableFood;
                }

                protected override void OnTick()
                {
                    m_From.EndAction(typeof(CookableFood));

                    if (m_From.Map != m_Map || (m_Point != null && m_From.GetDistanceToSqrt(m_Point) > 3))
                    {
                        m_From.SendLocalizedMessage(500686); // You burn the food to a crisp! It's ruined.
                        return;
                    }

                    if (m_From.CheckSkill(SkillName.Cooking, m_CookableFood.CookingLevel, 100))
                    {
                        Food cookedFood = m_CookableFood.Cook();

                        if (m_From.AddToBackpack(cookedFood))
                        {
                            m_From.PlaySound(0x57);
                        }
                    }
                    else
                    {
                        m_From.SendLocalizedMessage(500686); // You burn the food to a crisp! It's ruined.
                    }
                }
            }
        }
    }

#if false
	// ********** UncookedPizza **********
	public class UncookedPizza : CookableFood
	{
		[Constructible]
		public UncookedPizza() : base( 0x1083, 20 )
		{
			Weight = 1.0;
		}

		public UncookedPizza( Serial serial ) : base( serial )
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
            _ = reader.ReadInt();
        }

		public override Food Cook()
		{
			return new Pizza();
		}
	}
#endif


}
