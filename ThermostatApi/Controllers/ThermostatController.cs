using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.EntityFrameworkCore;
using ThermostatApi.Models;

namespace ThermostatApi.Controllers
{
    public class ThermostatController : ApiController
    {
        private ThermostatContext db;
        ThermostatController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ThermostatContext>();
            optionsBuilder.UseInMemoryDatabase();
            db = new ThermostatContext(optionsBuilder.Options);

            if (db.ThermostatItems.Count() == 0)
            {
                db.ThermostatItems.Add(new Thermostat { Id = 1, SetTemp = 75 });
                db.SaveChanges();
            }
        }

        // GET: api/Thermostats
        public IQueryable<Thermostat> GetThermostatItems()
        {
            return db.ThermostatItems;
        }

        // GET: api/Thermostats/5
        [ResponseType(typeof(Thermostat))]
        public IHttpActionResult GetThermostat(long id)
        {
            Thermostat thermostat = db.ThermostatItems.Find(id);
            if (thermostat == null)
            {
                return NotFound();
            }

            return Ok(thermostat);
        }

        // PUT: api/Thermostats/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutThermostat(long id, Thermostat thermostat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != thermostat.Id)
            {
                return BadRequest();
            }

            db.Entry(thermostat).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThermostatExists(id))
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

        // POST: api/Thermostats
        [ResponseType(typeof(Thermostat))]
        public IHttpActionResult PostThermostat(Thermostat thermostat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ThermostatItems.Add(thermostat);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = thermostat.Id }, thermostat);
        }

        // DELETE: api/Thermostats/5
        [ResponseType(typeof(Thermostat))]
        public IHttpActionResult DeleteThermostat(long id)
        {
            Thermostat thermostat = db.ThermostatItems.Find(id);
            if (thermostat == null)
            {
                return NotFound();
            }

            db.ThermostatItems.Remove(thermostat);
            db.SaveChanges();

            return Ok(thermostat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThermostatExists(long id)
        {
            return db.ThermostatItems.Count(e => e.Id == id) > 0;
        }
    }
}