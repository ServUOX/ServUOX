using Server.Accounting;
using System;

namespace Server.Engines.NewMagincia
{
    [PropertyObject]
    public class MaginciaBazaarPlot
    {
        private PlotDef m_Definition;
        private BaseBazaarMulti m_PlotMulti;
        private BaseBazaarBroker m_Merchant;

        [CommandProperty(AccessLevel.GameMaster)]
        public PlotDef PlotDef { get => m_Definition; set { } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public string ShopName { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public BaseBazaarMulti PlotMulti
        {
            get => m_PlotMulti;
            set
            {
                if (m_PlotMulti != null && m_PlotMulti != value && value != null)
                {
                    m_PlotMulti.Delete();
                    m_PlotMulti = null;
                }

                m_PlotMulti = value;

                if (m_PlotMulti != null)
                {
                    m_PlotMulti.MoveToWorld(m_Definition.MultiLocation, m_Definition.Map);
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public BaseBazaarBroker Merchant
        {
            get => m_Merchant;
            set
            {
                m_Merchant = value;

                if (m_Merchant != null)
                {
                    m_Merchant.Plot = this;

                    Point3D p = m_Definition.Location;
                    p.X++;
                    p.Y++;
                    p.Z = 27;

                    if (m_PlotMulti != null && m_PlotMulti.Fillers.Count > 0)
                    {
                        p = m_PlotMulti.Fillers[0].Location;
                        p.Z = m_PlotMulti.Fillers[0].Z + TileData.ItemTable[m_PlotMulti.Fillers[0].ItemID & TileData.MaxItemValue].CalcHeight;
                    }

                    m_Merchant.MoveToWorld(p, m_Definition.Map);
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public PlotSign Sign { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public MaginciaPlotAuction Auction { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Active => MaginciaBazaar.IsActivePlot(this);

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime AuctionEnds
        {
            get
            {
                if (Auction == null)
                {
                    return DateTime.MinValue;
                }

                return Auction.AuctionEnd;
            }

        }

        public MaginciaBazaarPlot(PlotDef definition)
        {
            m_Definition = definition;
            Owner = null;
            m_PlotMulti = null;
            m_Merchant = null;
            ShopName = null;
        }

        public bool IsOwner(Mobile from)
        {
            if (from == null || Owner == null)
            {
                return false;
            }

            if (from == Owner)
            {
                return true;
            }


            return from.Account is Account acct1 && Owner.Account is Account acct2 && acct1 == acct2;
        }

        public void AddPlotSign()
        {
            Sign = new PlotSign(this);
            Sign.MoveToWorld(m_Definition.SignLoc, m_Definition.Map);
        }

        public void Reset()
        {
            if (m_PlotMulti != null)
            {
                Timer.DelayCall(TimeSpan.FromMinutes(2), new TimerCallback(DeleteMulti_Callback));
            }

            EndTempMultiTimer();

            if (m_Merchant != null)
            {
                m_Merchant.Dismiss();
            }

            Owner = null;
            ShopName = null;
            m_Merchant = null;
            ShopName = null;
        }

        public void NewAuction(TimeSpan time)
        {
            Auction = new MaginciaPlotAuction(this, time);

            if (Sign != null)
            {
                Sign.InvalidateProperties();
            }
        }

        private void DeleteMulti_Callback()
        {
            if (m_PlotMulti != null)
            {
                m_PlotMulti.Delete();
            }

            m_PlotMulti = null;
        }

        public void OnTick()
        {
            if (Auction != null)
            {
                Auction.OnTick();
            }

            if (m_Merchant != null)
            {
                m_Merchant.OnTick();
            }

            if (Sign != null)
            {
                Sign.InvalidateProperties();
            }
        }

        #region Stall Style Multis
        private Timer m_Timer;

        public void AddTempMulti(int idx1, int idx2)
        {
            if (m_PlotMulti != null)
            {
                m_PlotMulti.Delete();
                m_PlotMulti = null;
            }

            BaseBazaarMulti multi = null;

            if (idx1 == 0)
            {
                switch (idx2)
                {
                    case 0: multi = new CommodityStyle1(); break;
                    case 1: multi = new CommodityStyle2(); break;
                    case 2: multi = new CommodityStyle3(); break;
                }
            }
            else
            {
                switch (idx2)
                {
                    case 0: multi = new PetStyle1(); break;
                    case 1: multi = new PetStyle2(); break;
                    case 2: multi = new PetStyle3(); break;
                }
            }

            if (multi != null)
            {
                PlotMulti = multi;
                BeginTempMultiTimer();
            }
        }

        public void ConfirmMulti(bool commodity)
        {
            EndTempMultiTimer();

            if (commodity)
            {
                Merchant = new CommodityBroker(this);
            }
            else
            {
                Merchant = new PetBroker(this);
            }
        }

        public void RemoveTempPlot()
        {
            EndTempMultiTimer();

            if (m_PlotMulti != null)
            {
                m_PlotMulti.Delete();
                m_PlotMulti = null;
            }
        }

        public void BeginTempMultiTimer()
        {
            if (m_Timer != null)
            {
                m_Timer.Stop();
                m_Timer = null;
            }

            m_Timer = new InternalTimer(this);
            m_Timer.Start();
        }

        public void EndTempMultiTimer()
        {
            if (m_Timer != null)
            {
                m_Timer.Stop();
                m_Timer = null;
            }
        }

        public bool HasTempMulti()
        {
            return m_Timer != null;
        }

        private class InternalTimer : Timer
        {
            private MaginciaBazaarPlot m_Plot;

            public InternalTimer(MaginciaBazaarPlot plot) : base(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))
            {
                m_Plot = plot;
            }

            protected override void OnTick()
            {
                if (m_Plot != null)
                {
                    m_Plot.RemoveTempPlot();
                }
            }
        }

        #endregion

        public override string ToString()
        {
            return "...";
        }

        public bool TrySetShopName(Mobile from, string text)
        {
            if (text == null || !Server.Guilds.BaseGuildGump.CheckProfanity(text) || text.Length == 0 || text.Length > 40)
            {
                return false;
            }

            ShopName = text;

            if (m_Merchant != null)
            {
                m_Merchant.InvalidateProperties();
            }

            if (Sign != null)
            {
                Sign.InvalidateProperties();
            }

            from.SendLocalizedMessage(1150333); // Your shop has been renamed.

            return true;
        }

        public void FireBroker()
        {
            if (m_Merchant != null)
            {
                m_Merchant.Delete();
                m_Merchant = null;

                if (m_PlotMulti != null)
                {
                    m_PlotMulti.Delete();
                    m_PlotMulti = null;
                }
            }
        }

        public void Abandon()
        {
            Reset();

            if (Auction != null)
            {
                Auction.ChangeAuctionTime(MaginciaBazaar.GetShortAuctionTime);
            }
        }

        public int GetBid(Mobile from)
        {
            if (Auction != null && Auction.Auctioners.ContainsKey(from))
            {
                return Auction.Auctioners[from].Amount;
            }

            return 0;
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(0);

            m_Definition.Serialize(writer);

            writer.Write(Owner);
            writer.Write(ShopName);
            writer.Write(m_Merchant);
            writer.Write(Sign);
            writer.Write(m_PlotMulti);

            if (Auction != null)
            {
                writer.Write(true);
                Auction.Serialize(writer);
            }
            else
            {
                writer.Write(false);
            }
        }

        public MaginciaBazaarPlot(GenericReader reader)
        {
            _ = reader.ReadInt();

            m_Definition = new PlotDef(reader);

            Owner = reader.ReadMobile();
            ShopName = reader.ReadString();
            m_Merchant = reader.ReadMobile() as BaseBazaarBroker;
            Sign = reader.ReadItem() as PlotSign;
            m_PlotMulti = reader.ReadItem() as BaseBazaarMulti;

            if (reader.ReadBool())
            {
                Auction = new MaginciaPlotAuction(reader, this);
            }

            if (m_Merchant != null)
            {
                m_Merchant.Plot = this;
            }

            if (Sign != null)
            {
                Sign.Plot = this;
            }
        }
    }

    [PropertyObject]
    public class PlotDef
    {
        private Point3D m_Location;

        [CommandProperty(AccessLevel.GameMaster)]
        public string ID { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D Location { get => m_Location; set => m_Location = value; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map Map { get; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D SignLoc => new Point3D(m_Location.X + 1, m_Location.Y - 2, m_Location.Z);

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D MultiLocation => new Point3D(m_Location.X, m_Location.Y, m_Location.Z + 2);

        public PlotDef(string id, Point3D pnt, int mapID)
        {
            ID = id;
            m_Location = pnt;
            Map = Server.Map.Maps[mapID];
        }

        public override string ToString()
        {
            return "...";
        }

        public PlotDef(GenericReader reader)
        {
            _ = reader.ReadInt();

            ID = reader.ReadString();
            m_Location = reader.ReadPoint3D();
            Map = reader.ReadMap();
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(0);

            writer.Write(ID);
            writer.Write(m_Location);
            writer.Write(Map);
        }
    }
}
