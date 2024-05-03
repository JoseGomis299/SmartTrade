namespace SmartTradeDTOs;

public class WishDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public SimplePostDTO Post { get; set; }

    public WishDTO()
    {

    }
    public WishDTO(PostDTO post, int id)
    {
        Id = id;
        Post = new SimplePostDTO(post);
    }
}