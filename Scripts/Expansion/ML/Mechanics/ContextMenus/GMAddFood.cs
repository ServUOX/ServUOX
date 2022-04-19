using Server.Items;

namespace Server.ContextMenus
{
    public class GMAddFood : ContextMenuEntry
    {
        private readonly Aquarium m_Aquarium;

        public GMAddFood(Aquarium aquarium)
            : base(6231, -1)// GM Add Food
        {
            m_Aquarium = aquarium;
        }

        public override void OnClick()
        {
            if (m_Aquarium.Deleted)
                return;

            m_Aquarium.Food.Added += 1;
            m_Aquarium.InvalidateProperties();
        }
    }
}
