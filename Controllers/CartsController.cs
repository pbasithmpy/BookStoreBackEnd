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
    public class CartsController : ApiController
    {
        private BookStoreDBEntities db = new BookStoreDBEntities();

        public IQueryable<CartDto> GetCart()
        {
            var carts = from b in db.carts.Include(b => b.book)
                        select new CartDto()
                        {
                            Id = b.Id,
                            BookId = b.BookId,
                            BookTitle = b.book.Title,
                            BookPrice = b.book.Price,
                            BookImageUrl = b.book.Image,
                            BookQuantity = b.BookQuantity
                        };

            return carts;
        }

        // GET: api/Carts/5
        [ResponseType(typeof(CartDto))]
        public async Task<IHttpActionResult> GetCart(int id)
        {
            var cart = await db.carts.Include(b => b.book).Select(b =>
                new CartDto()
                {
                    Id = b.Id,
                    BookId = b.BookId,
                    BookTitle = b.book.Title,
                    BookPrice = b.book.Price,
                    BookImageUrl = b.book.Image,
                    BookQuantity = b.BookQuantity
                }).SingleOrDefaultAsync(b => b.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // PUT: api/Carts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcart(int id, cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cart.Id)
            {
                return BadRequest();
            }

            db.Entry(cart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cartExists(id))
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

        // POST: api/Carts
        [ResponseType(typeof(cart))]
        public IHttpActionResult Postcart(cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.carts.Add(cart);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cart.Id }, cart);
        }

        // DELETE: api/Carts/5
        [ResponseType(typeof(cart))]
        public IHttpActionResult Deletecart(int id)
        {
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return NotFound();
            }

            db.carts.Remove(cart);
            db.SaveChanges();

            return Ok(cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cartExists(int id)
        {
            return db.carts.Count(e => e.Id == id) > 0;
        }
    }
}