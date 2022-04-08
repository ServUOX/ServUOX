using Server.Accounting;
using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;

namespace Server.Engines.NewMagincia
{
    public class MaginciaBazaar : Item
    {
        public static readonly int DefaultComissionFee = 5;
        public static TimeSpan GetShortAuctionTime => TimeSpan.FromMinutes(Utility.RandomMinMax(690, 750));
        public static TimeSpan GetLongAuctionTime => TimeSpan.FromHours(Utility.RandomMinMax(168, 180));

        public static MaginciaBazaar Instance { get; set; }

        private Timer m_Timer;

        public static List<MaginciaBazaarPlot> Plots { get; } = new List<MaginciaBazaarPlot>();
        public static Dictionary<Mobile, BidEntry> NextAvailable { get; } = new Dictionary<Mobile, BidEntry>();
        public static Dictionary<Mobile, int> Reserve { get; } = new Dictionary<Mobile, int>();

        private bool m_Enabled;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Enabled
        {
            get => m_Enabled;
            set
            {
                if (m_Enabled != value)
                {
                    if (value)
                    {
                        StartTimer();
                    }
                    else
                    {
                        EndTimer();
                    }
                }

                m_Enabled = value;
            }
        }

        /*
         * Phase 1 - Stalls A - D (0 - 19)
         * Phase 2 - Stalls E - G (20 - 34)
         * Phase 3 - Stalls H - J (35 - 49)
         */
        public enum Phase
        {
            Phase1 = 1,
            Phase2 = 2,
            Phase3 = 3
        }

        private Phase m_Phase;

        [CommandProperty(AccessLevel.GameMaster)]
        public Phase PlotPhase
        {
            get => m_Phase;
            set
            {

                if (value > m_Phase)
                {
                    m_Phase = value;
                    ActivatePlots();
                }
            }
        }

        public MaginciaBazaar() : base(3240)
        {
            Movable = false;
            m_Enabled = true;

            WarehouseSuperintendent mob = new WarehouseSuperintendent();
            mob.MoveToWorld(new Point3D(3795, 2259, 20), Map.Trammel);
            mob.Home = mob.Location;
            mob.RangeHome = 12;

            mob = new WarehouseSuperintendent();
            mob.MoveToWorld(new Point3D(3795, 2259, 20), Map.Felucca);
            mob.Home = mob.Location;
            mob.RangeHome = 12;

            LoadPlots();
            AddPlotSigns();

            if (m_Enabled)
            {
                StartTimer();
            }

            m_Phase = Phase.Phase1;
            ActivatePlots();
        }

        public static bool IsActivePlot(MaginciaBazaarPlot plot)
        {
            if (Instance != null)
            {
                int index = Plots.IndexOf(plot);

                if (index == -1)
                {
                    return false;
                }

                switch ((int)Instance.m_Phase)
                {
                    case 1: return index < 40;
                    case 2: return index < 70;
                    case 3: return index < 100;
                }
            }

            return false;
        }

        public void ActivatePlots()
        {
            for (int i = 0; i < Plots.Count; i++)
            {
                MaginciaBazaarPlot plot = Plots[i];

                switch ((int)m_Phase)
                {
                    case 1:
                        if (i < 40 && plot.Auction == null)
                        {
                            plot.NewAuction(GetShortAuctionTime);
                        }
                        break;
                    case 2:
                        if (i < 70 && plot.Auction == null)
                        {
                            plot.NewAuction(GetShortAuctionTime);
                        }
                        break;
                    case 3:
                        if (i < 100 && plot.Auction == null)
                        {
                            plot.NewAuction(GetShortAuctionTime);
                        }
                        break;
                }

                if (plot.Sign != null)
                {
                    plot.Sign.InvalidateProperties();
                }
            }
        }

        public void StartTimer()
        {
            if (m_Timer != null)
            {
                m_Timer.Stop();
            }

            m_Timer = Timer.DelayCall(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1), new TimerCallback(OnTick));
            m_Timer.Priority = TimerPriority.OneMinute;
            m_Timer.Start();
        }

