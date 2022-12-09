using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using ProfessorManagement.Data;
using ProfessorManagement.Models;

namespace ProfessorManagement.Controllers
{
    public class RecoveryController : Controller
    {
        string urlDomain = "https://localhost:7091/";
        private readonly ProfessorContext _context;
        public RecoveryController(ProfessorContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Recovery(string token)
        {
            Models.User model = new Models.User();
            model.Token_Recovery = token;
            var oUser = _context.Users.Where(d => d.Token_Recovery == token).FirstOrDefault();

            return View(oUser);
        }
        [HttpPost]
        public ActionResult Recovery(string confirmPassword, string token)
        {
            try
            {
                var oUser = _context.Users.Where(d => d.Token_Recovery == token).FirstOrDefault();
                if (oUser != null)
                {
                    oUser.Token_Recovery = token;
                    oUser.Password = confirmPassword;
                    _context.SaveChanges();
                    ViewBag.Message = "Password Modified";
                    return RedirectToAction("Index","Access");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }
        [HttpGet]
        public ActionResult StartRecovery()
        {
            Models.User model = new Models.User();
            return View(model);
        }
        [HttpPost]
        public ActionResult StartRecovery(string email)
        {
            try
            {
                string token = GetSha256(Guid.NewGuid().ToString());

                var oUser = _context.Users.Where(d => d.Email == email).FirstOrDefault();
                if (oUser != null)
                {
                    oUser.Token_Recovery = token;
                    _context.SaveChanges();

                    SendEmail(oUser.Email, token);
                }

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region HELPERS
        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void SendEmail(string EmailDestiny, string token)
        {
            string EmailOrigin = "nahuel.gutierrez.vargas17@gmail.com";
            string password = "wazwdvxppmhviccd\r\n";
            string url = urlDomain + "Recovery/Recovery/?token=" + token;

            MailMessage mailMessage = new MailMessage(EmailOrigin, EmailDestiny, "Recovery Password",
                "<p>Recovery Password email</p><br />" +
                "<a href='" + url + "'>Click for recovery</a>");

            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigin, password);
            smtpClient.Send(mailMessage);
            smtpClient.Dispose();
        }

        #endregion
    }
}
