namespace SmartTradeDTOs;

public class RatingDTO
{
    public int Id { get; set; }
    public int Points { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public int PostId { get; set; }

    public RatingDTO()
    {

    }

    public RatingDTO(PostDTO post, string consumerId, int points, string description)
    {
        Points = points;
        Description = description;
        UserId = consumerId;
        PostId = (int) post.Id;
    }
}