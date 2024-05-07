namespace SmartTrade.Entities;

public partial class Notification
{
    public Notification() { }

    public Notification(bool visited, string message, Consumer targetUser, Post targetPost)
    {
        Visited = visited;
        TargetUser = targetUser;
        TargetPost = targetPost;
        Message = message;
    }
}