namespace SmartTradeDTOs;

public class RatingDTO
{
    public int Id { get; set; }
    public int Points { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public string UserNickname { get; set; }
    public int PostId { get; set; }

    public RatingDTO()
    {

    }

    public RatingDTO(PostDTO post, ConsumerDTO user, int points, string description)
    {
        Points = points;
        Description = description;
        UserId = user.Email;
        PostId = (int) post.Id;
        UserNickname = user.Name + " " + user.LastNames;
    }
}