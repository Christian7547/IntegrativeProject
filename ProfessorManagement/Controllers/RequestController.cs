using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfessorManagement.Data;
using ProfessorManagement.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ProfessorManagement.Controllers
{
    public class RequestController : Controller
    {
        private readonly ProfessorContext _context;

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
            string accountSid = "ACd0d33519805f452ac53c933e8b63e05f";
            string authToken = "0c62a8f6f8c993465e5d6f9b1242bdd8";
            string contentMessageA = "Su solicitud ha sido aprobada, \nBienvenido a nuestra unidad educativa!";
            string contentMessageR = "Su solicitud ha sido rechazada, \nPara consultas acérquese a nuestras instalaciones";
            byte newStatusRequest;

            var professor = (from p in _context.Professors
                             where p.Id == professorId
                             select p).SingleOrDefault();

            TwilioClient.Init(accountSid, authToken);
            if (newStatus == "reject")
            {
                newStatusRequest = 2;
                professor.Status = newStatusRequest;

                var messageAprove = new CreateMessageOptions(new PhoneNumber("whatsapp:+591" + professor.Phone));
                messageAprove.From = new PhoneNumber("whatsapp:+14155238886");
                messageAprove.Body = contentMessageR;
                var sendMessageAprove = MessageResource.Create(messageAprove);
            }
            else
            {
                newStatusRequest = 1;
                professor.Status = newStatusRequest;

                var message = new CreateMessageOptions(new PhoneNumber("whatsapp:+591" + professor.Phone));
                message.From = new PhoneNumber("whatsapp:+14155238886");
                message.Body = contentMessageA;
                var sendMessage = MessageResource.Create(message);

            }

            Response response = new Response()
            {
                Description= description,
                NewStatusRequest= newStatusRequest,
                RequestId= Id   
            };


            var professor_Request = (from pr in _context.ProfessorsRequests
                                                   where pr.Id == Id
                                                   select pr)
                                                .SingleOrDefault(); ;
            professor_Request.NewStatus = newStatusRequest;

            _context.Responses.Add(response);
            await _context.SaveChangesAsync();

            return RedirectToAction("ShowRequests", "Request");
        }

        public async Task<IActionResult> RejectRequest()
        {
            var query = _context.ProfessorsRequests.Include(p => p.Professor)
                .Include(r => r.Request)
                .Where(p => p.NewStatus == 2);
            return View(await query.ToArrayAsync());
        }
    }
}
