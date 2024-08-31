using MaterDetailsMvcCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MaterDetailsMvcCore.Data
{
    public class MyDbContext:DbContext
    {
        //public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        //{
        //}
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options) 
        {
        }
        public DbSet<Applicant>applicants { get; set; }
        public DbSet<Experience>experiences { get; set; }
    }
}