        public void EndTimer()
        {
            if (m_Timer != null)
            {
                m_Timer.Stop();
            }

            m_Timer = null;
        }

        public void OnTick()
        {
            foreach (MaginciaBazaarPlot plot in Plots)
            {
                if (plot.Active)
                {
                    plot.OnTick();
                }
            }

            List<Mobile> toRemove = new List<Mobile>();

            foreach (KeyValuePair<Mobile, StorageEntry> kvp in m_WarehouseStorage)
            {
                Mobile m = kvp.Key;
                StorageEntry entry = kvp.Value;

                if (entry.Expires < DateTime.UtcNow)
                {
                    bool deleted = false;
                    bool stabled = false;

                    if (entry.CommodityTypes.Count > 0)
                    {
                        deleted = true;
                    }

                    foreach (BaseCreature bc in entry.Creatures)
                    {
                        if (m.Stabled.Count < AnimalTrainer.GetMaxStabled(m))
                        {
                            PetBroker.SendToStables(m, bc);

                            if (!stabled)
                            {
                                stabled = true;
                            }
                        }
                        else
                        {
                            if (!deleted)
                            {
                                deleted = true;
                            }

                            bc.Delete();
                        }
                    }

                    if (stabled)
                    {
                        string message;

                        if (deleted)
                        {
                            message = "Your broker inventory and/or funds in storage at the New Magincia Warehouse " +
                            "have been donated to charity, because these items remained unclaimed for a " +
                            "full week. These items may no longer be recovered, but the orphans will " +
                            "appreciate a nice hot meal. One or all of your pets have been placed in your stables.";
                        }
                        else
                        {
                            message = "Because your pets remained in storage for more than a full week, one or all of them have been placed in your stables. " +
                                "If you had insufficient room in your stables, any further pets will be lost and returned to the wild.";
                        }

                        MaginciaLottoSystem.SendMessageTo(m, new NewMaginciaMessage(new TextDefinition(1150676), message, null));
                    }
                    else if (deleted)
                    {
                        toRemove.Add(m);

                        /*Your broker inventory and/or funds in storage at the New Magincia Warehouse 
                         *have been donated to charity, because these items remained unclaimed for a 
                         *full week. These items may no longer be recovered, but the orphans will 
                         *appreciate a nice hot meal.*/

                        MaginciaLottoSystem.SendMessageTo(m, new NewMaginciaMessage(new TextDefinition(1150676), new TextDefinition(1150673), null));
                    }
                }
            }

            foreach (Mobile m in toRemove)
            {
                if (m_WarehouseStorage.ContainsKey(m))
                {
                    m_WarehouseStorage.Remove(m);
                }
            }

            ColUtility.Free(toRemove);
        }

        public void AddPlotSigns()
        {
            foreach (MaginciaBazaarPlot plot in Plots)
            {
                Point3D loc = new Point3D(plot.PlotDef.SignLoc.X - 1, plot.PlotDef.SignLoc.Y, plot.PlotDef.SignLoc.Z);

                Static pole = new Static(2592);
                pole.MoveToWorld(loc, plot.PlotDef.Map);

                pole = new Static(2969);
                pole.MoveToWorld(plot.PlotDef.SignLoc, plot.PlotDef.Map);

                plot.AddPlotSign();
            }
        }

        public override void Delete()
        {
            // Note: This cannot be deleted.  That could potentially piss alot of people off who have items and gold invested in a plot.
        }

        public static MaginciaBazaarPlot GetPlot(Mobile from)
        {
            foreach (MaginciaBazaarPlot plot in Plots)
            {
                if (plot.IsOwner(from))
                {
                    return plot;
                }
            }

            return null;
        }

        public static bool HasPlot(Mobile from)
        {
            foreach (MaginciaBazaarPlot plot in Plots)
            {
                if (plot.IsOwner(from))
                {
                    return true;
                }
            }

            return false;
        }

