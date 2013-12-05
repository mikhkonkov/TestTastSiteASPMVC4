using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TestTask.Domain.Entities {
    public class User {

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }
        
        public string Cookies { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }

        public DateTime DateBirth { get; set; }

    }
}
