﻿using Microsoft.IdentityModel.Tokens;
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
    public List<RatingDTO> Ratings { get; set; }
    public int? NumRatings { get; set; }
    public int? AveragePoints { get; set; }

    public PostDTO()
    {
        Offers = new List<OfferDTO>();
        Ratings = new List<RatingDTO>();
    }

    public PostDTO(Post post)
    {
        Product product = post.Offers.First().Product;

        Id = post.Id;
        Title = post.Title;
        Description = post.Description;
        ProductName = product.Name;
        Category = product.GetCategories().First();
        MinimumAge = product.MinimumAge;
        HowToUse = product.HowToUse;
        Certifications = product.Certification;
        EcologicPrint = product.EcologicPrint;
        HowToReducePrint = product.HowToReducePrint;
        Validated = post.Validated;
        SellerID = post.Seller.Email;
        Offers = post.Offers.Select(offer => new OfferDTO(offer)).ToList();
        SellerCompanyName = post.Seller.CompanyName;

        if (!post.Ratings.IsNullOrEmpty())
        {
            Ratings = post.Ratings.Select(rating => new RatingDTO(rating)).ToList();
            NumRatings = Ratings.Count();
            AveragePoints = GetAveragePoints();
        }
        else
        {
            Ratings = new List<RatingDTO>();
            NumRatings = 0;
            AveragePoints = 0;
        }
    }

    private int GetAveragePoints()
    {
        int averagePoints = 0;
        int aux = 0;
        foreach (var rating in Ratings)
        {
            aux += rating.Points;
        }

        averagePoints = (int)(aux / NumRatings);
        return averagePoints;
    }
}