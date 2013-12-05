using TestTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTask.WebUI.Models {
    public class UserListViewModel {

        private int curPage = 1;
        private int pageSize = 5;

        public IEnumerable<User> Users { private get; set; }
        public IQueryable<Role> Roles { get; set; }
        public IEnumerable<SelectListItem> listRoles {
            get {
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var role in Roles) {
                    SelectListItem sel = new SelectListItem {
                        Text = role.RoleName,
                        Value = role.RoleId.ToString(),
                    };
                    list.Add(sel);
                }
                return list.AsQueryable<SelectListItem>();
            }
        }

        public PagingInfo pagingInfo { get; set; }

        public bool isNull() { 
            return (Users == null || Roles == null);
        }

        public void setItemsPerPage(int _curPage, int _pageSize) {
            curPage = _curPage;
            pageSize = _pageSize;
        }

        public IEnumerable<User> getItemsPerPage() {
            var items = Users.OrderBy(p => p.UserId).Skip((curPage - 1) * pageSize).Take(pageSize);
            pagingInfo = new PagingInfo {
                CurrentPage = curPage,
                ItemsPerPage = pageSize,
                TotalItems = Users.Count()
            };
            return items;
        }

        public IEnumerable<SelectListItem> getRolesForUser(User user) {
            foreach (var role in Roles) {
                SelectListItem sel = new SelectListItem {
                    Text = role.RoleName,
                    Value = role.RoleId.ToString(),
                    Selected = (role.RoleId == user.RoleId)
                };
                yield return sel;
            }
        }
    }
}