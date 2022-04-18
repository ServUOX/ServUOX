using Server.Mobiles;

namespace Server.ContextMenus
{
    public class AttendantUseEntry : ContextMenuEntry
    {
        private readonly BasePersonalAttendant m_Attendant;
        public AttendantUseEntry(BasePersonalAttendant attendant, int title)
            : base(title)
        {
            m_Attendant = attendant;
        }

        public override void OnClick()
        {
            if (m_Attendant == null || m_Attendant.Deleted)
                return;

            m_Attendant.OnDoubleClick(Owner.From);
        }
    }
}
