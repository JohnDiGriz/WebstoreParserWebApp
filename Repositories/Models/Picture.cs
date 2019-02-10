using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repositories.Models
{
    public class Picture : BaseModel
    {
        public string PictureUrl { get; set; }
        public int ProductId { get; set; }
        //public Product Product { get; set; }
    }
}