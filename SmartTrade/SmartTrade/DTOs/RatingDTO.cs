namespace SmartTradeDTOs;

public class RatingDTO
{
    public int Id { get; set; }
    public int Points { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public SimplePostDTO Post { get; set; }

    public RatingDTO()
    {

    }

    public RatingDTO(PostDTO post, int id, string consumerId, int points, string description)
    {
        Id = id;
        Points = points;
        Description = description;
        UserId = consumerId;
        Post = new SimplePostDTO(post);
    }
}