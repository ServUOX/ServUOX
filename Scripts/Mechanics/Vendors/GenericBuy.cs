using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Mobiles
{
    public class GenericBuyInfo : IBuyItemInfo
    {
        public static Dictionary<Type, int> BuyPrices = new Dictionary<Type, int>();
        private int m_Price;
        private int m_MaxAmount, m_Amount;
        private IEntity m_DisplayEntity;
        private bool m_Stackable;

        public GenericBuyInfo(Type type, int price, int amount, int itemID, int hue, bool stacks = false)
            : this(null, type, price, amount, itemID, hue, null, stacks)
        {
        }

        public GenericBuyInfo(string name, Type type, int price, int amount, int itemID, int hue, bool stacks = false)
            : this(name, type, price, amount, itemID, hue, null, stacks)
        {
        }

        public GenericBuyInfo(Type type, int price, int amount, int itemID, int hue, object[] args, bool stacks = false)
            : this(null, type, price, amount, itemID, hue, args, stacks)
        {
        }

        public GenericBuyInfo(string name, Type type, int price, int amount, int itemID, int hue, object[] args, bool stacks = false)
        {
            if (type != null)
            {
                BuyPrices[type] = price;
            }

            Type = type;
            m_Price = price;
            ItemID = itemID;
            Hue = hue;
            Args = args;
            m_Stackable = stacks;

            if (type != null && EconomyItem)
            {
                m_MaxAmount = m_Amount = BaseVendor.EconomyStockAmount;
            }
            else
            {
                m_MaxAmount = m_Amount = amount;
            }

            if (Siege.SiegeShard)
            {
                m_Price *= 3;
            }

            if (name == null)
            {
                Name = itemID < 0x4000 ? (1020000 + itemID).ToString() : (1078872 + itemID).ToString();
            }
            else
            {
                Name = name;
            }
        }

        public virtual int ControlSlots => 0;
        public virtual bool CanCacheDisplay => false;//return ( m_Args == null || m_Args.Length == 0 ); } 
        public Type Type { get; set; }
        public string Name { get; set; }
        public int DefaultPrice { get; private set; }
        public int PriceScalar
        {
            get => DefaultPrice;
            set => DefaultPrice = value;
        }

        public int TotalBought { get; set; }

        public int TotalSold { get; set; }

        public virtual bool Stackable
        {
            get => m_Stackable;
            set => m_Stackable = value;
        }

        public bool EconomyItem => BaseVendor.UseVendorEconomy && m_Stackable;

        public int Price
        {
            get
            {
                int ecoInc = 0;

                if (EconomyItem)
                {
                    if (TotalBought >= BaseVendor.BuyItemChange)
                    {
                        ecoInc += TotalBought / BaseVendor.BuyItemChange;
                    }

                    if (TotalSold >= BaseVendor.SellItemChange)
                    {
                        ecoInc -= TotalSold / BaseVendor.SellItemChange;
                    }
                }

                if (DefaultPrice != 0)
                {
                    if (m_Price > 5000000)
                    {
                        long price = m_Price;

                        price *= DefaultPrice;
                        price += 50;
                        price /= 100;

                        if (price > int.MaxValue)
                        {
                            price = int.MaxValue;
                        }

                        if (EconomyItem && (int)price + ecoInc < 2)
                        {
                            return 2;
                        }

                        return (int)price + ecoInc;
                    }

                    if (EconomyItem && (((m_Price * DefaultPrice) + 50) / 100) + ecoInc < 2)
                    {
                        return 2;
                    }

                    return (((m_Price * DefaultPrice) + 50) / 100) + ecoInc;
                }

                if (EconomyItem && m_Price + ecoInc < 2)
                {
                    return 2;
                }

                return m_Price + ecoInc;
            }
            set => m_Price = value;
        }
        public int ItemID { get; set; }
        public int Hue { get; set; }
        public int Amount
        {
            get => m_Amount;
            set
            {
                // Amount is ALWAYS 500
                if (EconomyItem)
                {
                    m_Amount = BaseVendor.EconomyStockAmount;
                }
                else
                {
                    if (value < 0)
                    {
                        value = 0;
                    }

                    m_Amount = value;
                }
            }
        }
        public int MaxAmount
        {
            get
            {
                // Amount is ALWAYS 500
                if (EconomyItem)
                {
                    return BaseVendor.EconomyStockAmount;
                }

                return m_MaxAmount;
            }
            set => m_MaxAmount = value;
        }
        public object[] Args { get; set; }
        public void DeleteDisplayEntity()
        {
            if (m_DisplayEntity == null)
            {
                return;
            }

            m_DisplayEntity.Delete();
            m_DisplayEntity = null;
        }

        public IEntity GetDisplayEntity()
        {
            if (m_DisplayEntity != null && !IsDeleted(m_DisplayEntity))
            {
                return m_DisplayEntity;
            }

            bool canCache = CanCacheDisplay;

            if (canCache)
            {
                m_DisplayEntity = DisplayCache.Cache.Lookup(Type);
            }

            if (m_DisplayEntity == null || IsDeleted(m_DisplayEntity))
            {
                m_DisplayEntity = GetEntity();
            }

            DisplayCache.Cache.Store(Type, m_DisplayEntity, canCache);

            return m_DisplayEntity;
        }

        //get a new instance of an object (we just bought it)
        public virtual IEntity GetEntity()
        {
            if (Args == null || Args.Length == 0)
            {
                return (IEntity)Activator.CreateInstance(Type);
            }

            return (IEntity)Activator.CreateInstance(Type, Args);
            //return (Item)Activator.CreateInstance( m_Type );
        }

        //Attempt to restock with item, (return true if restock sucessful)
        public virtual bool Restock(Item item, int amount)
        {
            if (item == null || item.GetType() != Type)
            {
                return false;
            }

            return EconomyItem;
        }

        public virtual void OnRestock()
        {
            if (m_Amount <= 0)
            {
                m_MaxAmount *= 2;

                if (m_MaxAmount >= 999)
                {
                    m_MaxAmount = 999;
                }
            }
            else
            {
                /* NOTE: According to UO.com, the quantity is halved if the item does not reach 0
                * Here we implement differently: the quantity is halved only if less than half
                * of the maximum quantity was bought. That is, if more than half is sold, then
                * there's clearly a demand and we should not cut down on the stock.
                */
                int halfQuantity = m_MaxAmount;

                if (halfQuantity >= 999)
                {
                    halfQuantity = 640;
                }
                else if (halfQuantity > 20)
                {
                    halfQuantity /= 2;
                }

                if (m_Amount >= halfQuantity)
                {
                    m_MaxAmount = halfQuantity;
                }
            }

            m_Amount = m_MaxAmount;
        }

        private bool IsDeleted(IEntity obj)
        {
            if (obj is Item)
            {
                return ((Item)obj).Deleted;
            }
            else if (obj is Mobile)
            {
                return ((Mobile)obj).Deleted;
            }

            return false;
        }

        public void OnBought(Mobile buyer, BaseVendor vendor, IEntity entity, int amount)
        {
            if (EconomyItem)
            {
                foreach (var bii in vendor.GetBuyInfo().OfType<GenericBuyInfo>())
                {
                    if (bii.Type == Type || (Type == typeof(UncutCloth) && bii.Type == typeof(Cloth)) || (Type == typeof(Cloth) && bii.Type == typeof(UncutCloth)))
                    {
                        bii.TotalBought += amount;
                    }
                }
            }

            EventSink.InvokeValidVendorPurchase(new ValidVendorPurchaseEventArgs(buyer, vendor, entity, m_Price));
        }

        public void OnSold(BaseVendor vendor, int amount)
        {
            if (EconomyItem)
            {
                foreach (var bii in vendor.GetBuyInfo().OfType<GenericBuyInfo>())
                {
                    if (bii.Type == Type || (Type == typeof(UncutCloth) && bii.Type == typeof(Cloth)) || (Type == typeof(Cloth) && bii.Type == typeof(UncutCloth)))
                    {
                        bii.TotalSold += amount;
                    }
                }
            }
        }

        public static bool IsDisplayCache(IEntity e)
        {
            if (e is Mobile)
            {
                return DisplayCache.Cache.Mobiles != null && DisplayCache.Cache.Mobiles.Contains((Mobile)e);
            }

            return DisplayCache.Cache.Table != null && DisplayCache.Cache.Table.ContainsValue(e);
        }

        private class DisplayCache : Container
        {
            private static DisplayCache m_Cache;

            public List<Mobile> Mobiles { get; private set; }
            public Dictionary<Type, IEntity> Table { get; private set; }

            public DisplayCache()
                : base(0)
            {
                Table = new Dictionary<Type, IEntity>();
                Mobiles = new List<Mobile>();
            }

            public DisplayCache(Serial serial)
                : base(serial)
            {
            }

            public static DisplayCache Cache
            {
                get
                {
                    if (m_Cache == null || m_Cache.Deleted)
                    {
                        m_Cache = new DisplayCache();
                    }

                    return m_Cache;
                }
            }
            public IEntity Lookup(Type key)
            {
                Table.TryGetValue(key, out IEntity e);
                return e;
            }

            public void Store(Type key, IEntity obj, bool cache)
            {
                if (cache)
                {
                    Table[key] = obj;
                }

                if (obj is Item)
                {
                    AddItem((Item)obj);
                }
                else if (obj is Mobile)
                {
                    Mobiles.Add((Mobile)obj);
                }
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                for (int i = 0; i < Mobiles.Count; ++i)
                {
                    Mobiles[i].Delete();
                }

                Mobiles.Clear();

                for (int i = Items.Count - 1; i >= 0; --i)
                {
                    if (i < Items.Count)
                    {
                        Items[i].Delete();
                    }
                }

                if (m_Cache == this)
                {
                    m_Cache = null;
                }
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write(0);

                writer.Write(Mobiles);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                _ = reader.ReadInt();

                Mobiles = reader.ReadStrongMobileList();

                for (int i = 0; i < Mobiles.Count; ++i)
                {
                    Mobiles[i].Delete();
                }

                Mobiles.Clear();

                for (int i = Items.Count - 1; i >= 0; --i)
                {
                    if (i < Items.Count)
                    {
                        Items[i].Delete();
                    }
                }

                if (m_Cache == null)
                {
                    m_Cache = this;
                }
                else
                {
                    Delete();
                }

                Table = new Dictionary<Type, IEntity>();
            }
        }
    }
    public class GenericBuyInfo<T> : GenericBuyInfo
    {
        public Action<T, GenericBuyInfo> CreateCallback { get; set; }

        public GenericBuyInfo(int price, int amount, int itemID, int hue, bool stacks = false, Action<T, GenericBuyInfo> callback = null)
            : this(null, price, amount, itemID, hue, null, stacks, callback)
        {
        }

        public GenericBuyInfo(string name, int price, int amount, int itemID, int hue, bool stacks = false, Action<T, GenericBuyInfo> callback = null)
            : this(name, price, amount, itemID, hue, null, stacks, callback)
        {
        }

        public GenericBuyInfo(int price, int amount, int itemID, int hue, object[] args, bool stacks = false, Action<T, GenericBuyInfo> callback = null)
            : this(null, price, amount, itemID, hue, args, stacks, callback)
        {
        }

        public GenericBuyInfo(string name, int price, int amount, int itemID, int hue, object[] args, bool stacks = false, Action<T, GenericBuyInfo> callback = null)
            : base(name, typeof(T), price, amount, itemID, hue, args, stacks)
        {
            CreateCallback = callback;
        }

        public override IEntity GetEntity()
        {
            var entity = base.GetEntity();

            CreateCallback?.Invoke((T)entity, this);

            return entity;
        }
    }
}
