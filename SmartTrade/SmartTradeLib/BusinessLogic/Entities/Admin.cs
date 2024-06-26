﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartTrade.Entities;

public partial class Admin : User
{
    public Admin()
    {
        ValidatedPosts = new List<Post>();
        ValidatedProducts = new List<Product>();
    }
    public Admin(string email, string password, string name, string lastNames) : base(email, password, name, lastNames)
    {
        Email = email;
        Password = password;
        Name = name;
        LastNames = lastNames;

        ValidatedPosts = new List<Post>();
        ValidatedProducts = new List<Product>();
    }

    public void ValidatePost(Post post)
    {
        ValidatedPosts.Add(post);

        foreach (var offer in post.Offers)
        {
            ValidatedProducts.Add(offer.Product);
        }
    }
}