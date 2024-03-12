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
            Alerts = new List<Alert>();
        }

        protected Product(string name, string certification, string ecologicPrint, int minimumAge) : this()
        {
            Name = name;
            Certification = certification;
            EcologicPrint = ecologicPrint;
            MinimumAge = minimumAge;
        }

        public abstract string GetInfo();
        public abstract ICollection<Category> GetCategories();
        public abstract string GetDifferences(Product product);
    }