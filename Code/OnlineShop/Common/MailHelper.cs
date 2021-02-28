using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace Common
{
    public class MailHelper
    {
        /// <summary>
        /// Hàm gửi mail
        /// </summary>
        /// <param name="toEmailAddress">Gửi từ email</param>
        /// <param name="subject">Tiêu đề mail</param>
        /// <param name="content">Nội dung mail</param>
        public void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            //Tạo mới một MailMessage gồm 2 tham số 
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));

            // Tiêu đề
            message.Subject = subject;

            //Nội dung có cho phép html hay không
            message.IsBodyHtml = true;

            //Gán nội dung
            message.Body = body;

            //Khởi tạo SmtpClient
            var client = new SmtpClient();

            //Email và password
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);

            //Gán
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);

            //html Assets/client/template/neworder.html
        }
    }
}
