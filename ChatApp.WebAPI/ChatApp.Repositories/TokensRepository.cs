using ChatApp.DL;
using ChatApp.Models;
using ChatApp.Repositories.Common;
using ChatApp.Repositories.Interfaces;

namespace ChatApp.Repositories
{
    public class TokensRepository : GenericRepository<RefreshTokenModel>, ITokensRepository
    {
        public TokensRepository(ChatAppContext context)
            : base(context)
        {
        }
    }
}
