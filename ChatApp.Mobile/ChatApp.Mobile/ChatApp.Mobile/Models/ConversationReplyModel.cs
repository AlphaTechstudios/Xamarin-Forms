namespace ChatApp.Mobile.Models
{
    public class ConversationReplyModel : BaseModel
	{
		/// <summary>
		/// The sender user id.
		/// </summary>
		public long SenderUserId { get; set; }

		/// <summary>
		/// The conversation reply content
		/// </summary>
		public string Content { get; set; }

		public long ConversationID { get; set; }
		/// <summary>
		/// The  association of the conversation   
		/// </summary>
		//[ForeignKey("ConversationId")]
		public ConversationModel Conversation { get; set; }


	}
}
