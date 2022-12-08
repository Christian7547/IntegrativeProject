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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id) 
        {
            if (id == null || _context.Professors_Subjects == null)
            {
                return NotFound();
            }

            var ps = await _context.Professors_Subjects.FindAsync(id);
            if (ps == null)
            {
                return NotFound();
            }
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "Id", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            return View(ps);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfessorId,SubjectId")] Professor_Subject professor_subject)
        {
            ViewData["ProfessorId"] = new SelectList(_context.Professors_Subjects, "Id", "Name", professor_subject.ProfessorId);
            ViewData["SubjectId"] = new SelectList(_context.Professors_Subjects, "Id", "Name", professor_subject.SubjectId);
            if (id != professor_subject.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(professor_subject);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Professor_SubjectExists(professor_subject.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("ListAssignments", "Asignation");
        }
        private bool Professor_SubjectExists(int id)
        {
            return _context.Professors.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> ReportSearchProfessor()
        {
            var subjectContext = _context.Professors_Subjects.Include(s => s.subject).Include(p => p.professor);
            return View(await subjectContext.ToListAsync());
        }

        [HttpPost]
        public ViewResult ReportSearchProfessor(string searchString)
        {
            var query = from p in _context.Professors_Subjects.Include(s=> s.subject).Include(p=> p.professor)
                      select p;
            if(!string.IsNullOrEmpty(searchString) )
            {
                query = query.Where(p => p.professor.Name.Contains(searchString)
                                || p.professor.LastName.Contains(searchString));
            }
            return View(query.ToList());
        }
    }
}
