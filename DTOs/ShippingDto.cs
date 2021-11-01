using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreBackEnd.DTOs
{
    public class ShippingDto
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public Nullable<int> pincode { get; set; }
    }
}