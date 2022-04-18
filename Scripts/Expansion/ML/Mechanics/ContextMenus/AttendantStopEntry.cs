using Server.Mobiles;

namespace Server.ContextMenus
{
    public class AttendantStopEntry : ContextMenuEntry
    {
        private readonly BasePersonalAttendant m_Attendant;
        public AttendantStopEntry(BasePersonalAttendant attendant)
            : base(6112)
        {
            m_Attendant = attendant;
        }

        public override void OnClick()
        {
            if (m_Attendant == null || m_Attendant.Deleted)
                return;

            m_Attendant.CommandStop(Owner.From);
        }
    }
}
