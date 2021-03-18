using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Models
{
    public class ConversationModel: BaseModel
    {
        /// <summary>
        /// The conversation replies.
        /// </summary>
        public ICollection<ConversationReplyModel> ConversationsReplies { get; set; }

        /// <summary>
        /// The first user id.
        /// </summary>
        public long UserOneID { get; set; }


        /// <summary>
        /// The second user id.
        /// </summary>
        public long UserTwoID { get; set; }

      

    }
}
