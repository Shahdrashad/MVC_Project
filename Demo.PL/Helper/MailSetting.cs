using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace Demo.PL.Helper
{
	public class Email
	{
		public string Subject { get; set; }
		public string Body { get; set; }
		public string Recipient { get; set;}

	}
	public class MailSetting
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com",507);
			client.EnableSsl = true;
			var creds = new NetworkCredential("shahd@gmail.com","1234");
		//	client.Send("shahd@gmail.com",email.Recipient,email.Body);
		}

	}
}
