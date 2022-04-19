using Server.Items;
using Server.Mobiles;
using Server.Network;
using System;

namespace Server.Gumps
{
    public class GemResurrectGump : ResurrectGump
    {
        private readonly GemOfSalvation m_Gem;
        private readonly PlayerMobile m_Mobile;

        public GemResurrectGump(PlayerMobile pm, GemOfSalvation gem)
            : base(pm, ResurrectMessage.GemOfSalvation)
        {
            m_Gem = gem;
            m_Mobile = pm;
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            m_Mobile.CloseGump(typeof(ResurrectGump));

            if (info.ButtonID == 1 && !m_Gem.Deleted && m_Gem.IsChildOf(m_Mobile.Backpack))
            {
                if (m_Mobile.Map == null || !m_Mobile.Map.CanFit(m_Mobile.Location, 16, false, false))
                {
                    m_Mobile.SendLocalizedMessage(502391); // Thou can not be resurrected there!
                    return;
                }

                m_Mobile.PlaySound(0x214);
                m_Mobile.Resurrect();

                m_Mobile.SendLocalizedMessage(1095132); // The gem infuses you with its power and is destroyed in the process.

                m_Gem.Delete();

                m_Mobile.NextGemOfSalvationUse = DateTime.UtcNow + TimeSpan.FromHours(6);
            }
        }
    }
}

