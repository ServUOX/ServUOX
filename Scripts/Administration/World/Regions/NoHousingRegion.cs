using System.Xml;

namespace Server.Regions
{
    public class NoHousingRegion : BaseRegion
    {
        // False: this uses 'stupid OSI' house placement checking: part of the house may be placed here provided that the center is not in the region
        // True: this uses 'smart ServUOX' house placement checking: no part of the house may be in the region

        private bool m_SmartChecking;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SmartChecking => m_SmartChecking;

        public NoHousingRegion(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {
            ReadBoolean(xml["smartNoHousing"], "active", ref m_SmartChecking, false);
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return SmartChecking;
        }
    }
}
