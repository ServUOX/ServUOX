using Server.Items;

namespace Server.ContextMenus
{
    public class ExamineEntry : ContextMenuEntry
    {
        private readonly Aquarium m_Aquarium;

        public ExamineEntry(Aquarium aquarium)
            : base(6235, 2)// Examine Aquarium
        {
            m_Aquarium = aquarium;
        }

        public override void OnClick()
        {
            if (m_Aquarium.Deleted)
                return;

            m_Aquarium.ExamineAquarium(Owner.From);
        }
    }
}
