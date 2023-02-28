using Company.DAL.Entities;
using Microsoft.CodeAnalysis.Emit;
using System.Net;
using System.Net.Mail;

namespace Company.PL.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.ethereal.email", 587);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("jovanny11@ethereal.email", "UeNdwYw1sWAM4WD7jJ");

            client.Send("ahmed@gmail.com",email.To, email.Title,email.Body);
        }
    }
}
