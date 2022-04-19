using Server.Items;

namespace Server.ContextMenus
{
    public class ViewEventEntry : ContextMenuEntry
    {
        private readonly Aquarium m_Aquarium;

        public ViewEventEntry(Aquarium aquarium)
            : base(6239, 2)// View events
        {
            m_Aquarium = aquarium;
        }

        public override void OnClick()
        {
            if (m_Aquarium.Deleted || !m_Aquarium.HasAccess(Owner.From) || m_Aquarium.Events.Count == 0)
                return;

            Owner.From.SendLocalizedMessage(m_Aquarium.Events[0]);

            if (m_Aquarium.Events[0] == 1074366)
                Owner.From.PlaySound(0x5A2);

            m_Aquarium.Events.RemoveAt(0);
            m_Aquarium.InvalidateProperties();
        }
    }
}
