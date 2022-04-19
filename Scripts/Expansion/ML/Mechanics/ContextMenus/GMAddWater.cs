using Server.Items;

namespace Server.ContextMenus
{
    public class GMAddWater : ContextMenuEntry
    {
        private readonly Aquarium m_Aquarium;

        public GMAddWater(Aquarium aquarium)
            : base(6232, -1)// GM Add Water
        {
            m_Aquarium = aquarium;
        }

        public override void OnClick()
        {
            if (m_Aquarium.Deleted)
                return;

            m_Aquarium.Water.Added += 1;
            m_Aquarium.InvalidateProperties();
        }
    }
}
