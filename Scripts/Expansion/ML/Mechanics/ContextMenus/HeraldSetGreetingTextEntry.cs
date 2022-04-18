using Server.Mobiles;

namespace Server.ContextMenus
{
    public class HeraldSetGreetingTextEntry : ContextMenuEntry
    {
        private readonly BaseAttendantHerald m_Attendant;
        public HeraldSetGreetingTextEntry(BaseAttendantHerald attendant)
            : base(6246)
        {
            m_Attendant = attendant;
        }

        public override void OnClick()
        {
            if (m_Attendant == null || m_Attendant.Deleted)
                return;

            m_Attendant.SetGreetingText(Owner.From);
        }
    }
}
