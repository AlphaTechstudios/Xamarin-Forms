using ChatApp.DL;
using ChatApp.Models;
using ChatApp.Repositories.Common;
using ChatApp.Repositories.Interfaces;

namespace ChatApp.Repositories
{
    public class ConversationsRepository : GenericRepository<ConversationModel>, IConversationsRepository
    {
        public ConversationsRepository(ChatAppContext context) : base(context)
        {
        }
    }
}
