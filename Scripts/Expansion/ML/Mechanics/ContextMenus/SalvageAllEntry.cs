using Server.Items;
using Server.Network;

namespace Server.ContextMenus
{
    public class SalvageAllEntry : ContextMenuEntry
    {
        private readonly SalvageBag m_Bag;

        public SalvageAllEntry(SalvageBag bag, bool enabled)
            : base(6276)
        {
            m_Bag = bag;

            if (!enabled)
                Flags |= CMEFlags.Disabled;
        }

        public override void OnClick()
        {
            if (m_Bag.Deleted)
                return;

            var from = Owner.From;

            if (from.CheckAlive())
                m_Bag.SalvageAll(from);
        }
    }
}
