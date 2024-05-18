using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTrade.Entities;

namespace SmartTradeAPI.Controllers;

[ApiController]
[Route("SmartTradeAPI/Rating")]
public class RatingController : ControllerBase
{
    private readonly ISmartTradeService _service = new SmartTradeService();

    [HttpPost("CreateRating")]
    public void CreateRating(int postId, int points, string descrption)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.AddRating(points, descrption ,loggedId, postId);
    }

    [HttpDelete("DeleteRating")]
    public void DeleteRating(int id)
    {
        _service.DeleteRating(id);
    }

    [HttpGet("GetRatingList")]
    public List<RatingDTO> GetRatingList(int postId)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return _service.GetRatings(postId);
    }
}