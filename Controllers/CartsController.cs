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
                            userId = b.userId,
                            BookTitle = b.book.Title,
                            BookPrice = b.book.Price,
                            BookImageUrl = b.book.Image,
                            BookQuantity = b.BookQuantity
                        };

            return carts;
        }

        // GET: api/Carts/5
        //[ResponseType(typeof(CartDto))]
        //public async Task<IHttpActionResult> GetCart(string userId)
        //{
        //    var cart = await db.carts.Include(b => b.book).Select(b =>
        //        new CartDto()
        //        {
        //            Id = b.Id,
        //            BookId = b.BookId,
        //            BookTitle = b.book.Title,
        //            BookPrice = b.book.Price,
        //            BookImageUrl = b.book.Image,
        //            BookQuantity = b.BookQuantity
        //        }).SingleOrDefaultAsync(b => b.userId == userId);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(cart);
        //}

        public IHttpActionResult GetById(string userId)
        {
            IList<CartDto> cartItemList = null;
            using (var ctx = new BookStoreDBEntities())
            {
                cartItemList = ctx.carts.Include(b => b.book)
                                    .Where(s => s.userId == userId)
                                    .Select(s =>
                                    new CartDto()
                                    {
                                        Id = s.Id,
                                        userId = s.userId,
                                        BookId = s.BookId,
                                        BookTitle = s.book.Title,
                                        BookPrice = s.book.Price,
                                        BookImageUrl = s.book.Image,
                                        BookQuantity = s.BookQuantity

                                    }).ToList<CartDto>();
            }
            if (cartItemList.Count == 0)
            {
                return NotFound();
            }
            return Ok(cartItemList);
        }

        //public IHttpActionResult GetAllStudents(string name)
        //{
        //    IList<StudentViewModel> students = null;

        //    using (var ctx = new SchoolDBEntities())
        //    {
        //        students = ctx.Students.Include("StudentAddress")
        //            .Where(s => s.FirstName.ToLower() == name.ToLower())
        //            .Select(s => new StudentViewModel()
        //            {
        //                Id = s.StudentID,
        //                FirstName = s.FirstName,
        //                LastName = s.LastName,
        //                Address = s.StudentAddress == null ? null : new AddressViewModel()
        //                {
        //                    StudentId = s.StudentAddress.StudentID,
        //                    Address1 = s.StudentAddress.Address1,
        //                    Address2 = s.StudentAddress.Address2,
        //                    City = s.StudentAddress.City,
        //                    State = s.StudentAddress.State
        //                }
        //            }).ToList<StudentViewModel>();
        //    }

        //    if (students.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(students);
        //}


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