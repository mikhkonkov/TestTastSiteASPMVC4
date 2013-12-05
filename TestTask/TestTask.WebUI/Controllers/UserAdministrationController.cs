using TestTask.Domain.Abstract;
using TestTask.Domain.Concrete;
using TestTask.Domain.Entities;
using TestTask.WebUI.CustomAttribute;
using TestTask.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTask.WebUI.Controllers {

    [PageAuthorize(UserRoles = "admin")]
    public class UserAdministrationController : Controller {
        private int PageSize = 5;
        private IUserRepository repository;

        public UserAdministrationController(IUserRepository repo) {
            repository = repo;
        }

        public ActionResult Index(string name, bool? category, int page = 1) {
            if (!String.IsNullOrEmpty(name)) {
                var foundUsers = repository.Users.Where(u => (u.FirstName.Contains(name)
                        || u.LastName.Contains(name) || u.Email.Contains(name)));
                TempData["message"] = string.Format("Found {0} items.", foundUsers.Count());
                UserListViewModel foundModel = new UserListViewModel {
                    Users = foundUsers,
                    Roles = repository.Roles,
                };
                foundModel.setItemsPerPage(1, foundUsers.Count());
                return View(foundModel);
            }
            UserListViewModel model = new UserListViewModel {
                Users = repository.Users.Where(u => category == null || u.IsActive == category).OrderBy(u => u.UserId),
                Roles = repository.Roles,
            };
            model.setItemsPerPage(page, PageSize);
            return View(model);
        }

        public PartialViewResult Find() {
            FindModel find = new FindModel();
            return PartialView(find);
        }

        [HttpPost]
        public ActionResult Find(FindModel find) {
            
            return RedirectToAction("Index", "UserAdministration", new { @name = find.name });
        }

        public ActionResult Activate(int userId) {
            User user = repository.Users.FirstOrDefault(u => u.UserId == userId);
            repository.ActivateUser(user);
            sendEmail(user.Email, "Activate account", string.Format("Hello, {0}, your account has been activated.", user.FirstName));
            TempData["message"] = string.Format("{0} {1} has been activated", user.FirstName, user.LastName);
            return RedirectToAction("Index");
        }

        public ActionResult Deactivate(int userId) {
            User user = repository.Users.FirstOrDefault(u => u.UserId == userId);
            repository.DeactivateUser(user);
            sendEmail(user.Email, "Deactivate account", string.Format("Hello, {0}, your account has been deactivated.", user.FirstName));
            TempData["message"] = string.Format("{0} {1} has been deactivated", user.FirstName, user.LastName);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int userId, int Id) {
            User user = repository.Users.FirstOrDefault(u => u.UserId == userId);
            Role role = repository.Roles.FirstOrDefault(u => u.RoleId == Id);
            repository.ChangeRoleUser(user, role);
            sendEmail(user.Email, "Change of role account",
                string.Format("Hello, {0}, your role was changed on {1}", user.FirstName, role.RoleName));
            TempData["message"] = string.Format("Role of {0} {1} was changed on {2} ",
                user.FirstName, user.LastName, role.RoleName);
            return RedirectToAction("Index");
        }

        public ViewResult Create() {
            CreateUserModel model = new CreateUserModel();
            model.Roles = repository.Roles;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateUserModel model, int Id) {
            model.Roles = repository.Roles;
            DateTime date;
            if (!DateTime.TryParse(model.DateBirth, out date)) {
                ModelState.AddModelError("date", "Date Birth is incarrect");
                return View(model);
            }
            if (ModelState.IsValid) {
                User user = new User {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    DateBirth = date,
                    IsActive = model.IsActive,
                    RoleId = model.RoleId
                };
                user.RoleId = Id;
                user.Cookies = Guid.NewGuid().ToString();
                repository.SaveUser(user);
                sendEmail(user.Email, "Registration",
                    string.Format("Hello, {0}, you registered.", user.FirstName));
                TempData["message"] = string.Format("{0} has been saved", user.FirstName);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        private void sendEmail(string emailAdressTo, string title, string message) {
            EmailMailer mailer = new EmailMailer();
            mailer.Send(emailAdressTo, title, message);
        }

        public PartialViewResult Menu(bool? category = null) {
            ViewBag.selectedCategory = category;
            IEnumerable<bool> categories = repository.Users.Select(u => u.IsActive).Distinct();
            return PartialView(categories);
        }
    }
}
