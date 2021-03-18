namespace ChatApp.Models
{
    public class ConnectionModel : BaseModel
    {
        public string ConnectionID { get; set; }
        public string UserAgent { get; set; }
        public bool IsConnected { get; set; }
        public long UserID { get; set; }
        public UserModel User { get; set; }

    }
}
