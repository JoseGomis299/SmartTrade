﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.BusinessLogic;

namespace SmartTrade.Entities;

    public partial class Product
    {
        protected Product()
        {
            Images = new List<Image>();
            Posts = new List<Post>();
        }

        protected Product(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint) : this()
        {
            Name = name;
            Certification = certification;
            EcologicPrint = ecologicPrint;
            MinimumAge = minimumAge;
            HowToUse = howToUse;
            HowToReducePrint = howToReducePrint;
        }

        public void AddImage(Image image)
        {
            if(!Images.Contains(image)) Images.Add(image);
        }

        public void AddPost(Post post)
        {
            Posts.Add(post);
        }

        public abstract string[] GetAttributes();
        public abstract string GetInfo();
        public abstract ICollection<Category> GetCategories();
        public abstract string GetDifferences(Product product);

        public override bool Equals(object? obj)
        {
            return obj is Product product && Name.ToCommonSyntax() == product.Name.ToCommonSyntax();
        }
    }