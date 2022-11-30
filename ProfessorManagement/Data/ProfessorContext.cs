using Microsoft.EntityFrameworkCore;
using ProfessorManagement.Models;

namespace ProfessorManagement.Data
{
    public class ProfessorContext : DbContext
    {
        public ProfessorContext(DbContextOptions<ProfessorContext> options) : base(options)
        {

        }

        public DbSet<Professor> Products { get; set; }
    }
}
