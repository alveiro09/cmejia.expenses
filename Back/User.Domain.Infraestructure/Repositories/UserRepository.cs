using Domain.Core.Base;
using UserManagement.Domain.Model;
using UserManagement.Domain.Repositories;

namespace UserManagement.Domain.Infraestructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserManagementContext context)
            : base(context) { }
    }
}
