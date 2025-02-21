using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Web2212025.Models;

namespace Web2212025.Data
{
    public class StudentContext : DbContext
    {
        // public StudentContext() : base("name=StudentDBConnectionString") //winform
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) //webapi
        {
        }

        public DbSet<Student> Students { get; set; }

    }
}
