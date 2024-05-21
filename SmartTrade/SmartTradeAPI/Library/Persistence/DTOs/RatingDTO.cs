using SmartTradeDTOs;

namespace SmartTrade.Entities;

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

    public RatingDTO(Rating rating)
    {
        Id = rating.Id;
        Points = rating.Points;
        Description = rating.Description;
        UserId = rating.User.Email;
        PostId = rating.Post.Id;
    }
}