using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Models
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ValidationToken { get; set; }
        public string PasswordSalt { get; set; }

        public ICollection<ConversationModel> Conversations { get; set; }

        public ICollection<FriendModel> Friends { get; set; }

        public ICollection<ConnectionModel> Connections { get; set; }
        
        [NotMapped]
        public string RefreshToken { get; set; }

        [NotMapped]
        public DateTime TokenExpireTimes { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
