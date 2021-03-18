using System;
using System.Collections.Generic;

namespace ChatApp.Mobile.Models
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public ICollection<ConversationModel> Conversations { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpireTimes { get; set; }
        public string Token { get; set; }
    }
}
