using TestTask.Domain.Entities;
using System.Linq;

namespace TestTask.Domain.Abstract {
    public interface IUserRepository {
        IQueryable<User> Users { get; }
        IQueryable<Role> Roles { get; }
        void SaveUser(User user);
        void ChangeRoleUser(User user, Role role);
        void ActivateUser(User user);
        void DeactivateUser(User user);
    }
}
