
namespace Server.Engines.Chat
{
    public delegate void OnChatAction(ChatUser from, Channel channel, string param);

    public class ChatActionHandler
    {
        public bool RequireConference { get; }
        public OnChatAction Callback { get; }

        public ChatActionHandler(bool requireConference, OnChatAction callback)
        {
            RequireConference = requireConference;
            Callback = callback;
        }
    }
}
