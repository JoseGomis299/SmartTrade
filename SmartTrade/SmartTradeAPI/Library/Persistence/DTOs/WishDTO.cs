﻿using SmartTradeDTOs;

namespace SmartTrade.Entities;

public class WishDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public SimplePostDTO Post { get; set; }

    public WishDTO()
    {

    }
}