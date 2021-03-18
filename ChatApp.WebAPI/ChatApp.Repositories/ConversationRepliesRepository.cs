using ChatApp.DL;
using ChatApp.Models;
using ChatApp.Repositories.Common;
using ChatApp.Repositories.Interfaces;

namespace ChatApp.Repositories
{
    public class ConversationRepliesRepository : GenericRepository<ConversationReplyModel>, IConversationRepliesRepository
    {
        public ConversationRepliesRepository(ChatAppContext context) : base(context)
        {
        }
    }
}
