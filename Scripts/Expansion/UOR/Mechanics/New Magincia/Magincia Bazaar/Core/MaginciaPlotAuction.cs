using Server.Accounting;
using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;

namespace Server.Engines.NewMagincia
{
    [PropertyObject]
    public class MaginciaPlotAuction
    {
        public Dictionary<Mobile, BidEntry> Auctioners { get; } = new Dictionary<Mobile, BidEntry>();

        [CommandProperty(AccessLevel.GameMaster)]
        public MaginciaBazaarPlot Plot { get; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime AuctionEnd { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool EndCurrentAuction
        {
            get => false;
            set => EndAuction();
        }

        public MaginciaPlotAuction(MaginciaBazaarPlot plot) : this(plot, MaginciaBazaar.GetShortAuctionTime)
        {
        }

        public MaginciaPlotAuction(MaginciaBazaarPlot plot, TimeSpan auctionDuration)
        {
            Plot = plot;
            AuctionEnd = DateTime.UtcNow + auctionDuration;
        }

        public override string ToString()
        {
            return "...";
        }

        public void MakeBid(Mobile bidder, int amount)
        {
            Auctioners[bidder] = new BidEntry(bidder, amount, BidType.Specific);
        }

        public bool RetractBid(Mobile from)
        {
            Account acct = from.Account as Account;

            for (int i = 0; i < acct.Length; i++)
            {
                Mobile m = acct[i];

                if (m == null)
                {
                    continue;
                }

                if (Auctioners.ContainsKey(m))
                {
                    BidEntry entry = Auctioners[m];

                    if (entry != null && Banker.Deposit(m, entry.Amount))
                    {
                        Auctioners.Remove(m);
                        return true;
                    }
                }
            }

            return false;
        }

        public void RemoveBid(Mobile from)
        {
            if (Auctioners.ContainsKey(from))
            {
                Auctioners.Remove(from);
            }
        }

        public int GetHighestBid()
        {
            int highest = -1;
            foreach (BidEntry entry in Auctioners.Values)
            {
                if (entry.Amount >= highest)
                {
                    highest = entry.Amount;
                }
            }
            return highest;
        }

        public void OnTick()
        {
            if (AuctionEnd < DateTime.UtcNow)
            {
                EndAuction();
            }
        }

        public void EndAuction()
        {
            if (Plot == null)
            {
                return;
            }

            if (Plot.HasTempMulti())
            {
                Plot.RemoveTempPlot();
            }

            Mobile winner = null;
            int highest = 0;

            Dictionary<Mobile, BidEntry> combined = new Dictionary<Mobile, BidEntry>(Auctioners);

            //Combine auction bids with the bids for next available plot
            foreach (KeyValuePair<Mobile, BidEntry> kvp in MaginciaBazaar.NextAvailable)
            {
                combined.Add(kvp.Key, kvp.Value);
            }

            //Get highest bid
            foreach (BidEntry entry in combined.Values)
            {
                if (entry.Amount > highest)
                {
                    highest = entry.Amount;
                }
            }

            // Check for owner, and if the owner has a match bad AND hasn't bidded on another plot!
            if (Plot.Owner != null && MaginciaBazaar.Reserve.ContainsKey(Plot.Owner) && MaginciaBazaar.Instance != null && !MaginciaBazaar.Instance.HasActiveBid(Plot.Owner))
            {
                int matching = MaginciaBazaar.GetBidMatching(Plot.Owner);

                if (matching >= highest)
                {
                    MaginciaBazaar.DeductReserve(Plot.Owner, highest);
                    int newreserve = MaginciaBazaar.GetBidMatching(Plot.Owner);
                    winner = Plot.Owner;

                    /*You extended your lease on Stall ~1_STALLNAME~ at the ~2_FACET~ New Magincia 
					 *Bazaar. You matched the top bid of ~3_BIDAMT~gp. That amount has been deducted 
					 *from your Match Bid of ~4_MATCHAMT~gp. Your Match Bid balance is now 
					 *~5_NEWMATCH~gp. You may reclaim any additional match bid funds or adjust 
					 *your match bid for next week at the bazaar.*/
                    MaginciaLottoSystem.SendMessageTo(Plot.Owner, new NewMaginciaMessage(null, new TextDefinition(1150427), string.Format("@{0}@{1}@{2}@{3}@{4}", Plot.PlotDef.ID, Plot.PlotDef.Map.ToString(), highest.ToString("N0"), matching.ToString("N0"), newreserve.ToString("N0"))));
                }
                else
                {
                    /*You lost the bid to extend your lease on Stall ~1_STALLNAME~ at the ~2_FACET~ 
					 *New Magincia Bazaar. Your match bid amount of ~3_BIDAMT~gp is held in escrow 
					 *at the Bazaar. You may obtain a full refund there at any time. Your hired 
					 *merchant, if any, has deposited your proceeds and remaining inventory at the 
					 *Warehouse in New Magincia. You must retrieve these within one week or they 
					 *will be destroyed.*/
                    MaginciaLottoSystem.SendMessageTo(Plot.Owner, new NewMaginciaMessage(null, new TextDefinition(1150528), string.Format("@{0}@{1}@{2}", Plot.PlotDef.ID, Plot.PlotDef.Map.ToString(), matching.ToString("N0"))));
                }
            }
            else if (Plot.Owner != null)
            {
                /*Your lease has expired on Stall ~1_STALLNAME~ at the ~2_FACET~ New Magincia Bazaar.*/
                MaginciaLottoSystem.SendMessageTo(Plot.Owner, new NewMaginciaMessage(null, new TextDefinition(1150430), string.Format("@{0}@{1}", Plot.PlotDef.ID, Plot.PlotDef.Map.ToString())));
            }

            if (winner == null)
            {
                //Get list of winners
                List<BidEntry> winners = new List<BidEntry>();
                foreach (KeyValuePair<Mobile, BidEntry> kvp in combined)
                {
                    if (kvp.Value.Amount >= highest)
                    {
                        winners.Add(kvp.Value);
                    }
                }

                // One winner!
                if (winners.Count == 1)
                {
                    winner = winners[0].Bidder;
                }
                else
                {
                    // get a list of specific type (as opposed to next available)
                    List<BidEntry> specifics = new List<BidEntry>();
                    foreach (BidEntry bid in winners)
                    {
                        if (bid.BidType == BidType.Specific)
                        {
                            specifics.Add(bid);
                        }
                    }

                    // one 1 specific!
                    if (specifics.Count == 1)
                    {
                        winner = specifics[0].Bidder;
                    }
                    else if (specifics.Count > 1)
                    {
                        //gets oldest specific
                        BidEntry oldest = null;
                        foreach (BidEntry entry in specifics)
                        {
                            if (oldest == null || entry.DatePlaced < oldest.DatePlaced)
                            {
                                oldest = entry;
                            }
                        }

                        winner = oldest.Bidder;
                    }
                    else
                    {
                        //no specifics! gets oldest of list of winners
                        BidEntry oldest = null;
                        foreach (BidEntry entry in winners)
                        {
                            if (oldest == null || entry.DatePlaced < oldest.DatePlaced)
                            {
                                oldest = entry;
                            }
                        }

                        if (oldest != null)
                        {
                            winner = oldest.Bidder;
                        }
                    }
                }
            }

            //Give back gold
            foreach (KeyValuePair<Mobile, BidEntry> kvp in Auctioners)
            {
                Mobile m = kvp.Key;

                if (m != winner)
                {
                    if (!Banker.Deposit(m, kvp.Value.Amount, true) && m.Backpack != null)
                    {
                        int total = kvp.Value.Amount;

                        while (total > 60000)
                        {
                            m.Backpack.DropItem(new BankCheck(60000));
                            total -= 60000;
                        }

                        if (total > 0)
                        {
                            m.Backpack.DropItem(new BankCheck(total));
                        }
                    }
                }
            }
            //Does actual changes to plots
            if (winner != null)
            {
                MaginciaBazaar.AwardPlot(this, winner, highest);
            }
            else
            {
                Plot.Reset(); // lease expires
                Plot.NewAuction(MaginciaBazaar.GetShortAuctionTime);
            }
        }

        public int GetBidAmount(Mobile from)
        {
            if (!Auctioners.ContainsKey(from))
            {
                return 0;
            }

            return Auctioners[from].Amount;
        }

        public void ChangeAuctionTime(TimeSpan ts)
        {
            AuctionEnd = DateTime.UtcNow + ts;

            if (Plot != null && Plot.Sign != null)
            {
                Plot.Sign.InvalidateProperties();
            }
        }

        public MaginciaPlotAuction(GenericReader reader, MaginciaBazaarPlot plot)
        {
            _ = reader.ReadInt();

            Plot = plot;
            AuctionEnd = reader.ReadDateTime();

            int c = reader.ReadInt();
            for (int i = 0; i < c; i++)
            {
                Mobile m = reader.ReadMobile();
                BidEntry entry = new BidEntry(reader);

                if (m != null)
                {
                    Auctioners[m] = entry;
                }
            }
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(0);

            writer.Write(AuctionEnd);

            writer.Write(Auctioners.Count);
            foreach (KeyValuePair<Mobile, BidEntry> kvp in Auctioners)
            {
                writer.Write(kvp.Key);
                kvp.Value.Serialize(writer);
            }
        }
    }

    public enum BidType
    {
        Specific,
        NextAvailable
    }

    public class BidEntry : IComparable
    {
        public Mobile Bidder { get; }
        public int Amount { get; }
        public BidType BidType { get; }
        public DateTime DatePlaced { get; }

        public BidEntry(Mobile bidder, int amount, BidType type)
        {
            Bidder = bidder;
            Amount = amount;
            BidType = type;
            DatePlaced = DateTime.UtcNow;
        }

        public int CompareTo(object obj)
        {
            return ((BidEntry)obj).Amount - Amount;
        }

        public BidEntry(GenericReader reader)
        {
            _ = reader.ReadInt();
            Bidder = reader.ReadMobile();
            Amount = reader.ReadInt();
            BidType = (BidType)reader.ReadInt();
            DatePlaced = reader.ReadDateTime();
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(0);
            writer.Write(Bidder);
            writer.Write(Amount);
            writer.Write((int)BidType);
            writer.Write(DatePlaced);
        }
    }
}
