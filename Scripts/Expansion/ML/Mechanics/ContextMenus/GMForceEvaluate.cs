using Server.Items;

namespace Server.ContextMenus
{
    public class GMForceEvaluate : ContextMenuEntry
    {
        private readonly Aquarium m_Aquarium;

        public GMForceEvaluate(Aquarium aquarium)
            : base(6233, -1)// GM Force Evaluate
        {
            m_Aquarium = aquarium;
        }

        public override void OnClick()
        {
            if (m_Aquarium.Deleted)
                return;

            m_Aquarium.Evaluate();
        }
    }
}
