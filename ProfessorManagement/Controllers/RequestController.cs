using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfessorManagement.Data;
using ProfessorManagement.Models;

namespace ProfessorManagement.Controllers
{
    public class RequestController : Controller
    {
        private readonly ProfessorContext _context;

        private int? Request_Id { get; set; }    

        public RequestController(ProfessorContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ShowRequests()
        {
            var query = _context.ProfessorsRequests.Include(p => p.Professor).Include(r => r.Request).Where(p=> p.NewStatus == 0);
            return View(await query.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> DetailsRequest(int? id)
        {
            var query = await _context.ProfessorsRequests.Include(p => p.Professor)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Response(int? id)
        {
            var query = await _context.ProfessorsRequests.Include(p => p.Professor)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            return View(query);
        }

        [HttpPost]
        public async Task<IActionResult> Response(int professorId, int Id, string description, string newStatus)
        {
            if(description == null)
            {
                ViewBag.IsNull = "Please enter a description for the answer";
            }
            byte newStatusRequest;
            if(newStatus == "reject")
            {
                newStatusRequest = 2;
            }
            newStatusRequest = 1;

            Response response = new Response()
            {
                Description= description,
                NewStatusRequest= newStatusRequest,
                RequestId= Id   
            };


            var professor = (from p in _context.Professors
                                         where p.Id == professorId select p)
                                         .SingleOrDefault();
            professor.Status = newStatusRequest;

            var professor_Request = (from pr in _context.ProfessorsRequests
                                                   where pr.Id == Id
                                                   select pr)
                                                .SingleOrDefault(); ;
            professor_Request.NewStatus = newStatusRequest;

            _context.Responses.Add(response);
            await _context.SaveChangesAsync(); 

            return RedirectToAction("ShowRequests", "Request");
        }
    }
}
