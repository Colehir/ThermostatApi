using System;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace ThermostatApi.Models
{
    public class Thermostat
    {
        public long Id { get; set; }
        public int CurrentTemp { get; set; }
        public int SetTemp { get; set; }
    }

    public class ThermostatContext : DbContext
    {
        public ThermostatContext(DbContextOptions<ThermostatContext> options) : base(options)
        {
        }

        public DbSet<Thermostat> ThermostatItems { get; set; }
    }
}
