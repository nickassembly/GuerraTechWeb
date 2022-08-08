using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using GuerraTechWeb.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using FluentEmail.Smtp;
using FluentEmail.Core;
using System;

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
           // var smtpAddress = _configuration.GetValue<string>("EmailSettings:SMTP");
            var emailServerUsername = _configuration.GetValue<string>("EmailSettings:UserName");
           // var emailServerPassword = _configuration.GetValue<string>("EmailSettings:Password");

            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25, 
            });

            Email.DefaultSender = sender;

            try
            {
                var email = await Email
                    .From(sendMail.Email)
                    .To(emailServerUsername)
                    .Subject(sendMail.Subject)
                    .Body(sendMail.Message)
                    .SendAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }


}
