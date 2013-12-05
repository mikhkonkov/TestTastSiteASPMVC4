using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestTask.WebUI.Models {
    public class ChangeInformationModel {

        [RegularExpression(@"[a-zA-Zа-яА-Я]{1,100}",
            ErrorMessage = "Firstname is incorrect")]
        [Required(ErrorMessage = "Please enter a firstname")]
        public string FirstName { get; set; }

        [RegularExpression(@"[a-zA-Zа-яА-Я]{1,100}",
            ErrorMessage = "Lastname is incorrect")]
        [Required(ErrorMessage = "Please enter a lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [Compare("Password", ErrorMessage = "Password not compare")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Please enter a product firstname")]
        public DateTime DateBirth { get; set; }
    }
}