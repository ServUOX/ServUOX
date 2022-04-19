using Server.Engines.Quests;
using Server.Mobiles;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Server.Mechanics
{
    public class CollectionsSystem
    {
        private static readonly Dictionary<Collection, CollectionData> m_Collections = new Dictionary<Collection, CollectionData>();
        private static List<BaseCollectionMobile> m_Mobiles = new List<BaseCollectionMobile>();
        private static readonly string m_Path = Path.Combine("Saves", "CommunityCollections.bin");

        public static void Configure()
        {
            EventSink.WorldSave += EventSink_WorldSave;
            EventSink.WorldLoad += EventSink_WorldLoad;
        }

        public static void RegisterMobile(BaseCollectionMobile mob)
        {
            switch (m_Mobiles.Contains(mob))
            {
                case false:
                {
                    m_Mobiles.Add(mob);
                    if (m_Collections.ContainsKey(mob.CollectionID))
                        mob.SetData(m_Collections[mob.CollectionID]);
                    break;
                }
            }
        }

        public static void UnregisteredMobile(BaseCollectionMobile mob)
        {
            m_Collections[mob.CollectionID] = mob.GetData();
            m_Mobiles.Remove(mob);
        }

        private static void EventSink_WorldSave(WorldSaveEventArgs e)
        {
            var newMobiles = new List<BaseCollectionMobile>();
            foreach (var mob in m_Mobiles)
            {
                if (!mob.Deleted)
                    newMobiles.Add(mob);
            }
            m_Mobiles = newMobiles;

            Persistence.Serialize(
                m_Path,
                writer =>
                {
                    writer.WriteMobileList(m_Mobiles);
                    writer.Write(m_Mobiles.Count);
                    foreach (var mob in m_Mobiles)
                    {
                        writer.Write((int)mob.CollectionID);
                        var data = mob.GetData();
                        data.Write(writer);
                        m_Collections[mob.CollectionID] = data;
                    }
                });
        }

        private static void EventSink_WorldLoad()
        {
            Persistence.Deserialize(
                m_Path,
                reader =>
                {
                    m_Mobiles.AddRange(reader.ReadMobileList().Cast<BaseCollectionMobile>());
                    var mobs = new List<BaseCollectionMobile>();
                    mobs.AddRange(m_Mobiles);

                    var count = reader.ReadInt();
                    for (var i = 0; i < count; ++i)
                    {
                        var collection = reader.ReadInt();
                        var data = new CollectionData();
                        data.Read(reader);
                        var toRemove = -1;
                        foreach (var mob in mobs.Where(mob => mob.CollectionID == (Collection)collection))
                        {
                            mob.SetData(data);
                            toRemove = mobs.IndexOf(mob);
                            break;
                        }
                        if (toRemove >= 0)
                            mobs.RemoveAt(toRemove);
                    }
                });

        }
    }

    public class CollectionData
    {
        public Collection Collection;
        public long Points;
        public long StartTier;
        public long NextTier;
        public long DailyDecay;
        public int Tier;
        public object DonationTitle;
        public List<List<object>> Tiers = new List<List<object>>();

        public void Write(GenericWriter writer)
        {
            writer.Write(0); // version

            writer.Write((int)Collection);
            writer.Write(Points);
            writer.Write(StartTier);
            writer.Write(NextTier);
            writer.Write(DailyDecay);
            writer.Write(Tier);

            QuestWriter.Object(writer, DonationTitle);

            writer.Write(Tiers.Count);

            foreach (var t in Tiers)
            {
                writer.Write(t.Count);

                for (var j = 0; j < t.Count; j++)
                    QuestWriter.Object(writer, t[j]);
            }
        }

        public void Read(GenericReader reader)
        {
            _ = reader.ReadInt();

            Collection = (Collection)reader.ReadInt();
            Points = reader.ReadLong();
            StartTier = reader.ReadLong();
            NextTier = reader.ReadLong();
            DailyDecay = reader.ReadLong();
            Tier = reader.ReadInt();

            DonationTitle = QuestReader.Object(reader);

            for (var i = reader.ReadInt(); i > 0; i--)
            {
                var list = new List<object>();

                for (var j = reader.ReadInt(); j > 0; j--)
                    list.Add(QuestReader.Object(reader));

                Tiers.Add(list);
            }
        }
    }
}
