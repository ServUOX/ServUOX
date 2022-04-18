using Server.Mobiles;

namespace Server.ContextMenus
{
    public class AttendantDismissEntry : ContextMenuEntry
    {
        private readonly BasePersonalAttendant m_Attendant;
        public AttendantDismissEntry(BasePersonalAttendant attendant)
            : base(6228)
        {
            m_Attendant = attendant;
        }

        public override void OnClick()
        {
            if (m_Attendant == null || m_Attendant.Deleted)
                return;

            m_Attendant.Dismiss(Owner.From);
        }
    }
}
