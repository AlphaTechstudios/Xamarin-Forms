namespace ChatApp.Models
{
    public class ConnectionModel : BaseModel
    {
        public string ConnectionID { get; set; }
        public string UserAgent { get; set; }
        public bool IsConnected { get; set; }
        public bool IsAvailable { get; set; } = true;
        public long UserID { get; set; }
        public UserModel User { get; set; }

    }
}
