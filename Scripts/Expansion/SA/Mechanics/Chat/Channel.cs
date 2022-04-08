using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Server.Engines.Chat
{
    public class Channel
    {
        public static void Initialize()
        {
            EventSink.Disconnected += EventSink_Disconnected;

            // TODO: Add a configuration framework to define static channels outside of code, for example as XML under the Data/ directory.
            AddStaticChannel("Help");
            AddStaticChannel("General");
            AddStaticChannel("Trade");
            AddStaticChannel("Looking For Group");
        }

        private static void EventSink_Disconnected(DisconnectedEventArgs e)
        {
            ChatUser.RemoveChatUser(e.Mobile);
        }

        public static void AddStaticChannel(string name)
        {
            AddChannel(name).AlwaysAvailable = true;
        }

        public Channel(string name)
        {
            Name = name;
            m_Users = new List<ChatUser>();
        }

        public string Name { get; }
        private List<ChatUser> m_Users;
        public IEnumerable<ChatUser> Users => new ReadOnlyCollection<ChatUser>(m_Users);

        public bool Contains(ChatUser user) => m_Users.Contains(user);

        public void AddUser(ChatUser user)
        {
            if (Contains(user))
            {
                user.SendMessage(46, Name); // You are already in the conference '%1'.
            }
            else
            {
                if (user.CurrentChannel != null)
                    user.CurrentChannel.RemoveUser(user); // Remove them from their current channel first

                ChatSystem.SendCommandTo(user.Mobile, ChatCommand.JoinedChannel, Name);

                SendCommand(ChatCommand.AddUserToChannel, user.GetColorCharacter() + user.Username);

                m_Users.Add(user);
                user.CurrentChannel = this;

                SendUsersTo(user);

                ChatLogging.LogJoin(Name, user.Username);
            }
        }

        public void RemoveUser(ChatUser user)
        {
            if (Contains(user))
            {
                m_Users.Remove(user);
                user.CurrentChannel = null;

                SendCommand(ChatCommand.RemoveUserFromChannel, user, user.Username);
                ChatSystem.SendCommandTo(user.Mobile, ChatCommand.LeaveChannel, string.Format("{{{0}}}", Name));
                ChatSystem.SendCommandTo(user.Mobile, ChatCommand.LeftChannel, Name);

                ChatLogging.LogLeave(Name, user.Username);

                if (m_Users.Count == 0 && !AlwaysAvailable)
                    RemoveChannel(this);
            }
        }

        public bool AlwaysAvailable { get; set; }

        public void SendMessage(int number, ChatUser from, string param1, string param2)
        {
            foreach (var user in m_Users)
            {
                if (user.CheckOnline())
                    user.SendMessage(number, from.Mobile, param1, param2);
            }
        }

        public void SendCommand(ChatCommand command, string param1 = null, string param2 = null)
        {
            SendCommand(command, null, param1, param2);
        }

        public void SendCommand(ChatCommand command, ChatUser initiator, string param1 = null, string param2 = null)
        {
            foreach (var user in m_Users.ToArray())
            {
                if (user == initiator)
                    continue;

                if (user.CheckOnline())
                    ChatSystem.SendCommandTo(user.Mobile, command, param1, param2);
            }
        }

        public void SendUsersTo(ChatUser to)
        {
            foreach (var user in m_Users)
            {
                ChatSystem.SendCommandTo(to.Mobile, ChatCommand.AddUserToChannel, user.GetColorCharacter() + user.Username, string.Format("{{{0}}}", Name));
            }
        }

        public static List<Channel> Channels { get; } = new List<Channel>();

        public static void SendChannelsTo(ChatUser user)
        {
            foreach (var channel in Channels)
            {
                ChatSystem.SendCommandTo(user.Mobile, ChatCommand.AddChannel, channel.Name, "0");
            }
        }

        public static Channel AddChannel(string name)
        {
            var channel = FindChannelByName(name);

            if (channel == null)
            {
                channel = new Channel(name);
                Channels.Add(channel);
            }

            ChatUser.GlobalSendCommand(ChatCommand.AddChannel, name, "0");

            ChatLogging.LogCreateChannel(name);

            return channel;
        }

        public static void RemoveChannel(string name)
        {
            RemoveChannel(FindChannelByName(name));
        }

        public static void RemoveChannel(Channel channel)
        {
            if (channel == null)
                return;

            if (Channels.Contains(channel) && channel.m_Users.Count == 0)
            {
                ChatUser.GlobalSendCommand(ChatCommand.RemoveChannel, channel.Name);
                Channels.Remove(channel);
                ChatLogging.LogRemoveChannel(channel.Name);
            }
        }

        public static Channel FindChannelByName(string name)
        {
            return Channels.FirstOrDefault(channel => channel.Name == name);
        }

        public static Channel Default => FindChannelByName(ChatSystem.DefaultChannel);
    }
}
