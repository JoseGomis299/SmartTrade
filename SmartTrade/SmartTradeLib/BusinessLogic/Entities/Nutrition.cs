﻿namespace SmartTradeLib.Entities;

public partial class Nutrition : Product
{
    public override string GetInfo()
    {
        throw new NotImplementedException();
    }

    public override ICollection<Category> GetCategories()
    {
        throw new NotImplementedException();
    }

    public override string GetDifferences(Product product)
    {
        throw new NotImplementedException();
    }
}