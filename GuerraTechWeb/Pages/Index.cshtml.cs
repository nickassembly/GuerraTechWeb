using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using GuerraTechWeb.Models;
using System.Net;

namespace GuerraTechWeb.Pages
{

   public class IndexModel : PageModel
   {
      //private readonly ILogger<IndexModel> _logger;

      //public IndexModel(ILogger<IndexModel> logger)
      //{
      //   _logger = logger;
      //}

      public void OnGet()
      {

      }

      [BindProperty]
      public SendMessage sendMail { get; set; }

      public async Task OnPost()
      {

         using (MailMessage mail = new MailMessage())
         {
            string To = "guerratechinfo@gmail.com";
            string Subject = sendMail.Subject;
            string Body = sendMail.Message;
            mail.From = new MailAddress("guerratechinfo@gmail.com");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = false;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
               smtp.Credentials = new NetworkCredential("guerratechinfo@gmail.com", "password");
               smtp.EnableSsl = true;
               smtp.Send(mail);
            }
         }

      }


   }
}