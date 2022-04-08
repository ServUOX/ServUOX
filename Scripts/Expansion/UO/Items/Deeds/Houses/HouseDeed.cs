using System.Collections;
using Server.Multis;
using Server.Regions;
using Server.Targeting;

namespace Server.Items
{
    public class HousePlacementTarget : MultiTarget
    {
        private readonly HouseDeed m_Deed;
        public HousePlacementTarget(HouseDeed deed)
            : base(deed.MultiID, deed.Offset)
        {
            m_Deed = deed;
        }

        protected override void OnTarget(Mobile from, object o)
        {
            if (o is IPoint3D ip)
            {
                if (ip is Item)
                    ip = ((Item)ip).GetWorldTop();

                Point3D p = new Point3D(ip);

                Region reg = Region.Find(new Point3D(p), from.Map);

                if (from.AccessLevel >= AccessLevel.GameMaster || reg.AllowHousing(from, p))
                    m_Deed.OnPlacement(from, p);
                else if (reg.IsPartOf<TempNoHousingRegion>())
                    from.SendLocalizedMessage(501270); // Lord British has decreed a 'no build' period, thus you cannot build this house at this time.
                else if (reg.IsPartOf<TreasureRegion>() || reg.IsPartOf<HouseRegion>())
                    from.SendLocalizedMessage(1043287); // The house could not be created here.  Either something is blocking the house, or the house would not be on valid terrain.
                else if (reg.IsPartOf<HouseRaffleRegion>())
                    from.SendLocalizedMessage(1150493); // You must have a deed for this plot of land in order to build here.
                else
                    from.SendLocalizedMessage(501265); // Housing can not be created in this area.
            }
        }
    }

    public abstract class HouseDeed : Item
    {
        private Point3D m_Offset;
        public HouseDeed(int id, Point3D offset)
            : base(0x14F0)
        {
            Weight = 1.0;
            LootType = LootType.Newbied;

            MultiID = id;
            m_Offset = offset;
        }

        public HouseDeed(Serial serial)
            : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MultiID { get; set; }
        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D Offset
        {
            get
            {
                return m_Offset;
            }
            set
            {
                m_Offset = value;
            }
        }
        public abstract Rectangle2D[] Area { get; }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1);

            writer.Write(m_Offset);

            writer.Write(MultiID);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_Offset = reader.ReadPoint3D();

                        goto case 0;
                    }
                case 0:
                    {
                        MultiID = reader.ReadInt();

                        break;
                    }
            }

            if (Weight == 0.0)
                Weight = 1.0;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else
            {
                from.SendLocalizedMessage(1010433); /* House placement cancellation could result in a
                * 60 second delay in the return of your deed.
                */

                from.Target = new HousePlacementTarget(this);
            }
        }

        public abstract BaseHouse GetHouse(Mobile owner);

        public void OnPlacement(Mobile from, Point3D p)
        {
            if (Deleted)
                return;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else
            {
                Point3D center = new Point3D(p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z);
                HousePlacementResult res = HousePlacement.Check(from, MultiID, center, out ArrayList toMove);

                switch (res)
                {
                    case HousePlacementResult.Valid:
                        {
                            if (from.AccessLevel > AccessLevel.Player || BaseHouse.CheckAccountHouseLimit(from))
                            {
                                BaseHouse house = GetHouse(from);
                                house.MoveToWorld(center, from.Map);
                                Delete();

                                for (int i = 0; i < toMove.Count; ++i)
                                {
                                    object o = toMove[i];

                                    if (o is Mobile)
                                        ((Mobile)o).Location = house.BanLocation;
                                    else if (o is Item)
                                        ((Item)o).Location = house.BanLocation;
                                }
                            }
                            break;
                        }
                    case HousePlacementResult.BadItem:
                    case HousePlacementResult.BadLand:
                    case HousePlacementResult.BadStatic:
                    case HousePlacementResult.BadRegionHidden:
                        {
                            from.SendLocalizedMessage(1043287); // The house could not be created here.  Either something is blocking the house, or the house would not be on valid terrain.
                            break;
                        }
                    case HousePlacementResult.NoSurface:
                        {
                            from.SendMessage("The house could not be created here.  Part of the foundation would not be on any surface.");
                            break;
                        }
                    case HousePlacementResult.BadRegion:
                        {
                            from.SendLocalizedMessage(501265); // Housing cannot be created in this area.
                            break;
                        }
                    case HousePlacementResult.BadRegionTemp:
                        {
                            from.SendLocalizedMessage(501270); //Lord British has decreed a 'no build' period, thus you cannot build this house at this time.
                            break;
                        }
                    case HousePlacementResult.BadRegionRaffle:
                        {
                            from.SendLocalizedMessage(1150493); // You must have a deed for this plot of land in order to build here.
                            break;
                        }
                    case HousePlacementResult.NoQueenLoyalty:
                        {
                            from.SendLocalizedMessage(1113707, "10000"); // You must have at lease ~1_MIN~ loyalty to the Gargoyle Queen to place a house in Ter Mur.
                            break;
                        }
                }
            }
        }
    }
}
