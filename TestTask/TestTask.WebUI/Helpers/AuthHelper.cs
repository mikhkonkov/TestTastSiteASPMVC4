using TestTask.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;

namespace TestTask.WebUI.Helpers {
    public static class AuthHelper {

        public static void LoginUser(HttpContextBase httpContext, string cookies) {
            var cookie = new HttpCookie("__AUTH") {
                Value = cookies,
                Expires = DateTime.Now.AddMinutes(2880)
            };

            httpContext.Response.Cookies.Add(cookie);
        }

        public static void LogoutUser(HttpContextBase httpContext) {
            if (httpContext.Request.Cookies["__AUTH"] != null) {
                var cookie = new HttpCookie("__AUTH") {
                    Expires = DateTime.Now.AddDays(-1),
                };

                httpContext.Response.Cookies.Add(cookie);
            }
        }

        public static TestTask.Domain.Entities.User GetUser(HttpContextBase httpContext) {
            var authCookie = httpContext.Request.Cookies["__AUTH"];

            if (authCookie != null) {
                TestTask.Domain.Entities.User user = GetUserByCookies(authCookie.Value);
                return user;
            }
            return null;
        }

        public static bool IsAuthenticated(HttpContextBase httpContext) {
            var authCookie = httpContext.Request.Cookies["__AUTH"];
            if (authCookie != null) {
                TestTask.Domain.Entities.User user = GetUserByCookies(authCookie.Value);
                return user != null;
            }
            return false;
        }

        public static TestTask.Domain.Entities.User GetUserByCookies(string cookies) {
            TestTask.Domain.Entities.User user = null;
            try {
                user = UsersContainer.Users.FirstOrDefault(u => u.Cookies == cookies);
            } catch (Exception e) {
                return null;
            }
            return user;
        }

        public static TestTask.Domain.Entities.User GetUser(string email, string password) {
            TestTask.Domain.Entities.User user = null;
            try {
                user = UsersContainer.Users.First(u => u.Email == email && u.Password == password);
            } catch (Exception e) {
                return null;
            }
            return user;
        }
    }
}