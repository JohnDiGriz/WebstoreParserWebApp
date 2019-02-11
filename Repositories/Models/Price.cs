﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParserWebApp.DAL.Models
{
    public class Price : BaseModel
    {
        public int ProductId { get; set; }
        //public Product Product { get; set; }
        public string PriceValue { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public string DateStr { get { return Date.ToShortDateString() + " " + Date.ToShortTimeString(); } }
    }
}