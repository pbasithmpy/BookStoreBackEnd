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
    public class ShippingController : ApiController
    {
        private BookStoreDBEntities db = new BookStoreDBEntities();

        // GET: api/Shipping
        public IQueryable<ShippingDto> GetShippngs()
        {
            var shippings = from b in db.shippings.Include(b => b.User)
                        select new ShippingDto()
                        {
                            id = b.id,
                            userId = b.userId,
                            name = b.name,
                            address = b.address,
                            country = b.country,
                            state = b.state,
                            pincode = b.pincode
                        };

            return shippings;
        }

        //get book with details
        [Authorize]
        [ResponseType(typeof(ShippingDto))]
        public async Task<IHttpActionResult> GetShipping(string userId)
        {
            var shipping = await db.shippings.Include(b => b.User).Select(b =>
                new ShippingDto()
                {
                    id = b.id,
                    userId = b.userId,
                    name = b.name,
                    address = b.address,
                    country = b.country,
                    state = b.state,
                    pincode = b.pincode
                }).SingleOrDefaultAsync(b => b.userId == userId);
            if (shipping == null)
            {
                return NotFound();
            }

            return Ok(shipping);
        }

        // PUT: api/Shipping/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putshipping(int id, shipping shipping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shipping.id)
            {
                return BadRequest();
            }

            db.Entry(shipping).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!shippingExists(id))
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

        // POST: api/Shipping
        [ResponseType(typeof(shipping))]
        public IHttpActionResult Postshipping(shipping shipping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.shippings.Add(shipping);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shipping.id }, shipping);
        }

        // DELETE: api/Shipping/5
        [ResponseType(typeof(shipping))]
        public IHttpActionResult Deleteshipping(int id)
        {
            shipping shipping = db.shippings.Find(id);
            if (shipping == null)
            {
                return NotFound();
            }

            db.shippings.Remove(shipping);
            db.SaveChanges();

            return Ok(shipping);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool shippingExists(int id)
        {
            return db.shippings.Count(e => e.id == id) > 0;
        }
    }
}