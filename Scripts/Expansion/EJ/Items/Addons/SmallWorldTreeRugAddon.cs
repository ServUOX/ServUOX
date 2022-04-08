using Server.Gumps;

namespace Server.Items
{
    public class SmallWorldTreeRugAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new SmallWorldTreeRugAddonDeed();

        [Constructible]
        public SmallWorldTreeRugAddon()
            : this(true)
        {
        }

        [Constructible]
        public SmallWorldTreeRugAddon(bool south)
        {
            if (south)
            {
                for (int i = 0; i < m_SouthInfo.Length / 4; i++)
                {
                    AddComponent(new AddonComponent(m_SouthInfo[i, 0]), m_SouthInfo[i, 1], m_SouthInfo[i, 2], m_SouthInfo[i, 3]);
                }
            }
            else
            {
                for (int i = 0; i < m_EastInfo.Length / 4; i++)
                {
                    AddComponent(new AddonComponent(m_EastInfo[i, 0]), m_EastInfo[i, 1], m_EastInfo[i, 2], m_EastInfo[i, 3]);
                }
            }
        }

        private static int[,] m_SouthInfo = new int[,]
        {
              {40514, 1, 1, 0}, {40513, 1, 0, 0}, {40512, 1, -1, 0}// 1	2	3	
			, {40511, 0, -1, 0}, {40510, -1, -1, 0}, {40509, -1, 0, 0}// 4	5	6	
			, {40508, -1, 1, 0}, {40507, 0, 1, 0}, {40506, 0, 0, 0}// 7	8	9	
		};

        private static int[,] m_EastInfo = new int[,]
        {
              {40523, 1, 1, 0}, {40522, 0, 1, 0}, {40521, -1, 1, 0}// 1	2	3	
			, {40520, -1, 0, 0}, {40519, -1, -1, 0}, {40518, 0, -1, 0}// 4	5	6	
			, {40517, 1, -1, 0}, {40516, 1, 0, 0}, {40515, 0, 0, 0}// 7	8	9	
	    };

        public SmallWorldTreeRugAddon(Serial serial)
            : base(serial)
        {
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
