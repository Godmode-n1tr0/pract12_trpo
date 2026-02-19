using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract12_trpo.Data
{
    public class AppDbContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder
        optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=sql.ects;Database=UsersDBShengals;User Id = student_00;Password = student_00;TrustServerCertificate = True");
        }   
    }
}
