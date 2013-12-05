using TestTask.Domain.Abstract;
using TestTask.Domain.Concrete;
using TestTask.Domain.Entities;
using TestTask.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTask.WebUI.CustomAttribute {
    public class PageAuthorizeAttribute : AuthorizeAttribute {

        public string UserRoles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            var authCooke = httpContext.Request.Cookies["__AUTH"];

            if (authCooke != null) {
                User user = AuthHelper.GetUserByCookies(authCooke.Value);
                if (user == null)
                    return false;
                if (UserRoles == null)
                    return true;
                return (user.RoleId == UsersContainer.Roles.FirstOrDefault(p => p.RoleName.ToLower() == UserRoles.ToLower()).RoleId);
            }

            return false;
        }

    }
}