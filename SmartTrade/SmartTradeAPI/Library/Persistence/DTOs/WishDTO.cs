using SmartTradeDTOs;

namespace SmartTrade.Entities;

public class WishDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public SimplePostDTO Post { get; set; }

    public WishDTO()
    {

    }

    public WishDTO(Wish wish)
    {
        Id = wish.Id;
        UserId = wish.User.Email;
        Post = new SimplePostDTO(new PostDTO(wish.Post));
    }
}