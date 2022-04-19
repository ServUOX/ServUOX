using Server.Items;

namespace Server.ContextMenus
{
    public class GMFill : ContextMenuEntry
    {
        private readonly Aquarium m_Aquarium;

        public GMFill(Aquarium aquarium)
            : base(6236, -1)// GM Fill Food and Water
        {
            m_Aquarium = aquarium;
        }

        public override void OnClick()
        {
            if (m_Aquarium.Deleted)
                return;

            m_Aquarium.Food.Added = m_Aquarium.Food.Maintain;
            m_Aquarium.Water.Added = m_Aquarium.Water.Maintain;
            m_Aquarium.InvalidateProperties();
        }
    }
}
