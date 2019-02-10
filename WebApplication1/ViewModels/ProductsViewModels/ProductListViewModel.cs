using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParserWebApp.ViewModels
{
    public class ProductListViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public int PageCount { get; set; }
        public int PageNum { get; set; }
    }
}