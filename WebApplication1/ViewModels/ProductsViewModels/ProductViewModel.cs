using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ParserWebApp.WEB.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ProductViewModel()
        {
            Prices = new List<PriceViewModel>();
            Pictures = new List<PictureViewModel>();
        }
        public string ShortDescription
        {
            get
            {
                return Description.Substring(0, 200) + (Description.Length < 200 ? "" : "...");
            }
        }
        public int Id { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public List<PriceViewModel> Prices { get; set; }
        public List<PictureViewModel> Pictures { get; set; }
    }
}