using Server.Items;
using Server.Gumps;

namespace Server.ContextMenus
{
    public class GMOpen : ContextMenuEntry
    {
        private readonly Aquarium m_Aquarium;

        public GMOpen(Aquarium aquarium)
            : base(6234, -1)// GM Open Container
        {
            m_Aquarium = aquarium;
        }

        public override void OnClick()
        {
            if (m_Aquarium.Deleted)
                return;

            Owner.From.SendGump(new AquariumGump(m_Aquarium, true));
        }
    }
}
