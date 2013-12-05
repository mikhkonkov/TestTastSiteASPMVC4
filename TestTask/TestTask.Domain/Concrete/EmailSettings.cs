using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Domain.Concrete {
    public class EmailSettings {
        public string MailToAddress = "";
        public string MailFromAddress = "" //enter email
        public bool UseSsl = true;
        public string Username = "" //enter the login for authorization and sending email
        public string Password = "" //enter the password for authorization and sending email
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\sports_store_emails";
    }
}
