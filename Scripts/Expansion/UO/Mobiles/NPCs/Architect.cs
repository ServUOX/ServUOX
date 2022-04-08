using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Architect : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructible]
        public Architect()
            : base("the architect")
        {
        }

        public Architect(Serial serial)
            : base(serial)
        {
        }

        public override NpcGuild NpcGuild => NpcGuild.TinkersGuild;
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override void InitSBInfo()
        {
            if (!Core.AOS)
                m_SBInfos.Add(new SBHouseDeed());

            m_SBInfos.Add(new SBArchitect());
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
