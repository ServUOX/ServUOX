using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Engines.Chat
{
    public class ChatUser
    {
        public ChatUser(Mobile m)
        {
            Mobile = m;
        }

        public Mobile Mobile { get; }
        public string Username => string.Format("<{0}>{1}", Mobile.Serial.Value, Mobile.Name);
        public Channel CurrentChannel { get; set; }
        public bool IsOnline => (Mobile.NetState != null);
        public const char NormalColorCharacter = '0';

        public char GetColorCharacter()
        {
            return NormalColorCharacter;
        }

        public bool CheckOnline()
        {
            if (IsOnline)
                return true;

            RemoveChatUser(this);
            return false;
        }

        public void SendMessage(int number, string param1 = null, string param2 = null)
        {
            SendMessage(number, Mobile, param1, param2);
        }

        public void SendMessage(int number, Mobile from, string param1, string param2)
        {
            if (Mobile.NetState != null)
                Mobile.Send(new ChatMessagePacket(from, number, param1, param2));
        }

        private static List<ChatUser> m_Users = new List<ChatUser>();
        private static Dictionary<Mobile, ChatUser> m_Table = new Dictionary<Mobile, ChatUser>();

        public static ChatUser AddChatUser(Mobile from)
        {
            var user = GetChatUser(from);

            if (user == null)
            {
                user = new ChatUser(from);

                m_Users.Add(user);
                m_Table[from] = user;

                Channel.SendChannelsTo(user);
            }

            return user;
        }

        public static void RemoveChatUser(ChatUser user)
        {
            if (user == null)
                return;

            if (m_Users.Contains(user))
            {
                ChatSystem.SendCommandTo(user.Mobile, ChatCommand.CloseChatWindow);

                if (user.CurrentChannel != null)
                    user.CurrentChannel.RemoveUser(user);

                m_Users.Remove(user);
                m_Table.Remove(user.Mobile);
            }
        }

        public static void RemoveChatUser(Mobile from)
        {
            var user = GetChatUser(from);

            RemoveChatUser(user);
        }

        public static ChatUser GetChatUser(Mobile from)
        {
            ChatUser c;
            m_Table.TryGetValue(from, out c);
            return c;
        }

        public static ChatUser GetChatUser(string username)
        {
            return m_Users.FirstOrDefault(user => user.Username == username);
        }

        public static void GlobalSendCommand(ChatCommand command, string param1 = null, string param2 = null)
        {
            GlobalSendCommand(command, null, param1, param2);
        }

        public static void GlobalSendCommand(ChatCommand command, ChatUser initiator, string param1 = null, string param2 = null)
        {
            foreach (var user in m_Users.ToArray())
            {
                if (user == initiator)
                    continue;

                if (user.CheckOnline())
                    ChatSystem.SendCommandTo(user.Mobile, command, param1, param2);
            }
        }
    }
}
