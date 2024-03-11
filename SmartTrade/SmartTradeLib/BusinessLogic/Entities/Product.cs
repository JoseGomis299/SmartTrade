using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTradeLib.Entities;

    public partial class Product
    {
        protected Product()
        {
            Images = new List<byte[]>();
            Posts = new List<Post>();
            Variants = new List<Product>();
            Alerts = new List<Alert>();
        }

        protected Product(string name, string certification, string ecologicPrint, int minimumAge, bool validated, byte[] image, Post post) : base()
        {
            Name = name;
            Certification = certification;
            EcologicPrint = ecologicPrint;
            MinimumAge = minimumAge;
            Validated = validated;
            Posts.Add(post);
            Images.Add(image);
        }

        public abstract string GetInfo();
        public abstract ICollection<Category> GetCategories();
        public abstract string GetDifferences(Product product);
    }