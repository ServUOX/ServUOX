using Server.Mobiles;

namespace Server.ContextMenus
{
    public class HeraldSetAnnouncementTextEntry : ContextMenuEntry
    {
        private readonly BaseAttendantHerald m_Attendant;
        public HeraldSetAnnouncementTextEntry(BaseAttendantHerald attendant)
            : base(6247)
        {
            m_Attendant = attendant;
        }

        public override void OnClick()
        {
            if (m_Attendant == null || m_Attendant.Deleted)
                return;

            m_Attendant.SetAnnouncementText(Owner.From);
        }
    }
}
