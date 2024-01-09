using Demo.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Helper
{
    public static class MailSender
    {
        public static string SendMail(MailVM mail)
        {
			try
			{
				var smtp = new SmtpClient("smtp.gmail.com", 587);
				smtp.EnableSsl= true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("ahmedzxc56@gmail.com", "dembgakaafbkkplm");
				smtp.Send("ahmedzxc56@gmail.com", "ahmedzxc56@gmail.com", mail.Title, mail.Message);
				return "Success";
			}
			catch (Exception ex )
			{

				return ex.Message;
			}
        }
    }
}
