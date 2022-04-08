
namespace Server.Engines.Chat
{
    public enum ChatCommand
    {
        AddChannel = 0x3E8,
        RemoveChannel = 0x3E9,
        CloseChatWindow = 0x3EC,
        OpenChatWindow = 0x3ED,
        AddUserToChannel = 0x3EE,
        RemoveUserFromChannel = 0x3EF,
        LeaveChannel = 0x3F0,
        JoinedChannel = 0x3F1,
        LeftChannel = 0x3F4,
    }
}
