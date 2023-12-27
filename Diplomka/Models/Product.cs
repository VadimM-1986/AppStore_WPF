using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Price { get; set; }

        public Product()
        {
        }

        public Product(string name, string description, string img, int price)
        {
            this.Name = name;
            this.Description = description;
            this.Img = img;
            this.Price = price;
        }


        public string GetFoto
        {
            get { return "/images/tovari/" + Img; }
        }

    }
}
