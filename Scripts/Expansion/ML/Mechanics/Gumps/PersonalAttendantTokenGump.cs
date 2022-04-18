using Server.Items;
using Server.Network;

namespace Server.Gumps
{
    public class PersonalAttendantTokenGump : Gump
    {
        private readonly PersonalAttendantToken m_Token;
        public PersonalAttendantTokenGump(PersonalAttendantToken token)
            : base(200, 200)
        {
            m_Token = token;

            AddPage(0);

            AddBackground(0, 0, 291, 159, 0x13BE);
            AddImageTiled(5, 6, 280, 20, 0xA40);
            AddHtmlLocalized(9, 8, 280, 20, 1049004, 0x7FFF, false, false); // Confirm
            AddImageTiled(5, 31, 280, 100, 0xA40);
            AddHtmlLocalized(9, 35, 272, 100, 1076052, 0x7FFF, false, false); // Clicking "OK" will create a Personal Attendant contract bound to you. You will not be able to trade it to another player, and only you will be able to use it.

            AddButton(190, 133, 0xFB7, 0xFB8, 1, GumpButtonType.Reply, 0);
            AddHtmlLocalized(225, 135, 90, 20, 1006044, 0x7FFF, false, false); // OK

            AddButton(5, 133, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
            AddHtmlLocalized(40, 135, 100, 20, 1060051, 0x7FFF, false, false); // CANCEL
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (m_Token == null || m_Token.Deleted)
                return;

            if (info.ButtonID == 1)
            {
                sender.Mobile.AddToBackpack(new PersonalAttendantDeed(sender.Mobile));
                m_Token.Delete();
            }
        }
    }
}
