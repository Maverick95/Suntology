using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Suntology.Entities;

namespace Suntology.Contexts
{
    public class SuntologyContext : DbContext
    {
        public DbSet<Caste> Castes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=suntology;Trusted_Connection=true;");
        }
    }
}
