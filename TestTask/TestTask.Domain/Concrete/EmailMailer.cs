using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Domain.Concrete {
    public class EmailMailer {

        private EmailSettings setting = new EmailSettings();

        public bool Send(string emailTo, string titleSubject, string message) {
            try {
                setting.MailToAddress = emailTo;
                using (var smtpClient = new SmtpClient()) {
                    smtpClient.EnableSsl = setting.UseSsl;
                    smtpClient.Host = setting.ServerName;
                    smtpClient.Port = setting.ServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(setting.Username, setting.Password);
                    if (setting.WriteAsFile) {
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtpClient.PickupDirectoryLocation = setting.FileLocation;
                        smtpClient.EnableSsl = false;
                    }

                    MailMessage mailMessage = new MailMessage(
                     setting.MailFromAddress, // From
                     setting.MailToAddress, // To
                     titleSubject, // Subject
                     message); // message

                    if (setting.WriteAsFile) {
                        mailMessage.BodyEncoding = Encoding.ASCII;
                    }

                    smtpClient.Send(mailMessage);
                    return true;
                }
            } catch (Exception e) {
                return false;
            }
        }
    }
}
