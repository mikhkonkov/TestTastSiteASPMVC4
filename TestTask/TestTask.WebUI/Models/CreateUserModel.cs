using TestTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TestTask.WebUI.Models {
    public class CreateUserModel {
        private UserListViewModel edit = new UserListViewModel();

        public IQueryable<Role> Roles { get; set; }

        public IEnumerable<SelectListItem> selectListItem {
            get {
                edit.Roles = Roles;
                return edit.listRoles;
            }
        }

        [Required(ErrorMessage = "Please enter a firstname")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a lastname")]
        public string LastName { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            ErrorMessage = "Not is correct Email")]
        [Required(ErrorMessage = "Please enter a email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please enter a date birth")]
        //[RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](19|20)[0-9]{2}$",
        //    ErrorMessage = "DateBirth is incorrect")]
        public string DateBirth { get; set; }

        public int RoleId { get; set; }
    }
}