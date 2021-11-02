using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BookStoreBackEnd.DTOs;
using BookStoreBackEnd.Models;

namespace BookStoreBackEnd.Controllers
{
    public class WishlistController : ApiController
    {
        private BookStoreDBEntities db = new BookStoreDBEntities();

        public IQueryable<WishlistDto> GetWisthLists()
        {
            var wishlists = from b in db.wishlists.Include(b => b.book)
                        select new WishlistDto()
                        {
                            Id = b.id,
                            userId = b.userid,
                            BookId = b.booksid,
                            BookTitle = b.book.Title,
                            BookPrice = b.book.Price,
                            BookImageUrl = b.book.Image,
                            Author = b.book.Author
                        };

            return wishlists;
        }

        // GET: api/Carts/5
        [ResponseType(typeof(WishlistDto))]
        public async Task<IHttpActionResult> GetWishlist(string id)
        {
            var wishlist = await db.wishlists.Include(b => b.book).Select(b =>
                new WishlistDto()
                {
                    Id = b.id,
                    BookId = b.booksid,
                    BookTitle = b.book.Title,
                    BookPrice = b.book.Price,
                    BookImageUrl = b.book.Image,
                    Author = b.book.Author
                }).SingleOrDefaultAsync(b => b.userId == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return Ok(wishlist);
        }

        // PUT: api/Wishlist/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putwishlist(int id, wishlist wishlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wishlist.id)
            {
                return BadRequest();
            }

            db.Entry(wishlist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!wishlistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Wishlist
        [ResponseType(typeof(wishlist))]
        public IHttpActionResult Postwishlist(wishlist wishlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.wishlists.Add(wishlist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = wishlist.id }, wishlist);
        }

        // DELETE: api/Wishlist/5
        [ResponseType(typeof(wishlist))]
        public IHttpActionResult Deletewishlist(int id)
        {
            wishlist wishlist = db.wishlists.Find(id);
            if (wishlist == null)
            {
                return NotFound();
            }

            db.wishlists.Remove(wishlist);
            db.SaveChanges();

            return Ok(wishlist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool wishlistExists(int id)
        {
            return db.wishlists.Count(e => e.id == id) > 0;
        }
    }
}