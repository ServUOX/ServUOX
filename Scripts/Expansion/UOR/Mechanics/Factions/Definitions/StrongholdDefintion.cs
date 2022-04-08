using System;

namespace Server.Factions
{
    public class StrongholdDefinition
    {
        private readonly Rectangle2D[] m_Area;
        private readonly Point3D m_JoinStone;
        private readonly Point3D m_FactionStone;
        private readonly Point3D[] m_Monoliths;
        private readonly Point3D m_CollectionBox;

        public StrongholdDefinition(Rectangle2D[] area, Point3D joinStone, Point3D factionStone, Point3D[] monoliths, Point3D collectionBox)
        {
            m_Area = area;
            m_JoinStone = joinStone;
            m_FactionStone = factionStone;
            m_Monoliths = monoliths;
            m_CollectionBox = collectionBox;
        }

        public Rectangle2D[] Area => m_Area;
        public Point3D JoinStone => m_JoinStone;
        public Point3D FactionStone => m_FactionStone;
        public Point3D[] Monoliths => m_Monoliths;

        public Point3D CollectionBox => m_CollectionBox;
    }
}