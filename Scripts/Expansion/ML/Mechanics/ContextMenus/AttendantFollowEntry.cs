using Server.Mobiles;

namespace Server.ContextMenus
{
    public class AttendantFollowEntry : ContextMenuEntry
    {
        private readonly BasePersonalAttendant m_Attendant;
        public AttendantFollowEntry(BasePersonalAttendant attendant)
            : base(6108)
        {
            m_Attendant = attendant;
        }

        public override void OnClick()
        {
            if (m_Attendant == null || m_Attendant.Deleted)
                return;

            m_Attendant.CommandFollow(Owner.From);
        }
    }
}
