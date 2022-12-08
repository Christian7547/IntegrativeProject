using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProfessorManagement.Data;
using ProfessorManagement.Models;

namespace ProfessorManagement.Controllers
{
    public class SendRequestController : Controller
    {       
        private readonly ProfessorContext _context;
        public SendRequestController(ProfessorContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(SendRequest sendRequest)
        {
            Professor professor = new Professor();
            Request request = new Request();
            professor.Name= sendRequest.NamesProfessor;
            professor.LastName = sendRequest.LastNameProfessor;
            professor.SecondLastName = sendRequest.SecondLastNameProfessor;
            professor.CI = sendRequest.CIProfessor; 
            professor.BirthDate = sendRequest.BirthDateProfessor;
            professor.Phone= sendRequest.PhoneProfessor;
            professor.Address= sendRequest.AddressProfessor;
            professor.RegisterDate = DateTime.Now;
            professor.RegisterType = 0;
            professor.Status = 0;

            request.Gestion = sendRequest.ForWhatGestion;
            request.Degree = sendRequest.Degree;
            request.Comment = sendRequest.Comment;

            await CreateProfessorByRequest(professor);
            await CreateRequest(request);
            await TableProfessorRequest(professor.Id, request.Id, sendRequest.Specialty);
            return RedirectToAction("Index", "Home");
        }

        public async Task CreateRequest(Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateProfessorByRequest(Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task TableProfessorRequest(int idP, int r, string specialty)
        {
            Professor_Request professor_Request = new Professor_Request();  
            professor_Request.ProfessorId = idP;
            professor_Request.RequestId = r;
            professor_Request.NewStatus = 0;
            professor_Request.Specialty = specialty;
            professor_Request.ChangeDate = DateTime.Today;   
            
            _context.Add(professor_Request);
            await _context.SaveChangesAsync();
        }
    }
}
