using TestTask.Domain.Abstract;
using TestTask.Domain.Entities;
using System;
using System.Linq;

namespace TestTask.Domain.Concrete {
    public class EFUserRepository : IUserRepository {

        private EFDbContext context = new EFDbContext();

        public EFUserRepository() {
            UsersContainer.Users = context.Users;
            UsersContainer.Roles = context.Roles;
        }

        public IQueryable<User> Users {
            get { return context.Users; }
        }

        public IQueryable<Role> Roles {
            get { return context.Roles; }
        }

        public void SaveUser(User user) {
            if (user.UserId == 0) {
                user.Password = SecurityHelper.getHash(user.Password);
                context.Users.Add(user);
            } else {
                User dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null) {
                    dbEntry.Password = SecurityHelper.getHash(user.Password);
                    dbEntry.FirstName = user.FirstName;
                    dbEntry.LastName = user.LastName;
                    dbEntry.ImageData = user.ImageData;
                    dbEntry.ImageMimeType = user.ImageMimeType;
                    dbEntry.DateBirth = user.DateBirth;
                }
            }
            context.SaveChanges();
        }

        public void ChangeRoleUser(User user, Role role) {
            if (user.UserId == 0) {
                return;
            } else {
                User dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null) {
                    dbEntry.RoleId = role.RoleId;
                }
            }
            context.SaveChanges();
        }

        public void ActivateUser(User user) {
            User dbEntry = context.Users.Find(user.UserId);
            if (dbEntry != null) {
                dbEntry.IsActive = true;
            }
            context.SaveChanges();
        }

        public void DeactivateUser(User user) {
            User dbEntry = context.Users.Find(user.UserId);
            if (dbEntry != null) {
                dbEntry.IsActive = false;
            }
            context.SaveChanges();
        }

    }
}
