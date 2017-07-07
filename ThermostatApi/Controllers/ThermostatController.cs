using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThermostatApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThermostatApi.Controllers
{
    [Route("api/[controller]")]
    public class ThermostatController : Controller
    {
        private readonly ThermostatContext _context;

        public ThermostatController(ThermostatContext context)
        {
            _context = context;

            if (_context.ThermostatItems.Count() == 0)
            {
                _context.ThermostatItems.Add(new Thermostat { Id = 1, SetTemp = 75 });
                _context.SaveChanges();
            }
        }


        // GET api/thermostat
        [HttpGet]
        public IEnumerable<Thermostat> Get()
        {
            return _context.ThermostatItems.ToList();
        }

        [HttpGet("{id}", Name = "GetThermostat")]
        public IActionResult Get(int id)
        {
            var item = _context.ThermostatItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Thermostat item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.ThermostatItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetThermostat", new { id = item.Id }, item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Thermostat item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.ThermostatItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.SetTemp = item.SetTemp;
            todo.CurrentTemp = item.CurrentTemp;

            _context.ThermostatItems.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
