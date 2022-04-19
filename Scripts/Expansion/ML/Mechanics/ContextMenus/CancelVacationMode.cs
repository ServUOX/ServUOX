using Server.Items;

namespace Server.ContextMenus
{
    public class CancelVacationMode : ContextMenuEntry
    {
        private readonly Aquarium m_Aquarium;

        public CancelVacationMode(Aquarium aquarium)
            : base(6240, 2)// Cancel vacation mode
        {
            m_Aquarium = aquarium;
        }

        public override void OnClick()
        {
            if (m_Aquarium.Deleted || !m_Aquarium.HasAccess(Owner.From))
                return;

            Owner.From.SendLocalizedMessage(1074429); // Vacation mode has been cancelled.
            m_Aquarium.VacationLeft = 0;
            m_Aquarium.InvalidateProperties();
        }
    }
}
