using System;

namespace Server.Ethics
{
    [TypeAlias("Server.Factions.EthicsPersistance")]
    public class EthicsPersistence : Item
    {
        private static EthicsPersistence m_Instance;
        [Constructible]
        public EthicsPersistence()
            : base(1)
        {
            Movable = false;

            if (m_Instance == null || m_Instance.Deleted)
                m_Instance = this;
            else
                base.Delete();
        }

        public EthicsPersistence(Serial serial)
            : base(serial)
        {
            m_Instance = this;
        }

        public static EthicsPersistence Instance => m_Instance;
        public override string DefaultName => "Ethics Persistence - Internal";
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            for (int i = 0; i < Ethic.Ethics.Length; ++i)
                Ethic.Ethics[i].Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        for (int i = 0; i < Ethic.Ethics.Length; ++i)
                            Ethic.Ethics[i].Deserialize(reader);

                        break;
                    }
            }
        }

        public override void Delete()
        {
        }
    }
}
