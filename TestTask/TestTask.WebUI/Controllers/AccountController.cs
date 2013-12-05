using TestTask.Domain.Abstract;
using TestTask.Domain.Entities;
using TestTask.WebUI.Helpers;
using TestTask.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTask.Domain.Concrete;
using TestTask.WebUI.CustomAttribute;
using System.Web.Helpers;
using System.IO;
using System.Drawing;

namespace TestTask.WebUI.Controllers {
    public class AccountController : Controller {
        private IUserRepository repository;

        public AccountController(IUserRepository repo) {
            repository = repo;
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel, string returnUrl) {
            if (ModelState.IsValid) {
                User user = AuthHelper.GetUser(viewModel.UserName,
                    SecurityHelper.getHash(viewModel.Password));
                if (user != null) {
                    if (!user.IsActive) {
                        ViewBag.authErr = "Sorry. Your account is deactivated.";
                        return View(viewModel);
                    }
                    AuthHelper.LoginUser(HttpContext, user.Cookies);
                    if (user.RoleId == UsersContainer.getRoleIdByName("admin"))
                        return RedirectToAction("Index", "UserAdministration");
                    if (user.RoleId == UsersContainer.getRoleIdByName("client")) {
                        if (returnUrl == null)
                            return RedirectToAction("Index", "Home");
                        if (returnUrl.ToLower().Contains("/admin")) {
                            ViewBag.authErr = "Sorry.Your account is not admin.";
                            return View(viewModel);
                        }
                        return Redirect(returnUrl);
                    }
                }
                ModelState.AddModelError("", "Incorrect username or password");
            }
            return View(viewModel);
        }

        public ActionResult Logout() {
            AuthHelper.LogoutUser(HttpContext);
            return RedirectToAction("Index", "Home");
        }

        [PageAuthorize]
        public ActionResult MyAccount() {
            var user = getCurUser();
            return View(user);
        }

        public ActionResult Upload(ImageEditorModel model) {
            var image = WebImage.GetImageFromRequest();
            int wigth = 500;
            if (image != null) {
                if (image.Width > wigth) {
                    image.Resize(wigth, ((wigth * image.Height) / image.Width));
                }

                var filename = Path.GetFileName(image.FileName);
                image.Save(Path.Combine("~/Images", filename));
                filename = Path.Combine("~/Images", filename);
                model.ImageUrl = Url.Content(filename);
                model.Width = image.Width;
                model.Height = image.Height;
                model.Top = image.Height * 0.1;
                model.Left = image.Width * 0.9;
                model.Right = image.Width * 0.9;
                model.Bottom = image.Height * 0.9;
                return View("CropImage", model);

            }
            return View("upload", model);
        }

        public ActionResult EditImage(ImageEditorModel editor) {
            var image = new WebImage("~" + editor.ImageUrl);
            var height = image.Height;
            var width = image.Width;
            image.Crop((int)editor.Top, (int)editor.Left, (int)(height - editor.Bottom), (int)(width - editor.Right));
            image.Resize(200, 200, true, false);

            var authCookeis = Request.Cookies["__AUTH"];
            if (authCookeis != null) {
                var user = AuthHelper.GetUserByCookies(authCookeis.Value.ToString());
                user.ImageMimeType = image.ImageFormat;
                user.ImageData = image.GetBytes();
                repository.SaveUser(user);
                sendEmail(user.Email, "Change avatar",
                    string.Format("Hello, {0}, your avatar has been successfully changed.", user.FirstName));
                TempData["message"] = string.Format("{0}, your avatar has been successfully changed", user.FirstName);
            }
            System.IO.File.Delete(Server.MapPath(editor.ImageUrl));
            return RedirectToAction("MyAccount", "Account");
        }

        public FileContentResult GetAvatar() {
            DefaultAvatar defAvatar = new DefaultAvatar();
            var defFile = File(defAvatar.Avatar, defAvatar.MimeType);

            var user = getCurUser();
            if (user != null) {
                if (user.ImageData == null)
                    return defFile;
                return File(user.ImageData, user.ImageMimeType);
            }
            return defFile;
        }

        public ViewResult ChangeInformation() {

            ChangeInformationModel model = new ChangeInformationModel();
            var user = getCurUser();
            if (user != null) {
                model.DateBirth = user.DateBirth;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeInformation(ChangeInformationModel model) {
            if (ModelState.IsValid) {
                var user = getCurUser();
                user.DateBirth = model.DateBirth;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Password = model.Password;
                repository.SaveUser(user);
                sendEmail(user.Email, "Change information",
                    string.Format("Hello, {0}, your account information has been successfully changed.", user.FirstName));
                TempData["message"] = string.Format("{0}, your account information has been successfully changed", user.FirstName);
                return RedirectToAction("MyAccount", "Account");
            }
            return View(model);
        }

        private User getCurUser() {
            var authCookeis = Request.Cookies["__AUTH"];
            return (authCookeis != null) ? AuthHelper.GetUserByCookies(authCookeis.Value) : null;
        }

        private void sendEmail(string emailAdressTo, string title, string message) {
            EmailMailer mailer = new EmailMailer();
            mailer.Send(emailAdressTo, title, message);
        }

    }



}
