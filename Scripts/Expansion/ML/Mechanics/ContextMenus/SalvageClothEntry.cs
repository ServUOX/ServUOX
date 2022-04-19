using Server.Items;
using Server.Network;

namespace Server.ContextMenus
{
    public class SalvageClothEntry : ContextMenuEntry
    {
        private readonly SalvageBag m_Bag;

        public SalvageClothEntry(SalvageBag bag, bool enabled)
            : base(6278)
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
                m_Bag.SalvageCloth(from);
        }
    }
}
