namespace ChatApplicationModel.Util
{
    using ChatApplicationModel.Model;
    using ChatApplicationModel.Util.Infc;
    using Experimental.System.Messaging;
    using System;
    using System.Net;
    using System.Net.Mail;
    public class SendEmail
    {
        public static IMessagingService MessagingService = new MessagingService();

        public static void Email()
        {
            Message[] messages = MessagingService.ReceiveMsg();
            foreach (Message message in messages)
            {
                message.Formatter = new XmlMessageFormatter(new Type[] { typeof(Email) });
                Email EmailDetail = (Email) message.Body;
                SendMessage(EmailDetail.email,EmailDetail.message, EmailDetail.email); 
            }
        }

        private static void SendMessage(string subject,string body,string to)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress("ashish52922@gmail.com");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress("ashish52922@gmail.com"));

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";

                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = mailMessage.From.Address;
                networkCredential.Password = "peshwani@darshita@11121994";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
            catch (Exception e) 
            {
                throw e;
            }
        }
    }
}
