namespace ChatApp.Models
{
    public class FriendModel: BaseModel
    {
        public long UserID { get; set; }
        public long UserFriendId { get; set; }
        public UserModel UserFriend { get; set; }
    }
}
