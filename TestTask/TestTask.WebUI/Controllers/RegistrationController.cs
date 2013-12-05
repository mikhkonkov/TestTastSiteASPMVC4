using TestTask.Domain.Abstract;
using TestTask.Domain.Concrete;
using TestTask.Domain.Entities;
using TestTask.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;

namespace TestTask.WebUI.Controllers
{
    public class RegistrationController : Controller
    {

        private IUserRepository repository;

        public RegistrationController(IUserRepository repo) {
            repository = repo;
        }

        public ViewResult Index() {
            RegistrationModel model = new RegistrationModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(RegistrationModel model) {
            if (model.Captcha != Session[CaptchaImage.CaptchaValueKey].ToString()) {
                ModelState.AddModelError("Captcha", "Text from the image entered incorrectly.");
                return View(model);
            }

            if (ModelState.IsValid) {
                User user = new User {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    DateBirth = model.DateBirth
                };
                user.RoleId = UsersContainer.getRoleIdByName("client");
                user.Cookies = Guid.NewGuid().ToString();
                user.IsActive = false;
                repository.SaveUser(user);
                sendEmail(user.Email, "Registration",
                    string.Format("Hello, {0}, you registered.", user.FirstName));
                TempData["message"] = string.Format("{0} has been saved", user.FirstName);
                return View("Complete");
            }
            return View(model);
        }

        public ActionResult Captcha() {
            Session[CaptchaImage.CaptchaValueKey] = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Arial");

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            ci.Dispose();
            return null;
        }

        private void sendEmail(string emailAdressTo, string title, string message) {
            EmailMailer mailer = new EmailMailer();
            mailer.Send(emailAdressTo, title, message);
        }



    }
}
