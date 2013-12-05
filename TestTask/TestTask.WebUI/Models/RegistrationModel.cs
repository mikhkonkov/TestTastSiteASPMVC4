using System;
using System.ComponentModel.DataAnnotations;

namespace TestTask.WebUI.Models {
    public class RegistrationModel {

        [Required(ErrorMessage = "Please enter a captcha")]
        public string Captcha { get; set; }

        [RegularExpression(@"[a-zA-Zа-яА-Я]{1,100}",
            ErrorMessage = "Firstname is incorrect")]
        [Required(ErrorMessage = "Please enter a firstname")]
        public string FirstName { get; set; }

        [RegularExpression(@"[a-zA-Zа-яА-Я]{1,100}",
            ErrorMessage = "Lastname is incorrect")]
        [Required(ErrorMessage = "Please enter a lastname")]
        public string LastName { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            ErrorMessage = "Email is incorrect")]
        [Required(ErrorMessage = "Please enter a email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [Compare("Password", ErrorMessage = "Password not compare")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Please enter a product firstname")]
        public DateTime DateBirth { get; set; }

    }
}