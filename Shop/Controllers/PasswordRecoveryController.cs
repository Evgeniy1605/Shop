using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Shop.Models;

namespace Shop.Controllers
{
	public class PasswordRecoveryController : Controller
	{
        private readonly OrderDbConenction _content;
        public PasswordRecoveryController(OrderDbConenction content)
        {
            _content = content;
        }
        public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendEmail(string Name, string PhoneNumber, string Email)
		{
			if (_content.Users.Where(x => x.Name == Name && x.PhoneNumber == PhoneNumber && x.Email == Email).Count() == 0)
			{
				return View("AccountWasNotFound");
			}
			var user = _content.Users.Where(x => x.Name == Name && x.PhoneNumber == PhoneNumber && x.Email == Email).ToList();

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Shop", Email));
            message.To.Add(MailboxAddress.Parse(Email));
            message.Subject = "Code";
			message.Body = new TextPart("plain")
			{
				Text = $"Yor password:" +
				user.First().PassWord
            };
            string EmailAddress = "shopproject1213@gmail.com";
            string Password = "nyyfnnxnubspycjc";
			using (SmtpClient client = new SmtpClient())
			{
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync(EmailAddress, Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            return View("SendEmail");
		}
		
    }

}
