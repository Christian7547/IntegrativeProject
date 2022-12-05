using Microsoft.AspNetCore.Mvc;
using ProfessorManagement.Data;
using ProfessorManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ProfessorManagement.Controllers
{
    public class AccessController : Controller
    {
        private readonly ProfessorContext _context;
        public AccessController(ProfessorContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var userLogin = from usr in _context.Users
                            join rolesX in _context.Roles
                            on usr.RoleID equals rolesX.RoleId
                            where usr.Email == email && usr.Password == password
                            select new
                            {
                                UserId = usr.UserId,
                                Name = usr.Name,
                                Email = usr.Email,
                                Password = password,
                                RoleName = rolesX.RoleName
                            };
            if (userLogin.Count() != 0)
            {
                string _rol = userLogin.First().RoleName;
                string _email = userLogin.First().Email;
                string _name = userLogin.First().Name;

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, _name),
                    new Claim("Email", _email),
                    new Claim(ClaimTypes.Role, _rol)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Message"] = "Not registered";
                return View();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User userf)
        {
            userf.RoleID = 3;
            _context.Add(userf);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Access");
        }
    }
}
