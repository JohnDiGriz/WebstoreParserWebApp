using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParserWebApp.DAL.Models
{
    public class Picture : BaseModel
    {
        public string PictureUrl { get; set; }
        public int ProductId { get; set; }
        //public Product Product { get; set; }
    }
}