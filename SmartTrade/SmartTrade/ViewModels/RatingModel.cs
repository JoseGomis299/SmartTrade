using System;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class RatingModel
{
    public string Description { get; set; }
    public float Rating { get; set; }
    public string User { get; set; }

    public RatingModel(RatingDTO rating)
    {
        Description = rating.Description;
        Rating = rating.Points;
        User = rating.UserNickname;
    }
}