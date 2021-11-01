using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreBackEnd.DTOs
{
    public class BooksDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public Nullable<double> Price { get; set; }
        public string ImageUrl { get; set; }
    }

    public class BooksDetailsDto
    {
        public int BookId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public Nullable<int> ISBN { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<double> Price { get; set; }
        public string Description { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<bool> Status { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public Nullable<int> Stock { get; set; }
    }
}