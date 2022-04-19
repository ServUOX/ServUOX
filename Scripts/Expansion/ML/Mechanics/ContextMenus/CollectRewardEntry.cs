using Server.Items;

namespace Server.ContextMenus
{
    public class CollectRewardEntry : ContextMenuEntry
    {
        private readonly Aquarium m_Aquarium;

        public CollectRewardEntry(Aquarium aquarium)
            : base(6237, 2)// Collect Reward
        {
            m_Aquarium = aquarium;
        }

        public override void OnClick()
        {
            if (m_Aquarium.Deleted || !m_Aquarium.HasAccess(Owner.From))
                return;

            m_Aquarium.GiveReward(Owner.From);
        }
    }
}
