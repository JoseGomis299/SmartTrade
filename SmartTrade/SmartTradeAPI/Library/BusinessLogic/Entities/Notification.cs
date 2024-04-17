namespace SmartTrade.Entities;

public partial class Notification
{
    public Notification() { }

    public Notification(bool visited, Consumer targetUser, Post targetPost)
    {
        Visited = visited;
        TargetUser = targetUser;
        TargetPost = targetPost;
    }
}