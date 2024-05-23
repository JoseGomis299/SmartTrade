using System;
using System.Collections.Generic;
using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class PostDTO
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ProductName { get; set; }
    public Category Category { get; set; }
    public int MinimumAge { get; set; }
    public string HowToUse { get; set; }
    public string? Certifications { get; set; }
    public string? EcologicPrint { get; set; }
    public string? HowToReducePrint { get; set; }
    public bool Validated { get; set; }
    public string? SellerID { get; set; }
    public List<OfferDTO> Offers { get; set; }
    public string? SellerCompanyName { get; set; }
    public List<RatingDTO> Ratings { get; set; } = new();
    public int NumRatings { get; set; } = 0;
    public float AveragePoints => GetAveragePoints();

    public void AddRating(RatingDTO rating)
    {
        Ratings.Add(rating);
        NumRatings++;
    }

    public void AddRating(RatingDTO rating, int i)
    {
        Ratings[i] = rating;
    }

    private float GetAveragePoints()
    {
        int sum = 0;
        foreach (var rating in Ratings)
        {
            sum += rating.Points;
        }
        return MathF.Round(sum / (float)NumRatings, 2);
    }
}