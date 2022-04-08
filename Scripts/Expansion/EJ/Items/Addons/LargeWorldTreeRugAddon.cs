using Server.Gumps;

namespace Server.Items
{
    public class LargeWorldTreeRugAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new LargeWorldTreeRugAddonDeed();

        [Constructible]
        public LargeWorldTreeRugAddon()
            : this(true)
        {
        }

        [Constructible]
        public LargeWorldTreeRugAddon(bool south)
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
              {40546, -2, -1, 0}, {40545, -1, -1, 0}, {40544, -2, 0, 0}// 1	2	3	
			, {40543, -1, 0, 0}, {40542, -2, 1, 0}, {40541, -1, 1, 0}// 4	5	6	
			, {40540, -2, 2, 0}, {40539, -1, 2, 0}, {40536, 0, -1, 0}// 7	8	9	
			, {40534, 1, -1, 0}, {40533, 2, -1, 0}, {40532, 0, 0, 0}// 10	11	12	
			, {40531, 1, 0, 0}, {40530, 0, 1, 0}, {40529, 2, 0, 0}// 13	14	15	
			, {40528, 1, 1, 0}, {40527, 0, 2, 0}, {40526, 2, 1, 0}// 16	17	18	
			, {40525, 1, 2, 0}, {40547, -1, -2, 0}, {40538, 0, -2, 0}// 19	20	21	
			, {40537, 1, -2, 0}, {40535, 2, -2, 0}// 22	23	
		};

        private static int[,] m_EastInfo = new int[,]
        {
              {40572, -2, -1, 0}, {40571, -1, -2, 0}, {40570, -1, -1, 0}// 1	2	3	
			, {40569, 0, -2, 0}, {40568, 0, -1, 0}, {40567, 1, -2, 0}// 4	5	6	
			, {40566, 1, -1, 0}, {40565, 2, -2, 0}, {40564, 2, -1, 0}// 7	8	9	
			, {40563, -2, 0, 0}, {40562, -2, 1, 0}, {40561, -1, 0, 0}// 10	11	12	
			, {40560, -2, 2, 0}, {40559, -1, 1, 0}, {40558, -1, 2, 0}// 13	14	15	
			, {40557, 0, 0, 0}, {40556, 0, 1, 0}, {40555, 1, 0, 0}// 16	17	18	
			, {40551, 1, 2, 0}, {40554, 0, 2, 0}, {40553, 1, 1, 0}// 19	20	21	
			, {40552, 2, 0, 0}, {40550, 2, 1, 0}// 22	23	
		};

        public LargeWorldTreeRugAddon(Serial serial)
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
