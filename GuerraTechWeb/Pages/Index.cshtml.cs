using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using GuerraTechWeb.Models;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace GuerraTechWeb.Pages
{

    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public SendMessage sendMail { get; set; }

        public async Task OnPost()
        {
           var smtpAddress = _configuration.GetValue<string>("EmailSettings:SMTP");
           var emailUserName = _configuration.GetValue<string>("EmailSettings:UserName");
           var emailPassword = _configuration.GetValue<string>("EmailSettings:Password");

            using (MailMessage mail = new MailMessage())
            {
                string To = emailUserName.ToString();
                string Subject = sendMail.Subject;
                string Body = sendMail.Message;
                mail.From = new MailAddress(sendMail.Email);
                mail.To.Add(To);
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(smtpAddress, 465))
                {
                    smtp.Credentials = new NetworkCredential(emailUserName, emailPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

        }


    }
}