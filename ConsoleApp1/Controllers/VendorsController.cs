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
using ConsoleApp1.Models;

namespace ConsoleApp1.Controllers
{
    public class VendorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Vendors
        public IQueryable<Vendors> GetVendors()
        {
            return db.Vendors;
        }

        // GET: api/Vendors/5
        [ResponseType(typeof(Vendors))]
        public IHttpActionResult GetVendors(Guid id)
        {
            Vendors vendors = db.Vendors.Find(id);
            if (vendors == null)
            {
                return NotFound();
            }

            return Ok(vendors);
        }

        // PUT: api/Vendors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendors(Guid id, Vendors vendors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendors.ID)
            {
                return BadRequest();
            }

            db.Entry(vendors).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorsExists(id))
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

        // POST: api/Vendors
        [ResponseType(typeof(Vendors))]
        public IHttpActionResult PostVendors(Vendors vendors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            vendors.CreateDate = DateTime.Now;
            vendors.UpdateDate = DateTime.Now;
            vendors.Deleted = false;
            vendors.DomainID = Guid.NewGuid();
            db.Vendors.Add(vendors);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VendorsExists(vendors.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vendors.ID }, vendors);
        }

        // DELETE: api/Vendors/5
        [ResponseType(typeof(Vendors))]
        public IHttpActionResult DeleteVendors(Guid id)
        {
            Vendors vendors = db.Vendors.Find(id);
            if (vendors == null)
            {
                return NotFound();
            }

            db.Vendors.Remove(vendors);
            db.SaveChanges();

            return Ok(vendors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorsExists(Guid id)
        {
            return db.Vendors.Count(e => e.ID == id) > 0;
        }
    }
}