using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Commands;
using Server.Gumps;
using Server.Network;
using Vertex = Server.Mechanics.GuideHelper.GuideVertex;

namespace Server.Mechanics
{
    public static class GuideHelper
    {
        private static readonly Dictionary<string, List<Vertex>> m_GraphDefinitions = new Dictionary<string, List<Vertex>>();
        private static readonly List<int> m_ShopDefinitions = new List<int>();
        private static readonly char[] m_Separators = new char[] { '\t', ' ' };
        private const string m_Delimiter = "--------------------------------------------------------------------------";
        public static void LogMessage(string line)
        {
            try
            {
                using (var stream = new FileStream("Guide.log", FileMode.Append))
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static Vertex FindVertex(List<Vertex> list, int id)
        {
            return list.FirstOrDefault(v => v.ID == id);
        }

        public static int FindShopName(int id)
        {
            if (id < 0 || id > m_ShopDefinitions.Count)
                return -1;

            return m_ShopDefinitions[id];
        }

        public static void Initialize()
        {
            CommandSystem.Register("GuideEdit", AccessLevel.GameMaster, VertexEdit_OnCommand);

            try
            {
                using (var stream = File.OpenRead(Path.Combine("Data", "Guide", "Definitions.cfg")))
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        switch (string.IsNullOrEmpty(line))
                        {
                            case false when !line.StartsWith("#"):
                            {
                                var split = line.Split(m_Separators, StringSplitOptions.RemoveEmptyEntries);

                                if (split != null && split.Length > 1)
                                    m_ShopDefinitions.Add(int.Parse(split[1]));
                                break;
                            }
                        }
                    }
                }

                foreach (var file in Directory.GetFiles(Path.Combine("Data", "Guide"), "*.graph"))
                {
                    using (var stream = File.OpenRead(file))
                    using (var reader = new StreamReader(stream))
                    {
                        var list = new List<Vertex>();
                        Vertex current = null;
                        Vertex neighbor = null;

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();

                            if (!string.IsNullOrEmpty(line))
                            {
                                var split = line.Split(m_Separators, StringSplitOptions.RemoveEmptyEntries);
                                int num;

                                if (line.StartsWith("N:"))
                                {
                                    if (current != null)
                                    {
                                        for (var i = 1; i < split.Length; i++)
                                        {
                                            num = int.Parse(split[i]);
                                            neighbor = FindVertex(list, num);

                                            if (neighbor == null)
                                            {
                                                neighbor = new Vertex(num);
                                                list.Add(neighbor);
                                            }

                                            current.Vertices.Add(neighbor);
                                        }
                                    }
                                }
                                else if (line.StartsWith("S:"))
                                {
                                    if (current != null)
                                    {
                                        for (var i = 1; i < split.Length; i++)
                                        {
                                            num = int.Parse(split[i]);

                                            if (num >= 0 && num < m_ShopDefinitions.Count)
                                                current.Shops.Add(num);
                                            else
                                                throw new Exception($"Invalid shop ID: {num}");
                                        }
                                    }
                                }
                                else if (line.StartsWith("V:"))
                                {
                                    if (split.Length > 5)
                                    {
                                        num = int.Parse(split[1]);
                                        neighbor = FindVertex(list, num);

                                        if (neighbor != null)
                                            current = neighbor;
                                        else
                                        {
                                            current = new Vertex(num);
                                            list.Add(current);
                                        }

                                        var location = new Point3D
                                        {
                                            X = int.Parse(split[2]),
                                            Y = int.Parse(split[3]),
                                            Z = int.Parse(split[4])
                                        };
                                        current.Location = location;
                                        current.Teleporter = bool.Parse(split[5]);
                                    }
                                    else
                                        throw new Exception(string.Format("Incomplete vertex definition!"));
                                }
                            }
                        }

                        m_GraphDefinitions.Add(Path.GetFileNameWithoutExtension(file), list);
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
                LogMessage(ex.StackTrace);
                LogMessage(m_Delimiter);
            }
        }

        public static void VertexEdit_OnCommand(CommandEventArgs e)
        {
            var m = e.Mobile;

            if (m.Region != null)
            {
                Vertex closest = ClosestVertex(m.Region.Name, m.Location);

                if (closest != null)
                    m.SendGump(new GuideVertexEditGump(closest, m.Map, m.Region.Name));
                else
                    m.SendLocalizedMessage(1076113); // There are no shops nearby.  Please try again when you get to a town or city.
            }
            else
                m.SendLocalizedMessage(1076113); // There are no shops nearby.  Please try again when you get to a town or city.
        }

        public static Vertex ClosestVertex(string town, Point3D location)
        {
            if (town == null || !m_GraphDefinitions.ContainsKey(town))
                return null;

            var vertices = m_GraphDefinitions[town];

            Vertex closest = null;
            double min = int.MaxValue;

            foreach (var v in vertices)
            {
                var distance = Math.Sqrt(Math.Pow(location.X - v.Location.X, 2) + Math.Pow(location.Y - v.Location.Y, 2));

                switch (distance < min)
                {
                    case true:
                        closest = v;
                        min = distance;
                        break;
                }
            }

            return closest;
        }

        public static Dictionary<int, Vertex> FindShops(string town, Point3D location)
        {
            if (town == null || !m_GraphDefinitions.ContainsKey(town))
                return null;

            var vertices = m_GraphDefinitions[town];
            var shops = new Dictionary<int, Vertex>();

            foreach (var v in vertices)
            {
                foreach (var shop in v.Shops)
                {
                    if (shops.ContainsKey(shop))
                    {
                        var d = shops[shop];

                        if (v.DistanceTo(location) < d.DistanceTo(location))
                            shops[shop] = v;
                    }
                    else
                        shops.Add(shop, v);
                }
            }

            return shops.Count > 0 ? shops : null;
        }

        public static List<Vertex> Dijkstra(string town, Vertex source, Vertex destination)
        {
            if (town == null || !m_GraphDefinitions.ContainsKey(town))
                return null;

            var heap = new Heap<Vertex>();
            var path = new List<Vertex>();
            heap.Push(source);

            foreach (var v in m_GraphDefinitions[town])
            {
                v.Distance = int.MaxValue;
                v.Previous = null;
                v.Visited = false;
                v.Removed = false;
            }

            source.Distance = 0;
            while (heap.Count > 0)
            {
                var from = heap.Pop();
                from.Removed = true;

                if (from == destination)
                {
                    while (from != source)
                    {
                        path.Add(from);
                        from = from.Previous;
                    }

                    path.Add(source);
                    return path;
                }

                foreach (var v in from.Vertices)
                {
                    if (!v.Removed)
                    {
                        int dist = from.Distance + (from.Teleporter ? 1 : from.DistanceTo(v));
                        if (dist < v.Distance)
                        {
                            v.Distance = dist;
                            v.Previous = from;

                            if (!v.Visited)
                                heap.Push(v);
                            else
                                heap.Fix(v);
                        }
                    }
                }
            }

            return null;
        }

        public class GuideVertexEditGump : Gump
        {
            private readonly Vertex m_Vertex;
            private readonly Map m_Map;
            private readonly string m_Town;
            private readonly Item m_Item;
            public GuideVertexEditGump(Vertex vertex, Map map, string town)
                : base(50, 50)
            {
                m_Vertex = vertex;
                m_Map = map;
                m_Town = town;

                Closable = true;
                Disposable = true;
                Dragable = true;
                Resizable = false;

                var size = m_ShopDefinitions.Count;
                AddPage(0);
                AddBackground(0, 0, 540, 35 + size * 30 / 2, 9200);
                AddAlphaRegion(15, 10, 510, 15 + size * 30 / 2);

                for (var i = 0; i < size; i += 2)
                {
                    var on = m_Vertex.Shops.Contains(i);
                    AddButton(25, 25 + i * 30 / 2, on ? 2361 : 2360, on ? 2360 : 2361, i + 1, GumpButtonType.Reply, 0);
                    AddHtmlLocalized(50, 20 + i * 30 / 2, 200, 20, m_ShopDefinitions[i], 0x7773, false, false);

                    switch (i + 1 < size)
                    {
                        case true:
                            @on = m_Vertex.Shops.Contains(i + 1);
                            AddButton(280, 25 + i * 30 / 2, @on ? 2361 : 2360, @on ? 2360 : 2361, i + 2, GumpButtonType.Reply, 0);
                            AddHtmlLocalized(305, 20 + i * 30 / 2, 200, 20, m_ShopDefinitions[i + 1], 0x7773, false, false);
                            break;
                    }
                }

                m_Item = new Item(0x1183)
                {
                    Visible = false
                };
                m_Item.MoveToWorld(m_Vertex.Location, map);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (info.ButtonID > 0)
                {
                    if (m_Vertex.Shops.Contains(info.ButtonID - 1))
                        m_Vertex.Shops.Remove(info.ButtonID - 1);
                    else
                        m_Vertex.Shops.Add(info.ButtonID - 1);

                    sender.Mobile.SendGump(new GuideVertexEditGump(m_Vertex, m_Map, m_Town));
                }
                else if (m_Item != null && !m_Item.Deleted)
                {
                    m_Item.Delete();
                    Save(m_Town);
                }
            }

            private void Save(string town)
            {
                if (!m_GraphDefinitions.ContainsKey(town))
                    return;

                var list = m_GraphDefinitions[town];
                var path = Core.BaseDirectory + $"\\Data\\Guide\\{town}.graph";

                using (var stream = new FileStream(path, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("# Graph vertices");
                    writer.WriteLine("# {V:}VertexID{tab, }X{tab, }Y{tab, }Z{tab, }IsTeleporter");
                    writer.WriteLine("# {S:}ShopID{tab, }ShopID{tab, }...");
                    writer.WriteLine("# {N:}VertexID{tab, }VertexID{tab, }...");

                    foreach (var v in list)
                    {
                        writer.WriteLine(
                            $"V:\t{v.ID}\t{v.Location.X}\t{v.Location.Y}\t{v.Location.Z}\t{v.Teleporter.ToString()}");

                        if (v.Shops.Count > 0)
                        {
                            writer.Write("S:");

                            foreach (int i in v.Shops)
                                writer.Write($"\t{i}");

                            writer.WriteLine();
                        }

                        switch (v.Vertices.Count > 0)
                        {
                            case true:
                            {
                                writer.Write("N:");

                                foreach (Vertex n in v.Vertices)
                                    writer.Write($"\t{n.ID}");

                                writer.WriteLine();
                                break;
                            }
                        }
                    }
                }
            }
        }

        public class GuideVertex : IComparable<Vertex>
        {
            private Point3D m_Location;

            public GuideVertex(int id)
                : this(id, Point3D.Zero)
            {
            }

            public GuideVertex(int id, Point3D location)
            {
                ID = id;
                m_Location = location;
                Previous = null;
                Distance = int.MaxValue;
                Visited = false;
                Removed = false;
            }

            public int ID { get; }

            public Point3D Location
            {
                get => m_Location;
                set => m_Location = value;
            }
            public List<Vertex> Vertices { get; } = new List<Vertex>();

            public List<int> Shops { get; } = new List<int>();

            public bool Teleporter { get; set; }

            public Vertex Previous { get; set; }

            public int Distance { get; set; }

            public bool Visited { get; set; }

            public bool Removed { get; set; }

            public int DistanceTo(Vertex to)
            {
                return Math.Abs(m_Location.X - to.Location.X) + Math.Abs(m_Location.Y - to.Location.Y);
            }

            public int DistanceTo(Point3D to)
            {
                return Math.Abs(m_Location.X - to.X) + Math.Abs(m_Location.Y - to.Y);
            }

            public int CompareTo(Vertex o)
            {
                if (o != null)
                    return Distance - o.Distance;

                return 0;
            }
        }

        public class Heap<T> where T : IComparable<T>
        {
            private readonly List<T> m_List;
            public Heap()
            {
                m_List = new List<T>();
            }

            public int Count => m_List.Count;
            public T Top => m_List[0];
            public bool Contains(T item)
            {
                return m_List.Contains(item);
            }

            public void Push(T item)
            {
                m_List.Add(item);

                var child = m_List.Count - 1;
                var parent = (child - 1) / 2;
                T temp;

                while (item.CompareTo(m_List[parent]) < 0 && child > 0)
                {
                    temp = m_List[child];
                    m_List[child] = m_List[parent];
                    m_List[parent] = temp;

                    child = parent;
                    parent = (child - 1) / 2;
                }
            }

            public void Fix(T item)
            {
                var child = m_List.IndexOf(item);
                var parent = (child - 1) / 2;
                T temp;

                while (item.CompareTo(m_List[parent]) < 0 && child > 0)
                {
                    temp = m_List[child];
                    m_List[child] = m_List[parent];
                    m_List[parent] = temp;

                    child = parent;
                    parent = (child - 1) / 2;
                }
            }

            public T Pop()
            {
                if (m_List.Count == 0)
                    return default(T);

                var top = m_List[0];
                T temp;

                m_List[0] = m_List[m_List.Count - 1];
                m_List.RemoveAt(m_List.Count - 1);

                var parent = 0;
                int lchild;
                int rchild;

                bool ltl, ltr, cltc;

                do
                {
                    lchild = parent * 2 + 1;
                    rchild = parent * 2 + 2;
                    ltl = ltr = cltc = false;

                    if (m_List.Count > lchild)
                        ltl = m_List[parent].CompareTo(m_List[lchild]) > 0;

                    if (m_List.Count > rchild)
                        ltr = m_List[parent].CompareTo(m_List[rchild]) > 0;

                    if (ltl && ltr)
                        cltc = m_List[lchild].CompareTo(m_List[rchild]) > 0;

                    if (ltl && !ltr)
                    {
                        temp = m_List[parent];
                        m_List[parent] = m_List[lchild];
                        m_List[lchild] = temp;

                        parent = lchild;
                    }
                    else if (!ltl && ltr)
                    {
                        temp = m_List[parent];
                        m_List[parent] = m_List[rchild];
                        m_List[rchild] = temp;

                        parent = rchild;
                    }
                    else if (ltl && ltr && cltc)
                    {
                        temp = m_List[parent];
                        m_List[parent] = m_List[rchild];
                        m_List[rchild] = temp;

                        parent = rchild;
                    }
                    else if (ltl && ltr)
                    {
                        temp = m_List[parent];
                        m_List[parent] = m_List[lchild];
                        m_List[lchild] = temp;

                        parent = lchild;
                    }
                }
                while (ltl || ltr);

                return top;
            }
        }
    }
}
