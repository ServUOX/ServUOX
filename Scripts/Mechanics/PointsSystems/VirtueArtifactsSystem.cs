using System;
using Server.Items;
using Server.Mobiles;
using Server.Engines.SeasonalEvents;
using Server.Engines.Points;

namespace Server.Misc
{
    public class VirtueArtifactsSystem : PointsSystem
    {
        public static bool Enabled => SeasonalEventSystem.IsActive(EventType.VirtueArtifacts);

        public static Type[] Artifacts { get; } = new Type[]
            {
                typeof( KatrinasCrook ), typeof( JaanasStaff ), typeof( DragonsEnd ), typeof( AnkhPendant ),
                typeof( SentinelsGuard ), typeof( LordBlackthornsExemplar ), typeof( MapOfTheKnownWorld ), typeof( TenthAnniversarySculpture ),
                typeof( CompassionArms ), typeof( JusticeBreastplate ), typeof( ValorGauntlets ), typeof( HonestyGorget ),
                typeof( SpiritualityHelm ), typeof( HonorLegs ), typeof( SacrificeSollerets )
            };

        public override PointsType Loyalty => PointsType.VAS;
        public override TextDefinition Name => m_Name;
        public override bool AutoAdd => true;
        public override double MaxPoints => double.MaxValue;
        public override bool ShowOnLoyaltyGump => false;

        private TextDefinition m_Name = new TextDefinition("Virtue Artifact System");

        public VirtueArtifactsSystem()
        {
        }

        private bool CheckLocation(Mobile m)
        {
            Region r = m.Region;

            if (m is BaseCreature && ((BaseCreature)m).IsChampionSpawn)
                return false;

            if (r.IsPartOf<Server.Regions.HouseRegion>() || Multis.BaseBoat.FindBoatAt(m, m.Map) != null)
                return false;

            return (r.IsPartOf("Covetous") || r.IsPartOf("Deceit") || r.IsPartOf("Despise") || r.IsPartOf("Destard") ||
                r.IsPartOf("Hythloth") || r.IsPartOf("Shame") || r.IsPartOf("Wrong"));
        }

        public override void SendMessage(PlayerMobile from, double old, double points, bool quest)
        {
        }

        public override TextDefinition GetTitle(PlayerMobile from)
        {
            return new TextDefinition("Virtue Artifact System");
        }

        public override void ProcessKill(Mobile victim, Mobile killer)
        {
            if (!Enabled || !(killer is PlayerMobile pm) || !(victim is BaseCreature bc) || !CheckLocation(bc) || !CheckLocation(pm) || !killer.InRange(victim, 18) || !killer.Alive || bc.GivenSpecialArtifact)
                return;

            if (bc.Controlled || bc.Owners.Count > 0 || bc.Fame <= 0)
                return;

            int luck = Math.Max(0, pm.RealLuck);
            AwardPoints(pm, (int)Math.Max(0, (bc.Fame * (1 + Math.Sqrt(luck) / 100))));

            var vapoints = GetPoints(pm);
            const double A = 0.000863316841;
            const double B = 0.00000425531915;

            double chance = A * Math.Pow(10, B * vapoints);

            double roll = Utility.RandomDouble();

            if (chance > roll)
            {
                Item i;
                try
                {
                    i = Activator.CreateInstance(Artifacts[Utility.Random(Artifacts.Length)]) as Item;
                }
                catch
                {
                    return;
                }

                if (i != null)
                {
                    killer.PlaySound(0x5B4);
                    pm.SendLocalizedMessage(1062317); // For your valor in combating the fallen beast, a special artifact has been bestowed on you.

                    if (!pm.PlaceInBackpack(i))
                    {
                        if (pm.BankBox != null && pm.BankBox.TryDropItem(killer, i, false))
                            pm.SendLocalizedMessage(1079730); // The item has been placed into your bank box.
                        else
                        {
                            pm.SendLocalizedMessage(1072523); // You find an artifact, but your backpack and bank are too full to hold it.
                            i.MoveToWorld(pm.Location, pm.Map);
                        }
                    }

                    bc.GivenSpecialArtifact = true;
                    SetPoints(pm, 0);
                }
            }
        }
    }
}
