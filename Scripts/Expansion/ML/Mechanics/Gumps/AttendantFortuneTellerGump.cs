using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
    public class AttendantFortuneTellerGump : Gump
    {
        private readonly AttendantFortuneTeller m_Teller;
        public AttendantFortuneTellerGump(AttendantFortuneTeller teller)
            : base(200, 200)
        {
            m_Teller = teller;

            AddPage(0);

            AddBackground(0, 0, 291, 155, 0x13BE);
            AddImageTiled(5, 6, 280, 20, 0xA40);
            AddHtmlLocalized(9, 8, 280, 20, 1075994, 0x7FFF, false, false); // Fortune Teller
            AddImageTiled(5, 31, 280, 91, 0xA40);
            AddHtmlLocalized(9, 35, 280, 40, 1076025, 0x7FFF, false, false); // Ask your question
            AddImageTiled(9, 55, 270, 20, 0xDB0);
            AddImageTiled(10, 55, 270, 2, 0x23C5);
            AddImageTiled(9, 55, 2, 20, 0x23C3);
            AddImageTiled(9, 75, 270, 2, 0x23C5);
            AddImageTiled(279, 55, 2, 22, 0x23C3);

            AddTextEntry(12, 56, 263, 17, 0xA28, 15, "");

            AddButton(5, 129, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
            AddHtmlLocalized(40, 131, 100, 20, 1060051, 0x7FFF, false, false); // CANCEL

            AddButton(190, 129, 0xFB7, 0xFB8, 1, GumpButtonType.Reply, 0);
            AddHtmlLocalized(225, 131, 100, 20, 1006044, 0x7FFF, false, false); // OK
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (m_Teller == null || m_Teller.Deleted)
                return;

            if (info.ButtonID == 1)
            {
                TextRelay text = info.GetTextEntry(15);

                if (text != null && !string.IsNullOrEmpty(text.Text))
                {
                    sender.Mobile.CloseGump(typeof(FortuneGump));
                    sender.Mobile.SendGump(new FortuneGump(text.Text));
                }
                else
                    sender.Mobile.SendGump(this);
            }
        }
    }
}
