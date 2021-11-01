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
    
    public class BooksController : ApiController
    {
        private BookStoreDBEntities db = new BookStoreDBEntities();


        //get books with basic datas
        public IQueryable<BooksDto> GetBooks()
        {
            var books = from b in db.books
                        select new BooksDto()
                        {
                            BookId = b.BookId,
                            Title = b.Title,
                            AuthorName = b.Author,
                            ImageUrl = b.Image,
                            Price = b.Price
                        };

            return books;
        }

        //get book with details
        //[Authorize]
        [ResponseType(typeof(BooksDetailsDto))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var book = await db.books.Include(b => b.category).Select(b =>
                new BooksDetailsDto()
                {
                    BookId = b.BookId,
                    CategoryName = b.category.CategoryName,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Year = b.Year,
                    Price = b.Price,
                    Description = b.Description,
                    Position = b.Position,
                    Status = b.Status,
                    ImageUrl = b.Image,
                    Author = b.Author,
                    Stock = b.Stock
                }).SingleOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbooks(int id, book books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != books.BookId)
            {
                return BadRequest();
            }

            db.Entry(books).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!booksExists(id))
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

        // POST: api/books
        [ResponseType(typeof(book))]
        public IHttpActionResult Postbooks(book books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.books.Add(books);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = books.BookId }, books);
        }

        // DELETE: api/books/5
        [ResponseType(typeof(book))]
        public IHttpActionResult Deletebooks(int id)
        {
            book books = db.books.Find(id);
            if (books == null)
            {
                return NotFound();
            }

            db.books.Remove(books);
            db.SaveChanges();

            return Ok(books);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool booksExists(int id)
        {
            return db.books.Count(e => e.BookId == id) > 0;
        }
    }
}