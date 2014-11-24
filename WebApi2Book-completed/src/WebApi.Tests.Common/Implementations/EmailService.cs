using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebApi.Tests.Common.Interfaces;

namespace WebApi.Tests.Common.Implementations
{
	public class EmailService : IEmailService
	{
		private static IEmailService mvInstance = new EmailService();

		private EmailService() { }

		public static IEmailService GetInstance()
		{
			mvInstance.Reset();

			return mvInstance;
		}

		public static int Errors { get; set; }

		public void SendTestReport()
		{
			if (Errors <= 0)
				return;

			MailMessage MailMesaji = new MailMessage();

			MailMesaji.Subject = "subject";

			MailMesaji.Body = string.Format("Test has finished at {0}", DateTime.Now);

			MailMesaji.BodyEncoding = Encoding.GetEncoding("Windows-1254");

			MailMesaji.From = new MailAddress("deadean@yandex.ru");

			MailMesaji.To.Add(new MailAddress("deadean@yandex.ru"));

			System.Net.Mail.SmtpClient Smtp = new SmtpClient();

			Smtp.Host = "smtp.yandex.com";

			Smtp.EnableSsl = true;

			Smtp.Credentials = new System.Net.NetworkCredential("deadean", "810experimentum1989");

			Smtp.Send(MailMesaji);
		}


		public void Reset()
		{
			Errors = 0;
		}


		public void AddBadTest()
		{
			Errors++;
		}
	}
}
