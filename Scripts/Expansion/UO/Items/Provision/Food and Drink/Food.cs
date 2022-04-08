using Server.ContextMenus;
using Server.Engines.Craft;
using System;
using System.Collections.Generic;

namespace Server.Items
{
    public abstract class Food : Item, IEngravable, IQuality
    {
        private bool m_PlayerConstructed;
        private ItemQuality _Quality;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Poisoner { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed
        {
            get => m_PlayerConstructed;
            set
            {
                m_PlayerConstructed = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Poison Poison { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int FillFactor { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual ItemQuality Quality { get => _Quality; set { _Quality = value; InvalidateProperties(); } }

        private string m_EngravedText = string.Empty;

        [CommandProperty(AccessLevel.GameMaster)]
        public string EngravedText
        {
            get => m_EngravedText;
            set
            {
                if (value != null)
                {
                    m_EngravedText = value;
                }
                else
                {
                    m_EngravedText = string.Empty;
                }

                InvalidateProperties();
            }
        }

        public Food(int itemID)
            : this(1, itemID)
        {
        }

        public Food(int amount, int itemID)
            : base(itemID)
        {
            Stackable = true;
            Amount = amount;
            FillFactor = 1;
        }

        public Food(Serial serial)
            : base(serial)
        {
        }

        public override void OnAfterDuped(Item newItem)
        {
            if (!(newItem is Food food))
            {
                return;
            }

            food.PlayerConstructed = m_PlayerConstructed;
            food.Poisoner = Poisoner;
            food.Poison = Poison;
            food.Quality = _Quality;

            base.OnAfterDuped(newItem);
        }

        public virtual int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ItemQuality)quality;

            return quality;
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            if (from.Alive)
            {
                list.Add(new ContextMenus.EatEntry(from, this));
            }
        }

        public virtual bool TryEat(Mobile from)
        {
            if (Deleted || !Movable || !from.CheckAlive() || !CheckItemUse(from))
            {
                return false;
            }

            return Eat(from);
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            if (_Quality == ItemQuality.Exceptional)
            {
                list.Add(1060636); // Exceptional
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
            {
                return;
            }

            if (from.InRange(GetWorldLocation(), 1))
            {
                Eat(from);
            }
        }

        public override bool WillStack(Mobile from, Item dropped)
        {
            return dropped is Food && ((Food)dropped).PlayerConstructed == PlayerConstructed && base.WillStack(from, dropped);
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            base.AddNameProperty(list);

            if (!string.IsNullOrEmpty(EngravedText))
            {
                list.Add(1072305, Utility.FixHtml(EngravedText)); // Engraved: ~1_INSCRIPTION~
            }
        }

        public virtual bool Eat(Mobile from)
        {
            if (CheckHunger(from))
            {
                from.PlaySound(Utility.Random(0x3A, 3));

                if (from.Body.IsHuman && !from.Mounted)
                {
                    if (Core.SA)
                    {
                        from.Animate(AnimationType.Eat, 0);
                    }
                    else
                    {
                        from.Animate(34, 5, 1, true, false, 0);
                    }
                }

                if (Poison != null)
                {
                    from.ApplyPoison(Poisoner, Poison);
                }

                Consume();

                EventSink.InvokeOnConsume(new OnConsumeEventArgs(from, this));

                return true;
            }

            return false;
        }

        public virtual bool CheckHunger(Mobile from)
        {
            return FillHunger(from, FillFactor);
        }

        public static bool FillHunger(Mobile from, int fillFactor)
        {
            if (from.Hunger >= 20)
            {
                from.SendLocalizedMessage(500867); // You are simply too full to eat any more!
                return false;
            }

            int iHunger = from.Hunger + fillFactor;

            if (from.Stam < from.StamMax)
            {
                from.Stam += Utility.Random(6, 3) + fillFactor / 5;
            }

            if (iHunger >= 20)
            {
                from.Hunger = 20;
                from.SendLocalizedMessage(500872); // You manage to eat the food, but you are stuffed!
            }
            else
            {
                from.Hunger = iHunger;

                if (iHunger < 5)
                {
                    from.SendLocalizedMessage(500868); // You eat the food, but are still extremely hungry.
                }
                else if (iHunger < 10)
                {
                    from.SendLocalizedMessage(500869); // You eat the food, and begin to feel more satiated.
                }
                else if (iHunger < 15)
                {
                    from.SendLocalizedMessage(500870); // After eating the food, you feel much less hungry.
                }
                else
                {
                    from.SendLocalizedMessage(500871); // You feel quite full after consuming the food.
                }
            }

            return true;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(7);

            writer.Write((int)_Quality);

            writer.Write(m_EngravedText);

            writer.Write(m_PlayerConstructed);
            writer.Write(Poisoner);

            Poison.Serialize(Poison, writer);
            writer.Write(FillFactor);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        switch (reader.ReadInt())
                        {
                            case 0:
                                Poison = null;
                                break;
                            case 1:
                                Poison = Poison.Lesser;
                                break;
                            case 2:
                                Poison = Poison.Regular;
                                break;
                            case 3:
                                Poison = Poison.Greater;
                                break;
                            case 4:
                                Poison = Poison.Deadly;
                                break;
                        }

                        break;
                    }
                case 2:
                    {
                        Poison = Poison.Deserialize(reader);
                        break;
                    }
                case 3:
                    {
                        Poison = Poison.Deserialize(reader);
                        FillFactor = reader.ReadInt();
                        break;
                    }
                case 4:
                    {
                        Poisoner = reader.ReadMobile();
                        goto case 3;
                    }
                case 5:
                    {
                        m_PlayerConstructed = reader.ReadBool();
                        goto case 4;
                    }
                case 6:
                    m_EngravedText = reader.ReadString();
                    goto case 5;
                case 7:
                    _Quality = (ItemQuality)reader.ReadInt();
                    goto case 6;
            }
        }
    }
#if false
	public class Pizza : Food
	{
		[Constructible]
		public Pizza() : base( 0x1040 )
		{
			Stackable = false;
			Weight = 1.0;
			FillFactor = 6;
		}

		public Pizza( Serial serial ) : base( serial )
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
	}
#endif
}
