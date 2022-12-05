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
        public async Task<IActionResult> Create(Professor_Subject professorSubjectModel)
        {
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "Id", "Name", professorSubjectModel.ProfessorId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", professorSubjectModel.SubjectId);

            _context.Add(professorSubjectModel);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListAssignments", "Asignation");
        }

        [HttpGet]
        public IActionResult Assignments()
        {
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "Id", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");

            return View();
        }

        public async Task<IActionResult> ListAssignments()
        {
            var subjectContext = _context.Professors_Subjects.Include(s => s.subject).Include(p => p.professor);
            return View(await subjectContext.ToListAsync());
        }
    }
}
