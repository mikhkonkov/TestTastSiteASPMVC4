using TestTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask.Domain.Concrete {
    public class UsersContainer {
        public static IQueryable<User> Users { get; set; }
        public static IQueryable<Role> Roles { get; set; }

        public static int getRoleIdByName(string name) {
            return Roles.First(p => p.RoleName == name).RoleId;
        }
    }
}