﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreBackEnd.DTOs
{
    public class WishlistDto
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public Nullable<int> BookId { get; set; }
        public string BookTitle { get; set; }
        public Nullable<double> BookPrice { get; set; }
        public string BookImageUrl { get; set; }
        public string Author { get; set; }
    }
}