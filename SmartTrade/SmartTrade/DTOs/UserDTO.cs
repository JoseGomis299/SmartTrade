﻿using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class UserDTO
{
    public bool IsSeller { get; set; }
    public bool IsConsumer { get; set; }
    public bool IsAdmin { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string LastNames { get; set; }
}