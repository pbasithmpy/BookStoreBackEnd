//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStoreBackEnd.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public book()
        {
            this.carts = new HashSet<cart>();
            this.wishlists = new HashSet<wishlist>();
        }
    
        public int BookId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Title { get; set; }
        public Nullable<int> ISBN { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<double> Price { get; set; }
        public string Description { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public Nullable<int> Stock { get; set; }
    
        public virtual category category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wishlist> wishlists { get; set; }
    }
}
