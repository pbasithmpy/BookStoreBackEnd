using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookStoreBackEnd.Models;

namespace BookStoreBackEnd.Controllers
{
    public class CouponController : ApiController
    {
        private BookStoreDBEntities db = new BookStoreDBEntities();

        // GET: api/Coupon
        public IQueryable<coupon> Getcoupons()
        {
            return db.coupons;
        }

        // GET: api/Coupon/5
        [ResponseType(typeof(coupon))]
        public IHttpActionResult Getcoupon(string id)
        {
            coupon coupon = db.coupons.Find(id);
            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

        // PUT: api/Coupon/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcoupon(string id, coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coupon.couponcode)
            {
                return BadRequest();
            }

            db.Entry(coupon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!couponExists(id))
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

        // POST: api/Coupon
        [ResponseType(typeof(coupon))]
        public IHttpActionResult Postcoupon(coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.coupons.Add(coupon);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (couponExists(coupon.couponcode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = coupon.couponcode }, coupon);
        }

        // DELETE: api/Coupon/5
        [ResponseType(typeof(coupon))]
        public IHttpActionResult Deletecoupon(string id)
        {
            coupon coupon = db.coupons.Find(id);
            if (coupon == null)
            {
                return NotFound();
            }

            db.coupons.Remove(coupon);
            db.SaveChanges();

            return Ok(coupon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool couponExists(string id)
        {
            return db.coupons.Count(e => e.couponcode == id) > 0;
        }
    }
}