namespace SmartTrade.Entities;

public class SimpleCartItemDTO
{
    public int PostId { get; set; }
    public int OfferId { get; set; }
    public int Quantity { get; set; }

    public SimpleCartItemDTO() { }

    public SimpleCartItemDTO(int postId, int offerId, int quantity = 1)
    {
        PostId = postId;
        OfferId = offerId;
        Quantity = quantity;
    }
}