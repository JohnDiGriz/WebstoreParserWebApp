using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParserWebApp.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Prices = new List<PriceViewModel>();
            Pictures = new List<PictureViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public List<PriceViewModel> Prices { get; set; }
        public List<PictureViewModel> Pictures { get; set; }
    }
}