        public static MaginciaBazaarPlot GetBiddingPlot(Mobile from)
        {
            if (!(from.Account is Account acct))
            {
                return null;
            }

            for (int i = 0; i < acct.Length; i++)
            {
                Mobile m = acct[i];

                if (m == null)
                {
                    continue;
                }

                MaginciaBazaarPlot plot = GetBiddingPlotForAccount(m);

                if (plot != null)
                {
                    return plot;
                }
            }

            return null;
        }

        public static MaginciaBazaarPlot GetBiddingPlotForAccount(Mobile from)
        {
            foreach (MaginciaBazaarPlot plot in Plots)
            {
                if (plot.Auction != null && plot.Auction.Auctioners.ContainsKey(from))
                {
                    return plot;
                }
            }

            return null;
        }

        public bool HasActiveBid(Mobile from)
        {
            return GetBiddingPlot(from) != null || NextAvailable.ContainsKey(from);
        }

        public static bool TryRetractBid(Mobile from)
        {
            MaginciaBazaarPlot plot = GetBiddingPlot(from);

            if (plot != null)
            {
                return plot.Auction.RetractBid(from);
            }

            return RetractBid(from);
        }

        public static bool RetractBid(Mobile from)
        {
            Account acct = from.Account as Account;

            for (int i = 0; i < acct.Length; i++)
            {
                Mobile m = acct[i];

                if (m == null)
                {
                    continue;
                }

                if (NextAvailable.ContainsKey(m))
                {
                    BidEntry entry = NextAvailable[m];

                    if (entry != null && Banker.Deposit(m, entry.Amount))
                    {
                        NextAvailable.Remove(m);
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsBiddingNextAvailable(Mobile from)
        {
            return NextAvailable.ContainsKey(from);
        }

        public static int GetNextAvailableBid(Mobile from)
        {
            if (NextAvailable.ContainsKey(from))
            {
                return NextAvailable[from].Amount;
            }

            return 0;
        }

        public static void MakeBidNextAvailable(Mobile from, int amount)
        {
            NextAvailable[from] = new BidEntry(from, amount, BidType.NextAvailable);
        }

        public static void RemoveBidNextAvailable(Mobile from)
        {
            if (NextAvailable.ContainsKey(from))
            {
                NextAvailable.Remove(from);
            }
        }

        public static void AwardPlot(MaginciaPlotAuction auction, Mobile winner, int highest)
        {
            MaginciaBazaarPlot plot = auction.Plot;

            if (NextAvailable.ContainsKey(winner))
            {
                NextAvailable.Remove(winner);
            }

            if (plot != null && plot.Owner != winner)
            {
                MaginciaBazaarPlot current = GetPlot(winner);

                //new owner
                if (current == null && winner != plot.Owner)
                {
                    /*You won a lease on Stall ~1_STALLNAME~ at the ~2_FACET~ New Magincia Bazaar. 
                     *Your bid amount of ~3_BIDAMT~gp won the auction and has been remitted. Your 
                     *lease begins immediately and will continue for 7 days.*/
                    MaginciaLottoSystem.SendMessageTo(winner, new NewMaginciaMessage(null, new TextDefinition(1150426), string.Format("{0}\t{1}\t{2}", plot.PlotDef.ID, plot.PlotDef.Map.ToString(), highest.ToString("###,###,###"))));
                }

                plot.Reset();

                //Transfer to new plot
                if (current != null)
                {
                    /*You won a lease and moved to Stall ~1_STALLNAME~ at the ~2_FACET~ New Magincia 
                     *Bazaar. The lease on your previous stall has been terminated. Your hired 
                     *merchant, if any, has relocated your stall and goods to the new lot. Your 
                     *bid amount of ~3_BIDAMT~gp has been remitted.*/
                    MaginciaLottoSystem.SendMessageTo(winner, new NewMaginciaMessage(null, new TextDefinition(1150428), string.Format("{0}\t{1}\t{2}", plot.PlotDef.ID, plot.PlotDef.Map, highest.ToString("###,###,###"))));

                    plot.PlotMulti = current.PlotMulti;
                    plot.Merchant = current.Merchant;
                    plot.ShopName = current.ShopName;

                    current.PlotMulti = null;
                    current.Merchant = null;
                    current.Owner = null;

                    if (current.Auction != null)
                    {
                        current.Auction.EndAuction();
                    }
                }

                plot.Owner = winner;
                plot.NewAuction(GetLongAuctionTime);
            }
            else if (plot != null)
            {
                if (plot.Owner != null)
                {
                    plot.NewAuction(GetLongAuctionTime);
                }
                else
                {
                    plot.Reset();
                    plot.NewAuction(GetShortAuctionTime);
                }
            }
        }

        public static void RegisterPlot(PlotDef plotDef)
        {
            Plots.Add(new MaginciaBazaarPlot(plotDef));
        }

        public static bool IsSameAccount(Mobile check, Mobile checkAgainst)
        {
            return IsSameAccount(check, checkAgainst, false);
        }

        public static bool IsSameAccount(Mobile check, Mobile checkAgainst, bool checkLink)
        {
            if (check == null || checkAgainst == null)
            {
                return false;
            }

            Account acct2 = check.Account as Account;

            if (checkAgainst.Account is Account acct1 && acct1 == acct2)
            {
                return true;
            }

            return false;
        }

        #region Bizaar Authority Storage
        private static Dictionary<Mobile, StorageEntry> m_WarehouseStorage = new Dictionary<Mobile, StorageEntry>();

        public void AddInventoryToWarehouse(Mobile owner, BaseBazaarBroker broker)
        {
            StorageEntry entry = GetStorageEntry(owner);

            if (entry == null)
            {
                if (broker.HasValidEntry(owner))
                {
                    entry = new StorageEntry(owner, broker);
                }
            }
            else if (broker.HasValidEntry(owner))
            {
                entry.AddInventory(owner, broker);
            }

            if (entry != null)
            {
                m_WarehouseStorage[owner] = entry;
                /*Your hired broker has transferred any remaining inventory and funds from 
                 *your stall at the New Magincia Bazaar into storage at the New Magincia 
                 *Warehouse. You must reclaim these items or they will be destroyed! To reclaim 
                 *these items, see the Warehouse Superintendent in New Magincia.<br><br>This 
                 *service is provided free of charge. If you owed your broker any back fees, 
                 *those fees must be paid before you can reclaim your belongings. The storage 
                 *period lasts 7 days starting with the expiration of your lease at the New 
                 *Magincia Bazaar, and this storage period cannot be extended. Claim your 
                 *possessions and gold without delay!<br><br>The expiration time of this 
                 *message coincides with the expiration time of your Warehouse storage.*/

                MaginciaLottoSystem.SendMessageTo(owner, new NewMaginciaMessage(1150676, new TextDefinition(1150674), null));
            }
        }

        public static StorageEntry GetStorageEntry(Mobile from)
        {
            if (m_WarehouseStorage.ContainsKey(from))
            {
                return m_WarehouseStorage[from];
            }

            return null;
        }

        public static void RemoveFromStorage(Mobile from)
        {
            if (m_WarehouseStorage.ContainsKey(from))
            {
                m_WarehouseStorage.Remove(from);
            }
        }

        public static void AddToReserve(Mobile from, int amount)
        {
            foreach (Mobile m in Reserve.Keys)
            {
                if (from == m || IsSameAccount(from, m))
                {
                    Reserve[m] += amount;
                    return;
                }
            }

            Reserve[from] = amount;
        }

        public static void DeductReserve(Mobile from, int amount)
        {
            foreach (Mobile m in Reserve.Keys)
            {
                if (from == m || IsSameAccount(from, m))
                {
                    Reserve[m] -= amount;

                    if (Reserve[m] <= 0)
                    {
                        Reserve.Remove(m);
                    }

                    return;
                }
            }
        }

        public static int GetBidMatching(Mobile from)
        {
            foreach (Mobile m in Reserve.Keys)
            {
                if (from == m || IsSameAccount(m, from))
                {
                    return Reserve[m];
                }
            }

            return 0;
        }
        #endregion

        public MaginciaBazaar(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            writer.Write(m_Enabled);
            writer.Write((int)m_Phase);

            writer.Write(Plots.Count);
            for (int i = 0; i < Plots.Count; i++)
            {
                Plots[i].Serialize(writer);
            }

            writer.Write(NextAvailable.Count);
            foreach (KeyValuePair<Mobile, BidEntry> kvp in NextAvailable)
            {
                writer.Write(kvp.Key);
                kvp.Value.Serialize(writer);
            }

            writer.Write(m_WarehouseStorage.Count);
            foreach (KeyValuePair<Mobile, StorageEntry> kvp in m_WarehouseStorage)
            {
                writer.Write(kvp.Key);
                kvp.Value.Serialize(writer);
            }

            writer.Write(Reserve.Count);
            foreach (KeyValuePair<Mobile, int> kvp in Reserve)
            {
                writer.Write(kvp.Key);
                writer.Write(kvp.Value);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            m_Enabled = reader.ReadBool();
            m_Phase = (Phase)reader.ReadInt();

            int count = reader.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Plots.Add(new MaginciaBazaarPlot(reader));
            }

            count = reader.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Mobile m = reader.ReadMobile();
                BidEntry entry = new BidEntry(reader);

                if (m != null)
                {
                    NextAvailable[m] = entry;
                }
            }

            count = reader.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Mobile m = reader.ReadMobile();

                StorageEntry entry = new StorageEntry(reader);

                if (m != null)
                {
                    m_WarehouseStorage[m] = entry;
                }
            }

            count = reader.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Mobile m = reader.ReadMobile();
                int amt = reader.ReadInt();

                if (m != null && amt > 0)
                {
                    Reserve[m] = amt;
                }
            }

            Instance = this;

            if (m_Enabled)
            {
                StartTimer();
            }
        }

        public static void LoadPlots()
        {
            int idx = 0;
            RegisterPlot(new PlotDef("A-1", m_StallLocs[idx], 0));
            RegisterPlot(new PlotDef("A-1", m_StallLocs[idx], 1));
            idx++;
            RegisterPlot(new PlotDef("A-2", m_StallLocs[idx], 0));
            RegisterPlot(new PlotDef("A-2", m_StallLocs[idx], 1));
            idx++;
            RegisterPlot(new PlotDef("A-3", m_StallLocs[idx], 0));
            RegisterPlot(new PlotDef("A-3", m_StallLocs[idx], 1));
            idx++;
            RegisterPlot(new PlotDef("A-4", m_StallLocs[idx], 0));
            RegisterPlot(new PlotDef("A-4", m_StallLocs[idx], 1));
            idx++;
            RegisterPlot(new PlotDef("B-1", m_StallLocs[idx], 0));
            RegisterPlot(new PlotDef("B-1", m_StallLocs[idx], 1));
            idx++;
            RegisterPlot(new PlotDef("B-2", m_StallLocs[idx], 0));
            RegisterPlot(new PlotDef("B-2", m_StallLocs[idx], 1));
            idx++;
            RegisterPlot(new PlotDef("B-3", m_StallLocs[idx], 0));
            RegisterPlot(new PlotDef("B-3", m_StallLocs[idx], 1));
            idx++;
            RegisterPlot(new PlotDef("B-4", m_StallLocs[idx], 0));
            RegisterPlot(new PlotDef("B-4", m_StallLocs[idx], 1));
            idx++;
            RegisterPlot(new PlotDef("B-5", m_StallLocs[idx], 0));
            RegisterPlot(new PlotDef("B-5", m_StallLocs[idx], 1));
            idx++;
            RegisterPlot(new PlotDef("B-6", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("B-6", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("C-1", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("C-1", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("C-2", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("C-2", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("C-3", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("C-3", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("C-4", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("C-4", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("C-5", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("C-5", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("D-1", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("D-1", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("D-2", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("D-2", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("D-3", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("D-3", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("D-4", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("D-4", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("D-5", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("D-5", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("E-1", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("E-1", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("E-2", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("E-2", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("E-3", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("E-3", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("E-4", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("E-4", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("E-5", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("E-5", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("F-1", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("F-1", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("F-2", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("F-2", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("F-3", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("F-3", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("F-4", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("F-4", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("F-5", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("F-5", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("G-1", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("G-1", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("G-2", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("G-2", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("G-3", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("G-3", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("G-4", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("G-4", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("G-5", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("G-5", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("H-1", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("H-1", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("H-2", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("H-2", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("H-3", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("H-3", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("H-4", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("H-4", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("H-5", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("H-5", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("H-6", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("H-6", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("I-1", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("I-1", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("I-2", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("I-2", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("I-3", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("I-3", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("I-4", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("I-4", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("I-5", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("I-5", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("J-1", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("J-1", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("J-2", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("J-2", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("J-3", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("J-3", m_StallLocs[idx], 0));
            idx++;
            RegisterPlot(new PlotDef("J-4", m_StallLocs[idx], 1));
            RegisterPlot(new PlotDef("J-4", m_StallLocs[idx], 0));
        }

        private static Point3D[] m_StallLocs = new Point3D[]
        {
            //A
            new Point3D(3716, 2198, 20),
            new Point3D(3709, 2198, 20),
            new Point3D(3700, 2192, 20),
            new Point3D(3693, 2192, 20),

            //B
            new Point3D(3686, 2192, 20),
            new Point3D(3686, 2198, 20),
            new Point3D(3686, 2204, 20),
            new Point3D(3686, 2210, 20),
            new Point3D(3686, 2216, 20),
            new Point3D(3686, 2222, 20),

            //C
            new Point3D(3686, 2228, 20),
            new Point3D(3692, 2228, 20),
            new Point3D(3698, 2228, 20),
            new Point3D(3704, 2228, 20),
            new Point3D(3710, 2228, 20),

            //D
            new Point3D(3716, 2228, 20),
            new Point3D(3716, 2222, 20),
            new Point3D(3716, 2216, 20),
            new Point3D(3716, 2210, 20),
            new Point3D(3716, 2204, 20),

            //E
            new Point3D(3686, 2178, 20),
            new Point3D(3686, 2171, 20),
            new Point3D(3686, 2164, 20),
            new Point3D(3686, 2157, 20),
            new Point3D(3686, 2150, 20),

            //F
            new Point3D(3693, 2178, 20),
            new Point3D(3693, 2171, 20),
            new Point3D(3693, 2164, 20),
            new Point3D(3693, 2157, 20),
            new Point3D(3693, 2150, 20),

            //G
            new Point3D(3700, 2178, 20),
            new Point3D(3700, 2171, 20),
            new Point3D(3700, 2164, 20),
            new Point3D(3700, 2157, 20),
            new Point3D(3700, 2150, 20),

            //H
            new Point3D(3672, 2238, 20),
            new Point3D(3665, 2238, 20),
            new Point3D(3658, 2238, 20),
            new Point3D(3672, 2245, 20),
            new Point3D(3665, 2245, 20),
            new Point3D(3658, 2245, 20),

            //I
            new Point3D(3730, 2249, 20),
            new Point3D(3730, 2242, 20),
            new Point3D(3737, 2242, 20),
            new Point3D(3737, 2249, 20),
            new Point3D(3737, 2256, 20),

            //J
            new Point3D(3730, 2220, 20),
            new Point3D(3737, 2220, 20),
            new Point3D(3730, 2228, 20),
            new Point3D(3737, 2228, 20),
        };
    }
}
