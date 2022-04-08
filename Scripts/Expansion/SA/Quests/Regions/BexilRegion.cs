using Server.Items;
using Server.Mobiles;
using System.Linq;

namespace Server.Regions
{
    public class BexilRegion : BaseRegion
    {
        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return false;
        }

        public static void Initialize()
        {
            if (Core.SA)
            {
                new BexilRegion();
            }
        }

        public BexilRegion()
            : base("Bexil Region", Map.TerMur, DefaultPriority, new Rectangle2D(386, 3356, 35, 51))
        {
            Register();
            SetupRegion();
        }

        private void SetupRegion()
        {
            var map = Map.TerMur;

            for (int x = 390; x < 408; x++)
            {
                int z = map.GetAverageZ(x, 3360);

                if (map.FindItem<Blocker>(new Point3D(x, 3360, z)) == null)
                {
                    var blocker = new Blocker();
                    blocker.MoveToWorld(new Point3D(x, 3360, z), map);
                }
            }

            if (!GetEnumeratedMobiles().Any(m => m is BexilPunchingBag && !m.Deleted))
            {
                var bex = new BexilPunchingBag();
                bex.MoveToWorld(new Point3D(403, 3391, 38), Map.TerMur);
            }
        }

        public override bool CheckTravel(Mobile traveller, Point3D p, Server.Spells.TravelCheckType type)
        {
            return false;
        }
    }

}
