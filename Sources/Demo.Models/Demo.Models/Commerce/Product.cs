using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Demo.Models.Commerce
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<string> Features { get; set; }
        public int NumberOfLegs { get; set; }

        public Product()
        {
            Features = new List<string>();
            NumberOfLegs = 4;
        }

        public override string ToString()
        {
            return string.Format("Product named {0} is in category {1} with a price of {2:c}", Name, Category, Price);
        }
    }
}
