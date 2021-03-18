using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DL
{
    public class ChatAppContext : DbContext
    {
        public ChatAppContext(DbContextOptions<ChatAppContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<FriendModel> Friends { get; set; }
        public DbSet<ConversationModel> Conversations { get; set; }
        public DbSet<ConversationReplyModel> ConversationReplies { get; set; }
        public DbSet<ConnectionModel> Connections { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
