using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Mechanics;
using Server.Network;
using Vertex = Server.Mechanics.GuideHelper.GuideVertex;

namespace Server.Mobiles
{
    public class BaseAttendantGuide : BasePersonalAttendant
    {
        private List<Vertex> m_Path;
        public BaseAttendantGuide()
            : base("the Guide")
        {
            m_Path = null;
        }

        public BaseAttendantGuide(Serial serial)
            : base(serial)
        {
        }

        public List<Vertex> Path => m_Path;
        public override void OnDoubleClick(Mobile from)
        {
            if (from.Alive && IsOwner(from))
            {
                Dictionary<int, Vertex> m_Shops = GuideHelper.FindShops(Region?.Name, Location);

                if (m_Shops != null)
                {
                    from.CloseGump(typeof(InternalGump));
                    from.SendGump(new InternalGump(this, m_Shops));
                }
                else
                    from.SendLocalizedMessage(1076113); // There are no shops nearby.  Please try again when you get to a town or city.
            }
        }

        public override void AddCustomContextEntries(Mobile from, List<ContextMenuEntry> list)
        {
            if (from.Alive && IsOwner(from))
                list.Add(new AttendantUseEntry(this, 6249));

            base.AddCustomContextEntries(from, list);
        }

        public override void OnThink()
        {
            base.OnThink();

            if (ControlMaster != null && m_Path != null && m_Path.Count > 0)
            {
                Vertex v = m_Path[m_Path.Count - 1];
                Mobile m = ControlMaster;

                if (m.NetState != null)
                {
                    if (Math.Abs(v.DistanceTo(m.Location) - v.DistanceTo(Location)) > 10)
                        Frozen = true;
                    else
                        Frozen = false;

                    if (CurrentWayPoint == null)
                    {
                        CurrentWayPoint = new WayPoint();
                        CurrentWayPoint.MoveToWorld(v.Location, Map);
                    }

                    int dist = v.DistanceTo(Location);

                    if (dist < (v.Teleporter ? 1 : 3) || dist > 100)
                    {
                        m_Path.RemoveAt(m_Path.Count - 1);

                        if (m_Path.Count > 0)
                        {
                            if (CurrentWayPoint == null)
                                CurrentWayPoint = new WayPoint();

                            CurrentWayPoint.MoveToWorld(m_Path[m_Path.Count - 1].Location, Map);
                        }
                        else
                        {
                            Timer.DelayCall(TimeSpan.FromSeconds(3), new TimerStateCallback<Mobile>(CommandFollow), m);
                            Say(1076051); // We have reached our destination
                            CommandStop(m);
                        }
                    }
                }
            }
        }

        public override void CommandFollow(Mobile by)
        {
            StopGuiding();

            base.CommandFollow(by);
        }

        public override void CommandStop(Mobile by)
        {
            StopGuiding();

            base.CommandStop(by);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }

        public void StopGuiding()
        {
            CurrentSpeed = PassiveSpeed;

            if (CurrentWayPoint != null)
                CurrentWayPoint.Delete();

            if (m_Path != null)
                m_Path.Clear();

            Controlled = true;
            m_Path = null;
        }

        public void StartGuiding(List<Vertex> path)
        {
            m_Path = path;

            if (ControlMaster != null && m_Path != null && m_Path.Count > 0)
            {
                if (m_Path.Count > 1)
                    m_Path.RemoveAt(m_Path.Count - 1);

                if (CurrentWayPoint == null)
                    CurrentWayPoint = new WayPoint();

                CurrentWayPoint.MoveToWorld(m_Path[m_Path.Count - 1].Location, Map);

                AIObject.Action = ActionType.Wander;
                CurrentSpeed = ActiveSpeed;
                Controlled = false;
                Say(1076114); // Please follow me.
            }
        }

        private class InternalGump : Gump
        {
            private const int ShopsPerPage = 10;
            private const int ShopHeight = 24;
            private readonly BaseAttendantGuide m_Guide;
            private readonly Dictionary<int, Vertex> m_Shops;
            public InternalGump(BaseAttendantGuide guide, Dictionary<int, Vertex> shops)
                : base(60, 36)
            {
                m_Guide = guide;
                m_Shops = shops;

                AddPage(0);

                AddBackground(0, 0, 273, 84 + ShopsPerPage * ShopHeight, 0x13BE);
                AddImageTiled(10, 10, 253, 20, 0xA40);
                AddImageTiled(10, 40, 253, 4 + ShopsPerPage * ShopHeight, 0xA40);
                AddImageTiled(10, 54 + ShopsPerPage * ShopHeight, 253, 20, 0xA40);
                AddAlphaRegion(10, 10, 253, 64 + ShopsPerPage * ShopHeight);
                AddButton(10, 54 + ShopsPerPage * ShopHeight, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
                AddHtmlLocalized(45, 54 + ShopsPerPage * ShopHeight, 450, 20, 1060051, 0x7FFF, false, false); // CANCEL
                AddHtmlLocalized(14, 12, 273, 20, 1076029, 0x7FFF, false, false); // What sort of shop do you seek?

                int i = 0;
                int page = 0;
                int iPage = 0;

                foreach (KeyValuePair<int, Vertex> kvp in shops)
                {
                    if (i % ShopsPerPage == 0)
                    {
                        if (page > 0)
                        {
                            AddButton(188, 54 + ShopsPerPage * ShopHeight, 0xFA5, 0xFA7, 0, GumpButtonType.Page, page + 1);
                            AddHtmlLocalized(228, 56 + ShopsPerPage * ShopHeight, 60, 20, 1043353, 0x7FFF, false, false); // Next
                        }

                        AddPage(page + 1);

                        if (page > 0)
                        {
                            AddButton(113, 54 + ShopsPerPage * ShopHeight, 0xFAE, 0xFB0, 0, GumpButtonType.Page, page);
                            AddHtmlLocalized(153, 56 + ShopsPerPage * ShopHeight, 60, 20, 1011393, 0x7FFF, false, false); // Back
                        }

                        iPage = 0;
                        page++;
                    }

                    AddButton(19, 49 + iPage * ShopHeight, 0x845, 0x846, 100 + kvp.Key, GumpButtonType.Reply, 0);
                    AddHtmlLocalized(44, 47 + iPage * ShopHeight, 213, 20, GuideHelper.FindShopName(kvp.Key), 0x7FFF, false, false);

                    i++;
                    iPage++;
                }
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                int shop = info.ButtonID - 100;

                if (m_Guide == null || m_Guide.Deleted || m_Guide.Region == null || info.ButtonID == 0)
                    return;

                Vertex source = GuideHelper.ClosestVertex(m_Guide.Region.Name, m_Guide.Location);

                if (m_Shops.ContainsKey(shop))
                {
                    Vertex destination = m_Shops[shop];
                    List<Vertex> path = GuideHelper.Dijkstra(m_Guide.Region.Name, source, destination);
                    m_Guide.StartGuiding(path);
                }
            }
        }
    }
}
