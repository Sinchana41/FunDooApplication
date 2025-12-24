using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Service
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void Send(string to, string subject, string body)
        {
            var smtp = new SmtpClient(_config["Smtp:Host"])
            {
                Port = int.Parse(_config["Smtp:Port"]),
                Credentials = new NetworkCredential(
                _config["Smtp:Email"],
                _config["Smtp:Password"]),
                EnableSsl = true
            };

            smtp.Send(
                _config["Smtp:Email"],  // FROM
                to,                     // TO
                subject,                // SUBJECT
                body                     // BODY
            );
        }
    }

}

