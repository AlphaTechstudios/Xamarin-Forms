using ChatApp.DL;
using ChatApp.Models;
using ChatApp.Repositories.Common;
using ChatApp.Repositories.Interfaces;

namespace ChatApp.Repositories
{
    public class FriendsRepository : GenericRepository<FriendModel>, IFriendsRepository
    {
        public FriendsRepository(ChatAppContext context) : base(context)
        {
        }
    }
}
