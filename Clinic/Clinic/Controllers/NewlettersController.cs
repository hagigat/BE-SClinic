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
using Clinic.Models;

namespace Clinic.Controllers
{
    public class NewlettersController : ApiController
    {
        private DB db = new DB();

        // GET: api/Newletters
        public IQueryable<Newletter> GetNewletters()
        {
            return db.Newletters;
        }

        // GET: api/Newletters/5
        [ResponseType(typeof(Newletter))]
        public IHttpActionResult GetNewletter(int id)
        {
            Newletter newletter = db.Newletters.Find(id);
            if (newletter == null)
            {
                return NotFound();
            }

            return Ok(newletter);
        }

        // PUT: api/Newletters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNewletter(int id, Newletter newletter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newletter.NID)
            {
                return BadRequest();
            }

            db.Entry(newletter).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewletterExists(id))
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

        // POST: api/Newletters
        [ResponseType(typeof(Newletter))]
        public IHttpActionResult PostNewletter(Newletter newletter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Newletters.Add(newletter);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newletter.NID }, newletter);
        }

        // DELETE: api/Newletters/5
        [ResponseType(typeof(Newletter))]
        public IHttpActionResult DeleteNewletter(int id)
        {
            Newletter newletter = db.Newletters.Find(id);
            if (newletter == null)
            {
                return NotFound();
            }

            db.Newletters.Remove(newletter);
            db.SaveChanges();

            return Ok(newletter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewletterExists(int id)
        {
            return db.Newletters.Count(e => e.NID == id) > 0;
        }
    }
}