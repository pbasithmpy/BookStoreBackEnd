using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreBackEnd.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public Nullable<double> TotalPrice { get; set; }
    }
}