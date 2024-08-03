using ADM.Architectur;
using ADM.Architecture;
using ADM.Interface;
using ADM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ADM.API.Controllers
{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class EmailController : ControllerBase
//    {
//        [HttpPost("SendEmailWithAttachment")]
//        public async Task<IActionResult> SendEmailWithAttachment([FromForm] EmailRequest request)
//        {
//            if (request.Attachments == null || request.Attachments.Length == 0)
//            {
//                return BadRequest("No files uploaded.");
//            }

//            var attachments = new List<Attachment>();

//            foreach (var file in request.Attachments)
//            {
//                var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

//                using (var stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await file.CopyToAsync(stream);
//                }

//                attachments.Add(new Attachment(filePath));
//            }

//            await SendEmailAsync(request.To, request.Subject, request.Body, attachments);

//            return Ok("Email sent successfully.");
//        }

//        private async Task SendEmailAsync(string to, string subject, string body, List<Attachment> attachments)
//        {
//            var from = "testenviocorreo01@gmail.com";
//            var smtpServer = "smtp.gmail.com";
//            var smtpPort = 587;
//            var smtpUser = "testenviocorreo01@gmail.com";
//            var smtpPass = "TestEnvio@";

//            using (var message = new MailMessage())
//            {
//                message.From = new MailAddress(from);
//                message.To.Add(to);
//                message.Subject = subject;
//                message.Body = body;

//                foreach (var attachment in attachments)
//                {
//                    message.Attachments.Add(attachment);
//                }

//                using (var client = new SmtpClient(smtpServer, smtpPort))
//                {
//                    client.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass);
//                    client.EnableSsl = true;
//                    await client.SendMailAsync(message);
//                }
//            }
//        }
//    }

//    public class EmailRequest
//    {
//        public string To { get; set; }
//        public string Subject { get; set; }
//        public string Body { get; set; }
//        public IFormFile[] Attachments { get; set; }
//    }
}
