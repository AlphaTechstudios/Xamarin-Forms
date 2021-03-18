using ChatApp.DL;
using ChatApp.Models;
using ChatApp.Repositories.Common;
using ChatApp.Repositories.Interfaces;

namespace ChatApp.Repositories
{
    public class UsersRepository : GenericRepository<UserModel>, IUsersRepository
    {
        public UsersRepository(ChatAppContext context) : base(context)
        {
        }
    }
}
