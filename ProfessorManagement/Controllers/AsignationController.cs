using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProfessorManagement.Data;
using ProfessorManagement.Models;

namespace ProfessorManagement.Controllers
{
    public class AsignationController : Controller
    {
        private readonly ProfessorContext _context;

        public AsignationController(ProfessorContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int gradeId, int professorId, int subjectId)
        {
            Professor_Subject professor_Subject = new Professor_Subject() 
            { 
                ProfessorId = professorId,
                SubjectId = subjectId
            };
            

            Professor_Grade professor_Grade = new Professor_Grade()
            {
                ProfessorId = professorId,
                GradeId = gradeId
            };           

            _context.Professors_Subjects.Add(professor_Subject);
            _context.Professor_Grades.Add(professor_Grade);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListAssignments", "Asignation");
        }

        [HttpGet]
        public async Task<IActionResult> Assignments()
        {
            ViewData["ProfessorId"] = new SelectList(_context.Professors.Where(p => p.Status == 1), "Id", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            List<Grade> grades = await _context.Grades.ToListAsync();
            ViewData["GradeId"] = grades;

            return View();
        }

        public async Task<IActionResult> ListAssignments()
        {
            List<Professor> professors = await _context.Professors.ToListAsync();
            List<Subject> subjects = await _context.Subjects.ToListAsync();
            List<Grade> grades = await _context.Grades.ToListAsync();
            List<Professor_Subject> professor_Subjects = await _context.Professors_Subjects.ToListAsync();
            List<Professor_Grade> professor_Grades = await _context.Professor_Grades.ToListAsync();

            ViewBag.Professors = professors;
            ViewBag.Subjects = subjects;
            ViewBag.Grades = grades;
            ViewBag.PG = professor_Grades;
            ViewBag.PS = professor_Subjects;
            return View();
        }
    }
}
