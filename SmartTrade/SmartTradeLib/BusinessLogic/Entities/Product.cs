using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTradeLib.Entities;

    public partial class Product
    {
        public abstract string GetInfo();
        public abstract ICollection<Category> GetCategories();
        public abstract string GetDifferences(Product product);
    }