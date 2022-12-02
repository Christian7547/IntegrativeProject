using Microsoft.EntityFrameworkCore;
using ProfessorManagement.Models;

namespace ProfessorManagement.Data
{
    public class ProfessorContext : DbContext
    {
        public ProfessorContext(DbContextOptions<ProfessorContext> options) : base(options)
        {

        }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Request> Requests { get; set; }  
        public DbSet<ProfessorRequest> ProfessorsRequests { get; set; } 
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Professor_Subject> Professors_Subjects { get; set; }
        public DbSet<Professor_Grade> Professor_Grades { get; set; }
        public DbSet<AcademicPeriod> AcademicPeriods { get; set; }  
        public DbSet<AcademicDesignation> AcademicDesignations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role > Roles { get; set; } 
    }
}
