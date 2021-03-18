namespace ChatApp.Models.Settings
{
    public class AppSettings
    {
        public string OriginUrl { get; set; }
        public string SecretKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtMobileAudience { get; set; }
        public string JwtWebAudience { get; set; }
    }
}
