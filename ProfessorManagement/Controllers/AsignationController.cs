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

<<<<<<< HEAD
=======
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

>>>>>>> fbc853d42fe4d74b6556b6045fceb9e7a13e0b0b
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
